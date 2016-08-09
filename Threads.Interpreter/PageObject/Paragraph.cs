using Threads.Marker;

namespace Threads.Interpreter.PageObject {
    /// <summary>
    /// A page object representing a paragraph of text.
    /// </summary>
    public class Paragraph : IPageObject {
        public PageObjectType Type => PageObjectType.Paragraph;
        public string Text => FormattedText.ToString();

        /// <summary>
        /// The formatted text sequence for this <see cref="Paragraph" />.
        /// </summary>
        public TextSequence FormattedText { get; set; }
    }
}
