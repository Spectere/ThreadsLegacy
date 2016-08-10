using Threads.Interpreter.Types;
using Threads.Marker;

namespace Threads.Interpreter.PageObject {
    public interface IPageObject {
        /// <summary>
        /// The type of page object.
        /// </summary>
        PageObjectType Type { get; }

        /// <summary>
        /// The formatted text sequence for this <see cref="IPageObject" />.
        /// </summary>
        TextSequence FormattedText { get; set; }

        /// <summary>
        /// A textual description of this <see cref="IPageObject" />.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// The style that should be applied to this <see cref="IPageObject" />.
        /// </summary>
        PageObjectStyle Style { get; set; }
    }
}
