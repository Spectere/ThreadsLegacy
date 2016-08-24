using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Threads.Interpreter.Objects.Page;

namespace Threads.Editor.Objects {
    /// <summary>
    /// The base class that all editor objects are derived from. Editor objects implement the UI and logic necessary to modify story objects.
    /// </summary>
    internal abstract class EditorObject : UserControl {
        public static DependencyProperty PageObjectProperty = DependencyProperty.Register("PageObject", typeof(PageObject), typeof(EditorObject));

        /// <summary>
        /// The designer view that this <see cref="EditorObject" /> should display on a WPF form.
        /// </summary>
        protected Grid DesignerPanel { get; } = new Grid { Margin = new Thickness(5.0) };

        /// <summary>
        /// The friendly name of the object.
        /// </summary>
        public static string ObjectName => "EditorObject";

        /// <summary>
        /// A description of the object's functionality.
        /// </summary>
        public static string Description => "EditorObject";

        /// <summary>
        /// The <see cref="Type" /> of story object that this <see cref="EditorObject" /> handles.
        /// </summary>
        public abstract Type HandledType { get; }

        /// <summary>
        /// The instance of the <see cref="PageObject" /> that this <see cref="EditorObject" /> instance is modifying.
        /// </summary>
        public PageObject ObjectData {
            get { return (PageObject)GetValue(PageObjectProperty); }
            set { SetValue(PageObjectProperty, value); }
        }

        /// <summary>
        /// Configures the <see cref="PageObject" /> that should be modified.
        /// </summary>
        /// <param name="objectData">The <see cref="PageObject" /> that this <see cref="EditorObject" /> should modify.</param>
        protected EditorObject(PageObject objectData) {
            ObjectData = objectData;
        }

        /// <summary>
        /// Adds a row to the <see cref="DesignerPanel" />.
        /// </summary>
        /// <param name="control">The control to add to the panel. This control will span both columns.</param>
        protected void AppendRow(UIElement control) {
            DesignerPanel.RowDefinitions.Add(new RowDefinition());
            var thisRow = DesignerPanel.RowDefinitions.Count - 1;

            Grid.SetColumn(control, 0);
            Grid.SetRow(control, thisRow);
            Grid.SetColumnSpan(control, 2);

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

            if(DesignerPanel.ColumnDefinitions.Count < 2) {
                DesignerPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                DesignerPanel.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        protected TextBox CreateBoundTextBox(object dataContext, string path) {
            var textBox = new TextBox { DataContext = dataContext };
            textBox.SetBinding(TextBox.TextProperty, new Binding(path));
            return textBox;
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
