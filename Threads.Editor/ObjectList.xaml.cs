using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Threads.Interpreter.Objects.Page;

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
            set { SetValue(PageObjectsProperty, value); }
        }

        public PageObject SelectedObject => Objects.SelectedItems.Count > 0 ? (PageObject)Objects.SelectedItems[0] : null;

        public delegate void OnUpdate(object sender, RoutedEventArgs e);
        public delegate void OnSelectionChanged(object sender, PageObject e);

        /// <summary>
        /// Fired when an item is added to the Object list.
        /// </summary>
        public event OnUpdate Add;

        /// <summary>
        /// Fired when an item is deleted from the Object list.
        /// </summary>
        public event OnUpdate Delete;

        /// <summary>
        /// Fired when a <see cref="PageObject" /> is selected in the Object list.
        /// </summary>
        public event OnSelectionChanged SelectionChanged;

        public ObjectList() {
            InitializeComponent();
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
