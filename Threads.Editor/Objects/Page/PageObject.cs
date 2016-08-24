using System.Windows.Controls;

namespace Threads.Editor.Objects.Page {
    internal abstract class PageObject : EditorObject {
        protected sealed override void BuildEditor() {
            // Common properties.
            AppendRow(
                new Label { Content = "Text" },
                new Label { Content = "Blah!" }
            );

            // Additional properties for this specific control.
            BuildPageObjectEditor();

            // Standard PageObject style editor.
            AppendRow(new Separator());

            AppendRow(
                new Label { Content = "Top Margin" },
                new Label { Content = "Blah!" }
            );

            AppendRow(
                new Label { Content = "Bottom Margin" },
                new Label { Content = "Blah!" }
            );

            AppendRow(
                new Label { Content = "Left Margin" },
                new Label { Content = "Blah!" }
            );

            AppendRow(
                new Label { Content = "Right Margin" },
                new Label { Content = "Blah!" }
            );
        }

        /// <summary>
        /// Builds the user interface for the <see cref="PageObject" /> editor.
        /// </summary>
        protected virtual void BuildPageObjectEditor() {}
    }
}
