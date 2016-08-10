using Threads.Interpreter.Exceptions;

namespace Threads.Interpreter.PageObject {
    public abstract class PageObject : IPageObject {
        public virtual PageObjectType Type { get { throw new InvalidPageObjectException("unknown"); } }

        public virtual string Text => null;

        public PageObjectStyle Style { get; set; }

        /// <summary>
        /// Initializes a new instance of this <see cref="PageObject"/>.
        /// </summary>
        protected PageObject() {
            Style = new PageObjectStyle();
        }
    }
}
