using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Threads.Interpreter.PageObject;

namespace Threads.Player.UIObjects {
    internal class UIChoice : UIPageObject {
        public override PageObjectType HandledType => PageObjectType.Choice;

        public delegate void OnChoiceClick(object sender, RoutedEventArgs e);
        public event OnChoiceClick ChoiceClick;

        public UIChoice(Choice choiceObject) : base(choiceObject) {
            TextBlock.FontSize = 18.0;
            var button = new Button {
                FontFamily = new FontFamily("Cambria"),
                FontSize = 18.0,
                Tag = choiceObject
            };
            var buttonText = new TextBlock();
            buttonText.Inlines.Add(new Run($"{choiceObject.Shortcut}) ") { FontWeight = FontWeights.Bold });
            buttonText.Inlines.Add(TextBlock);
            button.Content = buttonText;
            button.Click += Button_Click;
            Content = button;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            ChoiceClick?.Invoke(sender, e);
        }
    }
}
