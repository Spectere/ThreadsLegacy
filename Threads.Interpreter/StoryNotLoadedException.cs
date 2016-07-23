using System;

namespace Threads.Interpreter {
    /// <summary>
    /// Represents the error that occurs if an engine action is taken when a story file is not loaded.
    /// </summary>
    public class StoryNotLoadedException : Exception {
        /// <summary>
        /// Initializes a new instance of the StoryNotLoadedException class.
        /// </summary>
        public StoryNotLoadedException() { }

        /// <summary>
        /// Initializes a new instance of the StoryNotLoadedException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public StoryNotLoadedException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the StoryNotLoadedException class with a specific error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the
        /// innerException parameter is not a null reference (Nothing in Visual Basic), the current
        /// exception is raised in a catch block that handles the inner exception.</param>
        public StoryNotLoadedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
