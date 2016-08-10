using Threads.Marker;

namespace Threads.Interpreter.PageObject {
    /// <summary>
    /// A page object representing a paragraph of text.
    /// </summary>
    public class Paragraph : PageObject {
        public override PageObjectType Type => PageObjectType.Paragraph;
        public override string Text => FormattedText.ToString();

        /// <summary>
        /// The formatted text sequence for this <see cref="Paragraph" />.
        /// </summary>
        public TextSequence FormattedText { get; set; }

        public Paragraph() {
            // Set default style.
            Style.MarginTop = 0.0;
            Style.MarginBottom = 20.0;
        }
    }
}
