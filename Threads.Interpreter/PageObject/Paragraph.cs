namespace Threads.Interpreter.PageObject {
    /// <summary>
    /// A page object representing a paragraph of text.
    /// </summary>
    public class Paragraph : PageObject {
        public override PageObjectType Type => PageObjectType.Paragraph;

        /// <summary>
        /// Creates a new instance of the <see cref="Paragraph" /> <see cref="PageObject" />.
        /// </summary>
        public Paragraph() {
            // Set default style.
            Style.MarginTop = 0.0;
            Style.MarginBottom = 20.0;
        }
    }
}
