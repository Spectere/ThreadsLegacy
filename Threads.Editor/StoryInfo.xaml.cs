using System.Windows;

namespace Threads.Editor {
    /// <summary>
    /// Interaction logic for StoryInfo.xaml
    /// </summary>
    public partial class StoryInfo {
        public static DependencyProperty StoryTitleProperty = DependencyProperty.Register("StoryTitle", typeof(string), typeof(StoryInfo));
        public static DependencyProperty AuthorProperty = DependencyProperty.Register("Author", typeof(string), typeof(StoryInfo));
        public static DependencyProperty VersionProperty = DependencyProperty.Register("Version", typeof(string), typeof(StoryInfo));
        public static DependencyProperty WebsiteProperty = DependencyProperty.Register("Website", typeof(string), typeof(StoryInfo));

        /// <summary>
        /// The title of the loaded story.
        /// </summary>
        public string StoryTitle {
            get { return (string)GetValue(StoryTitleProperty); }
            set { SetValue(StoryTitleProperty, value); }
        }

        /// <summary>
        /// The author of the loaded story.
        /// </summary>
        public string Author {
            get { return (string)GetValue(AuthorProperty); }
            set { SetValue(AuthorProperty, value); }
        }

        /// <summary>
        /// The version number of the loaded story.
        /// </summary>
        public string Version {
            get { return (string)GetValue(VersionProperty); }
            set { SetValue(VersionProperty, value); }
        }

        /// <summary>
        /// The web site for the story's author.
        /// </summary>
        public string Website {
            get { return (string)GetValue(WebsiteProperty); }
            set { SetValue(WebsiteProperty, value); }
        }

        public StoryInfo() {
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
