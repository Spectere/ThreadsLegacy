using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Threads.Interpreter;
using Threads.Interpreter.Exceptions;
using Threads.Interpreter.Schema;

namespace Threads.Player {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StoryWindow : Window {
        private readonly Engine _engine;

        public StoryWindow() {
            InitializeComponent();

            _engine = new Engine();
        }

        private void Choice_Click(object sender, RoutedEventArgs e) {
            var choice = (PageTypeChoice)((Button)sender).Tag;
            //_engine.SubmitChoice(choice);
            DisplayPage();
        }

        private void DisplayPage() {
            /*
            var page = _engine.CurrentPage;

            stack.Children.Clear();

            // Display room text.
            var text = new TextBlock {
                FontFamily = new FontFamily("Cambria"),
                FontSize = 24.0,
                Margin = new Thickness(40.0),
                Text = page.Text,
                TextWrapping = TextWrapping.WrapWithOverflow
            };
            stack.Children.Add(text);

            // Display choices.
            foreach(var choice in page.Choice ?? new PageTypeChoice[0]) {
                var button = new Button {
                    Content = string.Format($"{choice.Shortcut}) {choice.Value}"),
                    FontFamily = new FontFamily("Cambria"),
                    FontSize = 18.0,
                    Margin = new Thickness(40.0, 0.0, 40.0, 7.5),
                    Tag = choice
                };
                button.Click += Choice_Click;
                stack.Children.Add(button);
            }
            */
        }

        private void StoryWindow_OnKeyDown(object sender, KeyEventArgs e) {
            // System keys.
            if(e.Key == Key.F9) {
                RestartGame();
            }

            // Handle game input (if a page is active).
            //if(_engine.CurrentPage == null) return;
            var inKey = e.Key.ToString().ToUpper();

            /* Adjust numeric entry.
             *   D? - number row
             *   NUMPAD? - numeric keypad */
            /*if((inKey.StartsWith("D") && inKey.Length == 2) || inKey.StartsWith("NUMPAD"))
                inKey = inKey.Substring(inKey.Length - 1, 1);

            foreach(var choice in _engine.CurrentPage.Choice ?? new PageTypeChoice[0]) {
                if(inKey == choice.Shortcut.ToUpper()) {
                    _engine.SubmitChoice(choice);
                    DisplayPage();
                    return;
                }
            }*/
        }

        private void Load_OnClick(object sender, RoutedEventArgs e) {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Threads Story (*.XML)|*.xml|All Files (*.*)|*.*";
            fileDialog.Multiselect = false;

            var result = fileDialog.ShowDialog() ?? false;
            if(!result) return;

            _engine.Load(fileDialog.FileName);
            DisplayPage();
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
            if(e.GetPosition(stack).Y < 64)
                menu.Visibility = Visibility.Visible;
            else
                menu.Visibility = Visibility.Collapsed;
        }
    }
}
