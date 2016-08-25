using Threads.Editor.Objects.Page;
using EngineObjects = Threads.Interpreter.Objects;

namespace Threads.Editor.Objects {
    /// <summary>
    /// Includes a method to return an appropriate <see cref="EditorObject" /> when a <see cref="PageObject" /> is passed to it.
    /// </summary>
    internal static class EditorObjectFactory {
        /// <summary>
        /// Returns the appropriate <see cref="EditorObject" /> for the <see cref="PageObject" /> that the method receives.
        /// </summary>
        /// <param name="pageObject">The <see cref="PageObject" /> to associate with the created <see cref="EditorObject" />.</param>
        /// <returns>A specific <see cref="EditorObject" /> that is appropriate for the passed <see cref="PageObject" />. If no suitable object exists, null is returned.</returns>
        public static EditorObject Get(EngineObjects.Page.PageObject pageObject) {
            if(pageObject.GetType() == typeof(EngineObjects.Page.Paragraph))
                return new Paragraph(pageObject);
            if(pageObject.GetType() == typeof(EngineObjects.Page.Image))
                return new Image(pageObject);

            return null;
        }
    }
}
