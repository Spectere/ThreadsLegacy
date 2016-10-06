using Threads.Interpreter.Types;

namespace Threads.Interpreter.Objects.Page {
    /// <summary>
    /// A page object representing a paragraph of text.
    /// </summary>
    public class Paragraph : PageObject {
        public sealed override Style DefaultStyle => new Style {
            MarginTop = 0.0,
            MarginBottom = 20.0
        };

        internal override Schema.PageObject ExportObject() {
            return new Schema.ParagraphObject();
        }
    }
}
