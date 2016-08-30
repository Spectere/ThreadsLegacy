using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Threads.Editor.Objects;
using Threads.Interpreter.Objects.Page;
using Threads.Marker;

namespace Threads.Editor {
    /// <summary>
    /// Interaction logic for PageObjectList.xaml
    /// </summary>
    public partial class ObjectList {
        public static readonly DependencyProperty PageObjectsProperty = DependencyProperty.Register("PageObjects", typeof(List<PageObject>), typeof(ObjectList));

        /// <summary>
        /// Sets the list of <see cref="PageObject" />s that this <see cref="ObjectList" /> control will display.
        /// </summary>
        public List<PageObject> PageObjects {
            get { return (List<PageObject>)GetValue(PageObjectsProperty); }
            set {
                SetValue(PageObjectsProperty, value);
                Objects?.Items.Refresh();
            }
        }

        /// <summary>
        /// The currently selected <see cref="PageObject" />.
        /// </summary>
        public PageObject SelectedObject => Objects.SelectedItems.Count > 0 ? (PageObject)Objects.SelectedItems[0] : null;

        public delegate void OnNewObject(object sender, PageObject e);
        public delegate void OnSelectionChanged(object sender, PageObject e);

        /// <summary>
        /// Fired when a <see cref="PageObject" /> is created by the Object list.
        /// </summary>
        public event OnNewObject NewObject;

        /// <summary>
        /// Fired when a <see cref="PageObject" /> is selected in the Object list.
        /// </summary>
        public event OnSelectionChanged SelectionChanged;

        public ObjectList() {
            PageObjects = new List<PageObject>();
            InitializeComponent();
            PopulateObjectToolbox();
        }

        /// <summary>
        /// Clears all objects from the list and updates the view accordingly.
        /// </summary>
        public void ClearObjectList() {
            PageObjects = new List<PageObject>();
            Objects.Items.Refresh();
        }

        /// <summary>
        /// Populates the list of available <see cref="EditorObject" />s.
        /// </summary>
        private void PopulateObjectToolbox() {
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
            var newObject = (PageObject)Activator.CreateInstance(objectType);
            newObject.FormattedText = new TextSequence();
            NewObject?.Invoke(this, newObject);
        }

        private void Objects_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            SelectionChanged?.Invoke(sender, SelectedObject);
        }

        private void UpButton_OnClick(object sender, RoutedEventArgs e) {
            var pos = Objects.SelectedIndex;
            if(pos > 0)
                MoveItem(pos, pos - 1);
        }

        private void DownButton_OnClick(object sender, RoutedEventArgs e) {
            var pos = Objects.SelectedIndex;
            if(pos < Objects.Items.Count - 1)
                MoveItem(pos, pos + 1);
        }

        private void MoveItem(int oldIndex, int newIndex) {
            var item = PageObjects[oldIndex];
            PageObjects.RemoveAt(oldIndex);
            PageObjects.Insert(newIndex, item);
            Objects.Items.Refresh();
        }
    }
}
