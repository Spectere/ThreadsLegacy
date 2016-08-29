using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using Threads.Editor.Objects;
using Threads.Interpreter;
using Threads.Interpreter.Objects.Page;
using Threads.Interpreter.Types;
using Page = Threads.Interpreter.Types.Page;

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

        /// <summary>
        /// Removes a story page.
        /// </summary>
        /// <param name="pageToRemove">The instance of the page to remove.</param>
        private void DeletePage(Page pageToRemove) {
            // TODO: Prompt before anything actually happens.
            if(pageToRemove == null) return;

            // Find a suitable replacement page (usually the first page, unless that's the one getting deleted).
            var replacementPage = _engine.Story.Pages.FirstOrDefault(page => page != pageToRemove);
            var nukeChoices = replacementPage == null;

            // Evaluate all PageObjects and remove Choice references to the deleted page.
            foreach(var page in _engine.Story.Pages) {
                foreach(var pageObject in page.Objects.Where(o => o.GetType() == typeof(Choice))) {
                    var obj = (Choice)pageObject;
                    if(obj.Target != pageToRemove) continue;

                    // Uh oh, we have a match. If we have a replacement page, use that. Otherwise, delete the choice.
                    // TODO: (No, seriously, prompt before deleting stuff. That's just common courtesy.)
                    if(!nukeChoices)
                        obj.Target = replacementPage;
                    else
                        page.Objects.Remove(obj);
                }
            }

            // Replace the default first page if necessary.
            if(_engine.Story.Configuration.FirstPage == pageToRemove)
                _engine.Story.Configuration.FirstPage = replacementPage;

            // Remove the selected page from the page list.
            _engine.Story.Pages.Remove(pageToRemove);

            // Refresh the list.
            UpdatePageList();
        }

        private void New_OnClick(object sender, RoutedEventArgs e) {
            _engine = new Engine();
            Title = "Threads Editor - [Untitled.xml]";
            _filename = null;

            UpdatePageList();
            UpdateObjectList();
            UpdateObjectEditor();
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
            UpdateObjectList();
            UpdateObjectEditor();
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
            if(ObjectList.SelectedObject == null) {
                ObjectEditor.CurrentEditor = null;
                return;
            }

            ObjectEditor.CurrentEditor = EditorObjectFactory.Get(ObjectList.SelectedObject, _engine.Story);
        }

        private void UpdateObjectList() {
            if(PageList.SelectedPage == null) {
                ObjectList.ClearObjectList();
                return;
            }
            ObjectList.PageObjects = PageList.SelectedPage.Objects;
        }

        private void PageList_OnAdd(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void PageList_OnDelete(object sender, RoutedEventArgs e) {
            DeletePage(PageList.SelectedPage);
        }

        private void PageList_OnSelectionChanged(object sender, Page e) {
            UpdateObjectList();
        }

        private void ObjectList_OnSelectionChanged(object sender, PageObject e) {
            UpdateObjectEditor();
        }
    }
}
