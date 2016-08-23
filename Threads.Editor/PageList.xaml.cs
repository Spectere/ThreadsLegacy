using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Page = Threads.Interpreter.Types.Page;

namespace Threads.Editor {
    /// <summary>
    /// Interaction logic for PageList.xaml
    /// </summary>
    public partial class PageList {
        public static readonly DependencyProperty StoryPagesProperty = DependencyProperty.Register("StoryPages", typeof(IEnumerable<Page>), typeof(PageList));

        /// <summary>
        /// Sets the list of pages that this <see cref="PageList" /> control will display.
        /// </summary>
        public IEnumerable<Page> StoryPages {
            get { return (IEnumerable<Page>)GetValue(StoryPagesProperty); }
            set { SetValue(StoryPagesProperty, value); }
        }

        /// <summary>
        /// Returns the currently selected page from this <see cref="PageList" /> control.
        /// </summary>
        public Page SelectedPage => Pages.SelectedItems.Count > 0 ? (Page)Pages.SelectedItems[0] : null;

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
