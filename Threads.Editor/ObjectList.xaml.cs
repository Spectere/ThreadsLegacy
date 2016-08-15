using System.Windows;

namespace Threads.Editor {
    /// <summary>
    /// Interaction logic for PageObjectList.xaml
    /// </summary>
    public partial class ObjectList {
        public delegate void OnUpdate(object sender, RoutedEventArgs e);

        /// <summary>
        /// Fired when an item is added to the Object list.
        /// </summary>
        public event OnUpdate Add;

        /// <summary>
        /// Fired when an item is deleted from the Object list.
        /// </summary>
        public event OnUpdate Delete;

        public ObjectList() {
            InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e) {
            Add?.Invoke(sender, e);
        }

        private void DelButton_OnClick(object sender, RoutedEventArgs e) {
            Delete?.Invoke(sender, e);
        }
    }
}
