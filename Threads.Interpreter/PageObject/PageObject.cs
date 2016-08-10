﻿using Threads.Interpreter.Exceptions;
using Threads.Marker;

namespace Threads.Interpreter.PageObject {
    public abstract class PageObject : IPageObject {
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
            FormattedText = new TextSequence();
            Style = new PageObjectStyle();
        }
    }
}
