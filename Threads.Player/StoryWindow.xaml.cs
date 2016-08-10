using System.Linq;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using Documents = System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Threads.Interpreter;
using Threads.Interpreter.Exceptions;
using Threads.Marker;
using Threads.Marker.Commands;
using Threads.Interpreter.PageObject;

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
            var choice = (Choice)((Button)sender).Tag;
            _engine.SubmitChoice(choice);
            DisplayPage();
        }

        private void DisplayPage() {
            var page = _engine.CurrentPage;

            stack.Children.Clear();

            // Create a stack for the page objects and choices.
            var objStack = new StackPanel {
                Margin = new Thickness(40.0, 40.0, 40.0, 20.0)
            };

            // Display room text.
            foreach(var obj in page.Objects) {
                switch(obj.Type) {
                    case PageObjectType.Paragraph:
                        var text = new TextBlock {
                            FontFamily = new FontFamily("Cambria"),
                            FontSize = 24.0,
                            Margin = new Thickness(0.0, obj.Style.MarginTop, 0.0, obj.Style.MarginBottom),
                            TextWrapping = TextWrapping.WrapWithOverflow
                        };
                        FormatTextBlock(((Paragraph)obj).FormattedText, ref text);
                        objStack.Children.Add(text);
                        break;
                    case PageObjectType.Choice:
                        var choice = (Choice)obj;
                        var button = new Button {
                            FontFamily = new FontFamily("Cambria"),
                            FontSize = 18.0,
                            Margin = new Thickness(0.0, obj.Style.MarginTop, 0.0, obj.Style.MarginBottom),
                            Tag = choice
                        };
                        var buttonText = new TextBlock();
                        buttonText.Inlines.Add(new Documents.Run($"{choice.Shortcut}) ") { FontWeight = FontWeights.Bold });
                        FormatTextBlock(choice.FormattedText, ref buttonText);
                        button.Content = buttonText;
                        button.Click += Choice_Click;
                        objStack.Children.Add(button);
                        break;
                }
            }

            // Add container stacks to the main stack.
            stack.Children.Add(objStack);
        }

        private void FormatTextBlock(TextSequence sequence, ref TextBlock textBlock) {
            bool isBold = false, isItalic = false;
            foreach(var seq in sequence.Instructions) {
                switch(seq.Command) {
                    case Command.TextStyle:
                        var styleCmd = (StyleCommand) seq;
                        switch(styleCmd.TextStyle) {
                            case TextStyle.Bold:
                                isBold = !isBold;
                                break;
                            case TextStyle.Italic:
                                isItalic = !isItalic;
                                break;
                        }
                        break;
                    case Command.Text:
                        var textCmd = (TextCommand) seq;
                        var run = new Documents.Run(textCmd.Text);
                        if(isBold) run.FontWeight = FontWeights.Bold;
                        if(isItalic) run.FontStyle = FontStyles.Italic;
                        textBlock.Inlines.Add(run);
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
