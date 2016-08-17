namespace Threads.Interpreter.Objects.Page {
    /// <summary>
    /// A page object representing a paragraph of text.
    /// </summary>
    public class Paragraph : PageObject {
        public sealed override PageObjectStyle DefaultStyle => new PageObjectStyle {
            MarginTop = 0.0,
            MarginBottom = 20.0
        };

        internal override Schema.PageObject ExportObject() {
            // Paragraphs don't have any special fields to export,
            // so just create a new empty object.
            return new Schema.PageObject();
        }
    }
}
