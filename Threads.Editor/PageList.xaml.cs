using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            set {
                SetValue(StoryPagesProperty, value);
                Pages.Items.Refresh();
            }
        }

        /// <summary>
        /// Returns the currently selected page from this <see cref="PageList" /> control.
        /// </summary>
        public Page SelectedPage {
            get { return (Page)Pages.SelectedItem; }
            set { Pages.SelectedItem = value; }
        }

        public delegate void OnUpdate(object sender, RoutedEventArgs e);
        public delegate void OnPageUpdate(object sender, Page e);

        /// <summary>
        /// Fired when the Add button is pressed.
        /// </summary>
        public event OnUpdate Add;

        /// <summary>
        /// Fired when the Delete button is pressed.
        /// </summary>
        public event OnUpdate Delete;

        /// <summary>
        /// Fired when a page is double-clicked.
        /// </summary>
        public event OnPageUpdate PageDoubleClicked;

        /// <summary>
        /// Fired when the selected page is changed.
        /// </summary>
        public event OnPageUpdate SelectionChanged;

        public PageList() {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            Add?.Invoke(sender, e);
        }

        private void DelButton_Click(object sender, RoutedEventArgs e) {
            Delete?.Invoke(sender, e);
        }

        private void Pages_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            SelectionChanged?.Invoke(sender, SelectedPage);
        }

        private void Pages_OnKeyUp(object sender, KeyEventArgs e) {
            switch(e.Key) {
                case Key.Delete:
                    Delete?.Invoke(sender, e);
                    break;
                case Key.Insert:
                    Add?.Invoke(sender, e);
                    break;
            }
        }

        private void Pages_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if(SelectedPage == null || e.ChangedButton != MouseButton.Left) return;
            PageDoubleClicked?.Invoke(sender, SelectedPage);
        }
    }
}
