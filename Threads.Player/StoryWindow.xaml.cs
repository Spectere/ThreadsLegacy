using System;
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
        private Engine _engine;

        public StoryWindow() {
            InitializeComponent();
        }

        private void Choice_Click(object sender, RoutedEventArgs e) {
            var choice = (Choice)((Button)sender).Tag;
            _engine.SubmitChoice(choice);
            DisplayPage();
        }

        private void DisplayPage() {
            var globalConfig = _engine.Story.Configuration;
            var displayList = _engine.DisplayList;

            Stack.Children.Clear();
            StoryScroll.ScrollToTop();
            Stack.Margin = new Thickness(globalConfig.StoryMarginLeft, 40.0, globalConfig.StoryMarginRight, 20.0);

            // Display room text.
            foreach(var obj in displayList) {
                if(obj.GetType().BaseType != typeof(PageObject)) continue;

                var pObj = (IPageObject)obj;
                if(obj.GetType() == typeof(Paragraph)) {
                    Stack.Children.Add(new UIParagraph(pObj));
                } else if(obj.GetType() == typeof(Choice)) {
                    var button = new UIChoice((Choice)pObj);
                    button.ChoiceClick += Choice_Click;
                    Stack.Children.Add(button);
                } else if(obj.GetType() == typeof(PageObjectImage)) {
                    Stack.Children.Add(new UIImage((PageObjectImage)pObj));
                }
            }
        }

        private void StoryWindow_OnKeyDown(object sender, KeyEventArgs e) {
            // System keys.
            if(e.Key == Key.F9) {
                RestartGame();
            }

            // Handle game input (if a page is active).
            if(_engine?.CurrentPage == null) return;
            var inKey = e.Key.ToString().ToUpper();

            /* Adjust numeric entry.
             *   D? - number row
             *   NUMPAD? - numeric keypad */
            if((inKey.StartsWith("D") && inKey.Length == 2) || inKey.StartsWith("NUMPAD"))
                inKey = inKey.Substring(inKey.Length - 1, 1);

            foreach(var choiceObject in _engine.CurrentPage.Objects.Where(o => o.GetType() == typeof(Choice))) {
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

            if(!LoadStory(fileDialog.FileName)) return;

            DisplayPage();
            UpdateTitleBar();
        }

        /// <summary>
        /// Loads a <see cref="Interpreter.Types.Story" /> file into memory.
        /// </summary>
        /// <param name="filename">The path pointing to the <see cref="Interpreter.Types.Story" /> that should be loaded.</param>
        private bool LoadStory(string filename) {
            // Set the directory before loading the story so that relative paths will line up nicely.
            var storyPath = Path.GetDirectoryName(filename);
            if(storyPath == null) return false;

            try {
                _engine = new Engine(filename);
            } catch(Exception ex) when (ex is NullPagesException || ex is NoPagesFoundException) {
                MessageBox.Show("No pages could be found in the loaded story! Aborting.");
                return false;
            }

            // Only change the path if we return successfully, otherwise the currently loaded story could be affected.
            Directory.SetCurrentDirectory(storyPath);
            return true;
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

        /// <summary>
        /// Updates the game window's title bar.
        /// </summary>
        private void UpdateTitleBar() {
            var gameName = _engine.Story.Information.Name;
            Title = string.IsNullOrWhiteSpace(gameName) ? "Threads - [Untitled Story]" : $"Threads - [{gameName}]";
        }
    }
}
