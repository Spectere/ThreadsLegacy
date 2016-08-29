using System;

namespace Threads.Editor.Exceptions {
    public class PageAlreadyExistsException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageAlreadyExistsException" /> class with a specified page name.
        /// </summary>
        /// <param name="pageName">The name of the duplicate <see cref="Interpreter.Types.Page" />.</param>
        public PageAlreadyExistsException(string pageName) : base(pageName) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageAlreadyExistsException" /> class with a specified page name
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="pageName">The name of the duplicate <see cref="Interpreter.Types.Page" />.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the
        /// innerException parameter is not a null reference (Nothing in Visual Basic), the current
        /// exception is raised in a catch block that handles the inner exception.</param>
        public PageAlreadyExistsException(string pageName, Exception innerException) : base(pageName, innerException) { }
    }
}
