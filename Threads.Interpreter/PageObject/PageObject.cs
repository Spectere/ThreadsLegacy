using Threads.Interpreter.Exceptions;
using Threads.Marker;

namespace Threads.Interpreter.PageObject {
    /// <summary>
    /// A class implementing <see cref="IPageObject" />. This class must be inherited.
    /// </summary>
    public abstract class PageObject : IPageObject {
        /// <summary>
        /// The type of <see cref="PageObject" /> that this instance represents.
        /// </summary>
        public virtual PageObjectType Type { get { throw new InvalidPageObjectException("unknown"); } }

        /// <summary>
        /// The formatted <see cref="TextSequence" /> for this <see cref="PageObject" />.
        /// </summary>
        public virtual TextSequence FormattedText { get; set; }

        /// <summary>
        /// A textual description of this <see cref="PageObject" />.
        /// </summary>
        public virtual string Text => FormattedText.ToString();

        /// <summary>
        /// The style that should be applied to this <see cref="PageObject" />.
        /// </summary>
        public PageObjectStyle Style { get; set; }

        /// <summary>
        /// Initializes a new instance of this <see cref="PageObject"/>.
        /// </summary>
        protected PageObject() {
            Style = new PageObjectStyle();
        }
    }
}
