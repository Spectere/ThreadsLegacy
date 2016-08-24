using System;
using System.Windows;
using System.Windows.Controls;
using Threads.Interpreter.Objects.Page;

namespace Threads.Editor.Objects {
    /// <summary>
    /// The base class that all editor objects are derived from. Editor objects implement the UI and logic necessary to modify story objects.
    /// </summary>
    internal abstract class EditorObject : UserControl {
        /// <summary>
        /// The designer view that this <see cref="EditorObject" /> should display on a WPF form.
        /// </summary>
        protected Grid DesignerPanel { get; } = new Grid();

        /// <summary>
        /// The friendly name of the object.
        /// </summary>
        public abstract string ObjectName { get; }

        /// <summary>
        /// A description of the object's functionality.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// The <see cref="Type" /> of story object that this <see cref="EditorObject" /> handles.
        /// </summary>
        public abstract Type HandledType { get; }

        /// <summary>
        /// The instance of the <see cref="PageObject" /> that this <see cref="EditorObject" /> instance is modifying.
        /// </summary>
        public PageObject ObjectData { get; set; }

        /// <summary>
        /// Adds a row to the <see cref="DesignerPanel" />.
        /// </summary>
        /// <param name="control">The control to add to the panel. This control will span both columns.</param>
        protected void AppendRow(UIElement control) {
            DesignerPanel.RowDefinitions.Add(new RowDefinition());
            var thisRow = DesignerPanel.RowDefinitions.Count - 1;

            Grid.SetColumn(control, 0);
            Grid.SetRow(control, thisRow);

            DesignerPanel.Children.Add(control);
        }

        /// <summary>
        /// Adds a row to the <see cref="DesignerPanel" />.
        /// </summary>
        /// <param name="leftControl">The left control in the grid panel.</param>
        /// <param name="rightControl">The right control in the grid panel.</param>
        protected void AppendRow(UIElement leftControl, UIElement rightControl) {
            DesignerPanel.RowDefinitions.Add(new RowDefinition());
            var thisRow = DesignerPanel.RowDefinitions.Count - 1;

            Grid.SetColumn(leftControl, 0);
            Grid.SetRow(leftControl, thisRow);

            Grid.SetColumn(rightControl, 1);
            Grid.SetRow(rightControl, thisRow);

            DesignerPanel.Children.Add(leftControl);
            DesignerPanel.Children.Add(rightControl);
        }

        /// <summary>
        /// Populates this class's <see cref="DesignerPanel" /> with editor controls.
        /// </summary>
        protected abstract void BuildEditor();

        private void CommonSetup() {
            DesignerPanel.Children.Clear();

            DesignerPanel.ColumnDefinitions.Add(new ColumnDefinition());
            DesignerPanel.ColumnDefinitions.Add(new ColumnDefinition());
        }

        /// <summary>
        /// Displays the object editor in a <see cref="ContentControl" />.
        /// </summary>
        /// <param name="parent">The parent <see cref="ContentControl" /> that the editor controls should be displayed in.</param>
        public void DisplayControls(ContentControl parent) {
            CommonSetup();
            BuildEditor();
            parent.Content = DesignerPanel;
        }

        /// <summary>
        /// Displays the object editor in a <see cref="Panel" />.
        /// </summary>
        /// <param name="parent">The parent <see cref="Panel" /> that the editor controls should be displayed in.</param>
        public void DisplayControls(Panel parent) {
            CommonSetup();
            BuildEditor();
            parent.Children.Add(DesignerPanel);
        }
    }
}
