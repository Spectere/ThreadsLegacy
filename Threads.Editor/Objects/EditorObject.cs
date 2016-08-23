using System;
using System.Windows.Controls;
using Threads.Interpreter.Objects.Page;

namespace Threads.Editor.Objects {
    /// <summary>
    /// The base class that all editor objects are derived from. Editor objects implement the UI and logic necessary to modify story objects.
    /// </summary>
    internal abstract class EditorObject : UserControl {
        /// <summary>
        /// The <see cref="StackPanel" /> that this <see cref="EditorObject" /> displays its controls in.
        /// </summary>
        protected StackPanel DesignerPanel;

        /// <summary>
        /// The friendly name of the object.
        /// </summary>
        public abstract string ObjectName { get; }

        /// <summary>
        /// A description of the object's functionality.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// The <see cref="Type" /> of story object that this <see cref="EditorObject" /> handles.
        /// </summary>
        public abstract Type HandledType { get; }

        /// <summary>
        /// The instance of the <see cref="PageObject" /> that this <see cref="EditorObject" /> instance is modifying.
        /// </summary>
        public PageObject ObjectData { get; set; }

        /// <summary>
        /// Displays the object editor in a <see cref="ContentControl" />.
        /// </summary>
        /// <param name="parent">The parent <see cref="ContentControl" /> that the editor controls should be displayed in.</param>
        public void DisplayControls(ContentControl parent) {
            parent.Content = GetDesignerUI();
        }

        /// <summary>
        /// Displays the object editor in a <see cref="Panel" />.
        /// </summary>
        /// <param name="parent">The parent <see cref="Panel" /> that the editor controls should be displayed in.</param>
        public void DisplayControls(Panel parent) {
            parent.Children.Add(GetDesignerUI());
        }
        
        /// <summary>
        /// Returns the designer UI
        /// </summary>
        /// <returns></returns>
        public abstract StackPanel GetDesignerUI();
    }
}
