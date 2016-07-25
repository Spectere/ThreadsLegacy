using System;

namespace Threads.Interpreter.Exceptions {
    /// <summary>
    /// Represents the error that occurs if an engine action is taken when a story file is not loaded.
    /// </summary>
    public class PageNotFoundException : Exception {
        /// <summary>
        /// Initializes a new instance of the PageNotFoundException class with a specified page name.
        /// </summary>
        /// <param name="page">The missing page name that caused this exception.</param>
        public PageNotFoundException(string page) : base(page) { }

        /// <summary>
        /// Initializes a new instance of the PageNotFoundException class with a specific page name
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="page">The missing page name that caused this exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the
        /// innerException parameter is not a null reference (Nothing in Visual Basic), the current
        /// exception is raised in a catch block that handles the inner exception.</param>
        public PageNotFoundException(string page, Exception innerException) : base(page, innerException) { }
    }
}
