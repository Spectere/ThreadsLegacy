using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Threads.Interpreter.Objects.Page;
using Threads.Interpreter.Types;

namespace Threads.Player.UIObjects {
    internal class UIChoice : UIPageObject {
        public override Type HandledType => typeof(Choice);

        public delegate void OnChoiceClick(object sender, RoutedEventArgs e);
        public event OnChoiceClick ChoiceClick;

        public UIChoice(Choice choiceObject, Data storyData) : base(choiceObject, storyData) {
            TextBlock.FontSize = 18.0;
            var button = new Button {
                FontFamily = new FontFamily("Cambria"),
                FontSize = 18.0,
                Tag = choiceObject
            };
            var buttonText = new TextBlock();
            if(choiceObject.Shortcut != null)
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
