using Threads.Interpreter.StaticData;
using Threads.Interpreter.Types;

namespace Threads.Interpreter.Objects.Page {
    /// <summary>
    /// A page object representing a paragraph of text.
    /// </summary>
    public class Paragraph : PageObject {
        public sealed override Style DefaultStyle => DefaultStyles.Style[typeof(Paragraph)];

        internal override Schema.PageObject ExportObject() {
            return new Schema.ParagraphObject();
        }
    }
}
