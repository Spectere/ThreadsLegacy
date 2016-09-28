using Threads.Editor.Objects.Action;
using Threads.Editor.Objects.Page;
using Threads.Interpreter.Objects;
using EngineObjects = Threads.Interpreter.Objects;

namespace Threads.Editor.Objects {
    /// <summary>
    /// Includes a method to return an appropriate <see cref="EditorObject" /> when a <see cref="PageObject" /> is passed to it.
    /// </summary>
    internal static class EditorObjectFactory {
        /// <summary>
        /// Returns the appropriate <see cref="EditorObject" /> for the <see cref="PageObject" /> that the method receives.
        /// </summary>
        /// <param name="storyObject">The <see cref="IObject" /> to associate with the created <see cref="EditorObject" />.</param>
        /// <param name="storyData">The <see cref="Interpreter.Types.Story" /> that this object exists in.</param>
        /// <returns>A specific <see cref="EditorObject" /> that is appropriate for the passed <see cref="PageObject" />. If no suitable object exists, null is returned.</returns>
        public static EditorObject Get(IObject storyObject, Interpreter.Types.Story storyData) {
            // Page Objects
            if(storyObject.GetType() == typeof(EngineObjects.Page.Paragraph))
                return new Paragraph(storyObject, storyData);
            if(storyObject.GetType() == typeof(EngineObjects.Page.Image))
                return new Image(storyObject, storyData);
            if(storyObject.GetType() == typeof(EngineObjects.Page.Choice))
                return new Choice(storyObject, storyData);

            // Action Objects
            if(storyObject.GetType() == typeof(EngineObjects.Action.Flag))
                return new Flag(storyObject, storyData);
            if(storyObject.GetType() == typeof(EngineObjects.Action.Redirect))
                return new Redirect(storyObject, storyData);
            if(storyObject.GetType() == typeof(EngineObjects.Action.Variable))
                return new Variable(storyObject, storyData);

            return null;
        }
    }
}
