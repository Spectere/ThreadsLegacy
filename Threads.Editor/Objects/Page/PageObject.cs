using System.Windows;
using System.Windows.Controls;

namespace Threads.Editor.Objects.Page {
    internal abstract class PageObject : EditorObject {
        protected sealed override void BuildEditor() {
            var pageObject = (Interpreter.Objects.Page.PageObject)ObjectData;

            // Common properties.
            var textEditor = new TextBox {
                Text = pageObject.FormattedText.MarkupText,
                Height = 120,
                TextWrapping = TextWrapping.WrapWithOverflow,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };
            CreateBoundTextBox(pageObject.FormattedText, "MarkupText", ref textEditor);
            AppendRow(new Label { Content = "Text" }, textEditor);

            // Additional properties for this specific control.
            BuildPageObjectEditor();

            // Standard PageObject style editor.
            AppendRow(new Separator());
            AppendRow(new Label { Content = "Top Margin" },
                CreateBoundTextBox(pageObject.Style, "MarginTop"));
            AppendRow(new Label { Content = "Bottom Margin" },
                CreateBoundTextBox(pageObject.Style, "MarginBottom"));
            AppendRow(new Label { Content = "Left Margin" },
                CreateBoundTextBox(pageObject.Style, "MarginLeft"));
            AppendRow(new Label { Content = "Right Margin" },
                CreateBoundTextBox(pageObject.Style, "MarginRight"));
        }

        /// <summary>
        /// Builds the user interface for the <see cref="PageObject" /> editor.
        /// </summary>
        protected virtual void BuildPageObjectEditor() {}

        protected PageObject(Interpreter.Objects.IObject objectData, Interpreter.Types.Story storyData) : base(objectData, storyData) {}
    }
}
