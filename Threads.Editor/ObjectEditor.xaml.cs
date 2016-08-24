using Threads.Editor.Objects;

namespace Threads.Editor {
    /// <summary>
    /// Interaction logic for ObjectEditor.xaml
    /// </summary>
    public partial class ObjectEditor {
        private EditorObject _thisEditor;

        /// <summary>
        /// Sets the active <see cref="EditorObject" /> that this control should be editing.
        /// </summary>
        internal EditorObject CurrentEditor {
            get { return _thisEditor; }
            set {
                _thisEditor = value;
                if(_thisEditor == null) return;

                ObjectProperties.Children.Clear();
                _thisEditor.DisplayControls(ObjectProperties);
            }
        }

        public ObjectEditor() {
            InitializeComponent();
        }
    }
}
