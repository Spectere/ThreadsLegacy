using System;

namespace Threads.Interpreter.Exceptions {
    /// <summary>
    /// Thrown when the loaded story file contains no page data.
    /// </summary>
    public class NullPagesException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullPagesException" /> class.
        /// </summary>
        public NullPagesException() { }
    }
}
