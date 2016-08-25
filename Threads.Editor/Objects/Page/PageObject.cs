using System.Windows;
using System.Windows.Controls;

namespace Threads.Editor.Objects.Page {
    internal abstract class PageObject : EditorObject {
        protected sealed override void BuildEditor() {
            // Common properties.
            AppendRow(
                new Label { Content = "Text" },
                new TextBox { Text = ObjectData.FormattedText.MarkupText,
                    Height = 120,
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                    VerticalScrollBarVisibility = ScrollBarVisibility.Auto
                }
            );

            // Additional properties for this specific control.
            BuildPageObjectEditor();

            // Standard PageObject style editor.
            AppendRow(new Separator());
            AppendRow(new Label { Content = "Top Margin" },
                CreateBoundTextBox(ObjectData.Style, "MarginTop"));
            AppendRow(new Label { Content = "Bottom Margin" },
                CreateBoundTextBox(ObjectData.Style, "MarginBottom"));
            AppendRow(new Label { Content = "Left Margin" },
                CreateBoundTextBox(ObjectData.Style, "MarginLeft"));
            AppendRow(new Label { Content = "Right Margin" },
                CreateBoundTextBox(ObjectData.Style, "MarginRight"));
        }

        /// <summary>
        /// Builds the user interface for the <see cref="PageObject" /> editor.
        /// </summary>
        protected virtual void BuildPageObjectEditor() {}

        protected PageObject(Interpreter.Objects.Page.PageObject objectData) : base(objectData) {}
    }
}
