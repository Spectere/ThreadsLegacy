using System.IO;
using System.Linq;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Threads.Interpreter;
using Threads.Interpreter.Exceptions;
using Threads.Interpreter.Objects.Page;
using Threads.Player.UIObjects;
using PageObjectImage = Threads.Interpreter.Objects.Page.Image;

namespace Threads.Player {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StoryWindow {
        private readonly Engine _engine;

        public StoryWindow() {
            InitializeComponent();

            _engine = new Engine();
        }

        private void Choice_Click(object sender, RoutedEventArgs e) {
            var choice = (Choice)((Button)sender).Tag;
            _engine.SubmitChoice(choice);
            DisplayPage();
        }

        private void DisplayPage() {
            var globalConfig = _engine.Story.Configuration;
            var page = _engine.CurrentPage;

            Stack.Children.Clear();
            Stack.Margin = new Thickness(globalConfig.StoryMarginLeft, 40.0, globalConfig.StoryMarginRight, 20.0);

            // Display room text.
            foreach(var obj in page.Objects) {
                switch(obj.Type) {
                    case PageObjectType.Paragraph:
                        Stack.Children.Add(new UIParagraph(obj));
                        break;
                    case PageObjectType.Choice:
                        var button = new UIChoice((Choice)obj);
                        button.ChoiceClick += Choice_Click;
                        Stack.Children.Add(button);
                        break;
                    case PageObjectType.Image:
                        Stack.Children.Add(new UIImage((PageObjectImage)obj));
                        break;
                }
            }
        }

        private void StoryWindow_OnKeyDown(object sender, KeyEventArgs e) {
            // System keys.
            if(e.Key == Key.F9) {
                RestartGame();
            }

            // Handle game input (if a page is active).
            if(_engine.CurrentPage == null) return;
            var inKey = e.Key.ToString().ToUpper();

            /* Adjust numeric entry.
             *   D? - number row
             *   NUMPAD? - numeric keypad */
            if((inKey.StartsWith("D") && inKey.Length == 2) || inKey.StartsWith("NUMPAD"))
                inKey = inKey.Substring(inKey.Length - 1, 1);

            foreach(var choiceObject in _engine.CurrentPage.Objects.Where(o => o.Type == PageObjectType.Choice)) {
                var choice = (Choice)choiceObject;
                if(inKey == choice.Shortcut.ToString().ToUpper()) {
                    _engine.SubmitChoice(choice);
                    DisplayPage();
                    return;
                }
            }
        }

        private void Load_OnClick(object sender, RoutedEventArgs e) {
            var fileDialog = new OpenFileDialog {
                Filter = "Threads Story (*.XML)|*.xml|All Files (*.*)|*.*",
                Multiselect = false
            };

            var result = fileDialog.ShowDialog() ?? false;
            if(!result) return;

            // Set the directory before loading the story so that relative paths will line up nicely.
            // ReSharper disable once AssignNullToNotNullAttribute
            Directory.SetCurrentDirectory(Path.GetDirectoryName(fileDialog.FileName));
            _engine.Load(fileDialog.FileName);
            DisplayPage();
            Title = $"Threads - [{_engine.Story.Information.Name}]";
        }

        private void Restart_OnClick(object sender, RoutedEventArgs e) {
            RestartGame();
        }

        private void RestartGame() {
            try {
                _engine.Restart();
                DisplayPage();
            } catch(StoryNotLoadedException) { /* null handler */ }
        }

        private void StoryWindow_OnMouseMove(object sender, MouseEventArgs e) {
            Menu.Visibility = e.GetPosition(this).Y < 64
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}
