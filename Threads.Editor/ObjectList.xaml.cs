using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Threads.Editor.Objects;
using Threads.Interpreter.Objects;

namespace Threads.Editor {
    /// <summary>
    /// Interaction logic for PageObjectList.xaml
    /// </summary>
    public partial class ObjectList {
        public static readonly DependencyProperty ObjectsProperty = DependencyProperty.Register("Objects", typeof(List<IObject>), typeof(ObjectList));

        /// <summary>
        /// Sets the list of <see cref="IObject" />s that this <see cref="ObjectList" /> control will display.
        /// </summary>
        public List<IObject> Objects {
            get { return (List<IObject>)GetValue(ObjectsProperty); }
            set {
                SetValue(ObjectsProperty, value);
                ObjectListBox?.Items.Refresh();
            }
        }

        /// <summary>
        /// The currently selected <see cref="IObject" />.
        /// </summary>
        public IObject SelectedObject => ObjectListBox.SelectedItems.Count > 0 ? (IObject)ObjectListBox.SelectedItems[0] : null;

        public delegate void OnObjectChanged(object sender, IObject e);

        /// <summary>
        /// Fired when a <see cref="IObject" /> is created by the Object list.
        /// </summary>
        public event OnObjectChanged AddObject;

        /// <summary>
        /// Fired when a <see cref="IObject" /> is deleted from the Object list.
        /// </summary>
        public event OnObjectChanged DeleteObject;

        /// <summary>
        /// Fired when a <see cref="IObject" /> is selected in the Object list.
        /// </summary>
        public event OnObjectChanged SelectionChanged;

        public ObjectList() {
            Objects = new List<IObject>();
            InitializeComponent();
            PopulateObjectToolbox();
        }

        /// <summary>
        /// Clears all objects from the list and updates the view accordingly.
        /// </summary>
        public void ClearObjectList() {
            Objects = new List<IObject>();
            ObjectListBox.Items.Refresh();
        }

        /// <summary>
        /// Refreshes the list of objects.
        /// </summary>
        public void RefreshObjects() {
            ObjectListBox?.Items.Refresh();
        }

        /// <summary>
        /// Populates the list of available <see cref="EditorObject" />s.
        /// </summary>
        private void PopulateObjectToolbox() {
            PopulatePageObjects();

            var newLabel = new Label {
                Content = "Action Objects",
                BorderBrush = Brushes.Silver,
                BorderThickness = new Thickness(0.0, 0.0, 0.0, 1.0),
                Margin = new Thickness(4.0, 12.0, 4.0, 2.0)
            };
            ObjectToolbox.Children.Add(newLabel);

            PopulateActionObjects();
        }

        /// <summary>
        /// Populates the object toolbox with all of the available <see cref="Interpreter.Objects.Action.ActionObject" />s.
        /// </summary>
        private void PopulateActionObjects() {
            var actionObjectList = EditorObjectList.GetActionObjects();
            foreach(var obj in actionObjectList) {
                var newButton = new Button {
                    Name = obj.ObjectName,
                    Content = obj.ObjectName,
                    ToolTip = obj.Description,
                    Style = FindResource("ToolboxButtonStyle") as Style,
                    Tag = obj.HandledType
                };

                newButton.Click += AddObject_Click;

                ObjectToolbox.Children.Add(newButton);
            }
        }

        /// <summary>
        /// Populates the object toolbox with all of the available <see cref="Interpreter.Objects.Page.PageObject" />s.
        /// </summary>
        private void PopulatePageObjects() {
            var pageObjectList = EditorObjectList.GetPageObjects();
            foreach(var obj in pageObjectList) {
                var newButton = new Button {
                    Name = obj.ObjectName,
                    Content = obj.ObjectName,
                    ToolTip = obj.Description,
                    Style = FindResource("ToolboxButtonStyle") as Style,
                    Tag = obj.HandledType
                };

                newButton.Click += AddObject_Click;

                ObjectToolbox.Children.Add(newButton);
            }
        }

        private void AddObject_Click(object sender, RoutedEventArgs e) {
            var thisButton = (Button)sender;
            var objectType = (Type)thisButton.Tag;
            var newObject = (IObject)Activator.CreateInstance(objectType);
            AddObject?.Invoke(this, newObject);
        }

        private void Objects_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            SelectionChanged?.Invoke(sender, SelectedObject);
        }

        private void UpButton_OnClick(object sender, RoutedEventArgs e) {
            var pos = ObjectListBox.SelectedIndex;
            if(pos > 0)
                MoveItem(pos, pos - 1);
        }

        private void DownButton_OnClick(object sender, RoutedEventArgs e) {
            var pos = ObjectListBox.SelectedIndex;
            if(pos < ObjectListBox.Items.Count - 1)
                MoveItem(pos, pos + 1);
        }

        private void MoveItem(int oldIndex, int newIndex) {
            var item = Objects[oldIndex];
            Objects.RemoveAt(oldIndex);
            Objects.Insert(newIndex, item);
            ObjectListBox.Items.Refresh();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e) {
            if(ObjectListBox.SelectedItem == null) return;
            DeleteObject?.Invoke(this, (IObject)ObjectListBox.SelectedItem);
        }
    }
}
