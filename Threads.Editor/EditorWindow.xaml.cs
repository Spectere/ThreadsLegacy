using System.Windows;
using Threads.Interpreter;
using Threads.Interpreter.Types;

namespace Threads.Editor {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EditorWindow {
        private Engine _engine = new Engine();

        public EditorWindow() {
            InitializeComponent();
        }

        private void New_OnClick(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void Open_OnClick(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void Save_OnClick(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void SaveAs_OnClick(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void StoryInfo_OnClick(object sender, RoutedEventArgs e) {
            var infoWindow = new StoryInfo {
                StoryTitle = _engine.Story.Information.Name,
                Author = _engine.Story.Information.Author,
                Version = _engine.Story.Information.Version,
                Website = _engine.Story.Information.Website
            };

            var result = infoWindow.ShowDialog();
            if(result == null || !result.Value) return;

            _engine.Story.Information = new Information {
                Name = infoWindow.StoryTitle,
                Author = infoWindow.Author,
                Version = infoWindow.Version,
                Website = infoWindow.Website
            };
        }
    }
}
