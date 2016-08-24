using Threads.Editor.Objects.Page;
using EngineObjects = Threads.Interpreter.Objects;

namespace Threads.Editor.Objects {
    internal static class EditorObjectFactory {
        public static EditorObject Get(EngineObjects.Page.PageObject pageObject) {
            if(pageObject.GetType() == typeof(EngineObjects.Page.Paragraph)) {
                return new Paragraph(pageObject);
            }

            return null;
        }
    }
}
