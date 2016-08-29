using System.Windows;

namespace Threads.Editor {
    /// <summary>
    /// Interaction logic for NameEntry.xaml
    /// </summary>
    public partial class NameEntry {
        public static DependencyProperty EnteredNameProperty = DependencyProperty.Register("Name", typeof(string), typeof(NameEntry));

        /// <summary>
        /// Gets or sets the name in the input box.
        /// </summary>
        public string EnteredName {
            get { return (string)GetValue(EnteredNameProperty); }
            set { SetValue(EnteredNameProperty, value); }
        }

        public NameEntry() {
            InitializeComponent();
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e) {
            DialogResult = true;
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e) {
            DialogResult = false;
            Close();
        }
    }
}
