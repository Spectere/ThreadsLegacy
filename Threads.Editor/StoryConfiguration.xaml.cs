using System.Collections.Generic;
using System.Windows;
using Threads.Interpreter.Types;

namespace Threads.Editor {
    /// <summary>
    /// Interaction logic for StoryConfiguration.xaml
    /// </summary>
    public partial class StoryConfiguration {
        public static DependencyProperty PageListProperty = DependencyProperty.Register("PageList", typeof(ICollection<Page>), typeof(StoryConfiguration));
        public static DependencyProperty StartingPageProperty = DependencyProperty.Register("StartingPage", typeof(Page), typeof(StoryConfiguration));
        public static DependencyProperty MarginLeftProperty = DependencyProperty.Register("MarginLeft", typeof(double), typeof(StoryConfiguration));
        public static DependencyProperty MarginRightProperty = DependencyProperty.Register("MarginRight", typeof(double), typeof(StoryConfiguration));

        /// <summary>
        /// Gets of sets the page list of the story.
        /// </summary>
        public ICollection<Page> PageList {
            get { return (ICollection<Page>)GetValue(PageListProperty); }
            set { SetValue(PageListProperty, value); }
        }

        /// <summary>
        /// Gets or sets the starting page in this <see cref="Story" />.
        /// </summary>
        public Page StartingPage {
            get { return (Page)GetValue(StartingPageProperty); }
            set { SetValue(StartingPageProperty, value); }
        }

        /// <summary>
        /// Gets or sets the left margin in this <see cref="Story" />.
        /// </summary>
        public double MarginLeft {
            get { return (double)GetValue(MarginLeftProperty); }
            set { SetValue(MarginLeftProperty, value); }
        }

        /// <summary>
        /// Gets or sets the right margin in this <see cref="Story" />.
        /// </summary>
        public double MarginRight {
            get { return (double)GetValue(MarginRightProperty); }
            set { SetValue(MarginRightProperty, value); }
        }

        public StoryConfiguration() {
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
