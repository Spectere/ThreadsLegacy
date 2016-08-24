using System.IO;
using System.Windows;
using Microsoft.Win32;
using Threads.Editor.Objects;
using Threads.Interpreter;
using Threads.Interpreter.Objects.Page;
using Threads.Interpreter.Types;

namespace Threads.Editor {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EditorWindow {
        private Engine _engine = new Engine();
        private string _filename;

        public EditorWindow() {
            InitializeComponent();
        }

        private void New_OnClick(object sender, RoutedEventArgs e) {
            _engine = new Engine();
            Title = "Threads Editor - [Untitled.xml]";
            _filename = null;
        }

        private void Open_OnClick(object sender, RoutedEventArgs e) {
            var fileDialog = new OpenFileDialog {
                Filter = "Threads Story (*.XML)|*.xml|All Files (*.*)|*.*",
                Multiselect = false
            };

            var result = fileDialog.ShowDialog() ?? false;
            if(!result) return;

            _filename = fileDialog.FileName;

            // ReSharper disable once AssignNullToNotNullAttribute
            Directory.SetCurrentDirectory(Path.GetDirectoryName(_filename));
            _engine = new Engine(_filename);

            Title = $"Threads Editor - [{Path.GetFileName(_filename)}]";

            UpdatePageList();
        }

        private void Save_OnClick(object sender, RoutedEventArgs e) {
            if(_filename == null)
                SaveAs();

            _engine.Save(_filename);
        }

        private void SaveAs() {
            var fileDialog = new SaveFileDialog {
                Filter = "Threads Story (*.XML)|*.xml|All Files (*.*)|*.*"
            };

            var result = fileDialog.ShowDialog() ?? false;
            if(!result) return;

            _filename = fileDialog.FileName;

            // ReSharper disable once AssignNullToNotNullAttribute
            Directory.SetCurrentDirectory(Path.GetDirectoryName(_filename));
            _engine.Save(_filename);

            Title = $"Threads Editor - [{Path.GetFileName(_filename)}]";
        }

        private void SaveAs_OnClick(object sender, RoutedEventArgs e) {
            SaveAs();
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

        private void UpdatePageList() {
            PageList.StoryPages = _engine.Story.Pages;
        }

        private void UpdateObjectEditor() {
            ObjectEditor.CurrentEditor = ObjectList.SelectedObject != null ? EditorObjectFactory.Get(ObjectList.SelectedObject) : null;
        }

        private void UpdateObjectList() {
            if(PageList.SelectedPage == null) return;
            ObjectList.PageObjects = PageList.SelectedPage.Objects;
        }

        private void PageList_OnSelectionChanged(object sender, Page e) {
            UpdateObjectList();
        }

        private void ObjectList_OnSelectionChanged(object sender, PageObject e) {
            UpdateObjectEditor();
        }
    }
}
