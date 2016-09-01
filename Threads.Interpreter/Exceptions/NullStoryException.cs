using System;

namespace Threads.Interpreter.Exceptions {
    /// <summary>
    /// Thrown when the loaded story file contains no story data.
    /// </summary>
    public class NullStoryException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullStoryException" /> class.
        /// </summary>
        public NullStoryException() { }
    }
}
