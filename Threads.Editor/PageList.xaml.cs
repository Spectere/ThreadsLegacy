using System.Windows;
using System.Windows.Controls;

namespace Threads.Editor {
    /// <summary>
    /// Interaction logic for PageList.xaml
    /// </summary>
    public partial class PageList {
        public delegate void OnUpdate(object sender, RoutedEventArgs e);

        /// <summary>
        /// Fired when an item is added to the Page list.
        /// </summary>
        public event OnUpdate Add;

        /// <summary>
        /// Fired when an item is deleted from the Page list.
        /// </summary>
        public event OnUpdate Delete;

        public PageList() {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            Add?.Invoke(sender, e);
        }

        private void DelButton_Click(object sender, RoutedEventArgs e) {
            Delete?.Invoke(sender, e);
        }
    }
}
