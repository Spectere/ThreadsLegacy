using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Threads.Interpreter.Objects;
using Threads.Interpreter.Types;

namespace Threads.Editor.Objects {
    /// <summary>
    /// The base class that all editor objects are derived from. Editor objects implement the UI and logic necessary to modify story objects.
    /// </summary>
    internal abstract class EditorObject : UserControl {
        /// <summary>
        /// A <see cref="DependencyProperty" /> that contains the loaded <see cref="Story" /> object.
        /// This is used for objects that need information about the story as a whole.
        /// </summary>
        public static DependencyProperty StoryProperty = DependencyProperty.Register("GameState", typeof(Story), typeof(EditorObject));

        /// <summary>
        /// A <see cref="DependencyProperty" /> that contains the current <see cref="IObject" /> information.
        /// This is used for objects that only need to access data relating to the selected object.
        /// </summary>
        public static DependencyProperty ObjectProperty = DependencyProperty.Register("Object", typeof(IObject), typeof(EditorObject));

        /// <summary>
        /// The designer view that this <see cref="EditorObject" /> should display on a WPF form.
        /// </summary>
        protected Grid DesignerPanel { get; } = new Grid { Margin = new Thickness(5.0) };

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
        /// The instance of the <see cref="IObject" /> that this <see cref="EditorObject" /> instance is modifying.
        /// </summary>
        public IObject ObjectData {
            get { return (IObject)GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }

        /// <summary>
        /// The instance of the <see cref="Story" /> that this <see cref="EditorObject" /> is part of.
        /// </summary>
        public Story StoryData {
            get { return (Story)GetValue(StoryProperty); }
            set { SetValue(StoryProperty, value); }
        }

        /// <summary>
        /// Configures the <see cref="IObject" /> that should be modified.
        /// </summary>
        /// <param name="objectData">The <see cref="IObject" /> that this <see cref="EditorObject" /> should modify.</param>
        /// <param name="storyData">The <see cref="Story" /> that this <see cref="EditorObject" /> can access.</param>
        protected EditorObject(IObject objectData, Story storyData) {
            ObjectData = objectData;
            StoryData = storyData;
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

            AppendRow(new Label { Content = "Name" }, CreateBoundTextBox(ObjectData, "Name"));
        }

        /// <summary>
        /// Creates a <see cref="TextBox" /> that binds to a <see cref="DependencyObject" />.
        /// </summary>
        /// <param name="dataContext">A <see cref="DependencyObject" /> to bind to.</param>
        /// <param name="path">The property to bind to.</param>
        /// <returns>A <see cref="TextBox" /> with the requested bindings.</returns>
        protected TextBox CreateBoundTextBox(object dataContext, string path) {
            var textBox = new TextBox { DataContext = dataContext };
            textBox.SetBinding(TextBox.TextProperty, new Binding(path) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            return textBox;
        }

        /// <summary>
        /// Modifies an existing <see cref="TextBox" /> and binds it to a <see cref="DependencyObject" />.
        /// </summary>
        /// <param name="dataContext">A <see cref="DependencyObject" /> to bind the <see cref="TextBox" /> to.</param>
        /// <param name="path">The property to bind to.</param>
        /// <param name="textBox">The <see cref="TextBox" /> to bind the property to.</param>
        protected void CreateBoundTextBox(object dataContext, string path, ref TextBox textBox) {
            textBox.DataContext = dataContext;
            textBox.SetBinding(TextBox.TextProperty, new Binding(path) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
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
