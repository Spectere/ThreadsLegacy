namespace Threads.Editor.Objects.Action {
    internal abstract class ActionObject : EditorObject {
        protected ActionObject(Interpreter.Objects.IObject objectData, Interpreter.Types.Story storyData) : base(objectData, storyData) { }

        protected override void BuildEditor() {
            BuildActionObjectEditor();
        }

        /// <summary>
        /// Builds the user interface for the <see cref="ActionObject" /> editor.
        /// </summary>
        protected virtual void BuildActionObjectEditor() { }
    }
}
