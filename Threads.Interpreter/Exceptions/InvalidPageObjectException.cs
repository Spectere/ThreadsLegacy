using System;

namespace Threads.Interpreter.Exceptions {
    /// <summary>
    /// Represents the error that occurs if an engine action is taken when a story file is not loaded.
    /// </summary>
    public class InvalidPageObjectException : Exception {
        /// <summary>
        /// Initializes a new instance of the InvalidPageObjectException class with a specified page object type.
        /// </summary>
        /// <param name="type">The invalid object type that caused this exception.</param>
        public InvalidPageObjectException(string type) : base(type) { }

        /// <summary>
        /// Initializes a new instance of the InvalidPageObjectException class with a specified page object type
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="type">The invalid object type that caused this exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the
        /// innerException parameter is not a null reference (Nothing in Visual Basic), the current
        /// exception is raised in a catch block that handles the inner exception.</param>
        public InvalidPageObjectException(string type, Exception innerException) : base(type, innerException) { }
    }
}
