using Threads.Editor.Objects;

namespace Threads.Editor {
    /// <summary>
    /// Interaction logic for ObjectEditor.xaml
    /// </summary>
    public partial class ObjectEditor {
        private EditorObject _thisEditor;

        public delegate void OnNameChange(object sender, string name);

        /// <summary>
        /// Fired when the name of the object changes.
        /// </summary>
        public event OnNameChange NameChange;

        /// <summary>
        /// Sets the active <see cref="EditorObject" /> that this control should be editing.
        /// </summary>
        internal EditorObject CurrentEditor {
            get { return _thisEditor; }
            set {
                ObjectProperties.Children.Clear();

                _thisEditor = value;
                _thisEditor?.DisplayControls(ObjectProperties);
                if(_thisEditor != null) _thisEditor.NameChange += (sender, name) => NameChange?.Invoke(sender, name);
                ObjectEditorScroll.ScrollToTop();
            }
        }

        public ObjectEditor() {
            InitializeComponent();
        }
    }
}
