using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using Documents = System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Threads.Interpreter;
using Threads.Interpreter.Exceptions;
using Threads.Marker;
using Threads.Marker.Commands;
using Threads.Interpreter.PageObject;
using PageObjectImage = Threads.Interpreter.PageObject.Image;

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
            var page = _engine.CurrentPage;

            Stack.Children.Clear();
            Stack.Margin = new Thickness(40.0, 40.0, 40.0, 20.0);

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
                        FormatTextBlock(obj.FormattedText, ref text);
                        Stack.Children.Add(text);
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
                        Stack.Children.Add(button);
                        break;
                    case PageObjectType.Image:
                        var imageObj = (PageObjectImage)obj;

                        if(File.Exists(imageObj.Source)) {
                            var source = new Uri(imageObj.Source, UriKind.RelativeOrAbsolute);
                            var container = new Viewbox {
                                Margin = new Thickness(0.0, obj.Style.MarginTop, 0.0, obj.Style.MarginBottom),
                                Stretch = Stretch.Uniform,
                                StretchDirection = StretchDirection.DownOnly
                            };
                            var image = new System.Windows.Controls.Image {
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Margin = new Thickness(0.0, obj.Style.MarginTop, 0.0, obj.Style.MarginBottom),
                                Source = new BitmapImage(source)
                            };

                            image.Width = image.Source.Width;
                            image.Height = image.Source.Height;

                            container.Child = image;
                            Stack.Children.Add(container);
                        } else {
                            var brokenImageText = new TextBlock {
                                FontFamily = new FontFamily("Cambria"),
                                FontSize = 20.0,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Margin = new Thickness(0.0, obj.Style.MarginTop, 0.0, obj.Style.MarginBottom),
                                TextWrapping = TextWrapping.WrapWithOverflow
                            };
                            FormatTextBlock(obj.FormattedText, ref brokenImageText);
                            Stack.Children.Add(brokenImageText);
                        }
                        break;
                }
            }
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
