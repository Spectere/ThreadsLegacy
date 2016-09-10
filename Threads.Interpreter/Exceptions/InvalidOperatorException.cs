using System;

namespace Threads.Interpreter.Exceptions {
    /// <summary>
    /// Thrown when an expression contains invalid operator use.
    /// </summary>
    public class InvalidOperatorException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOperatorException" /> class.
        /// </summary>
        public InvalidOperatorException() {}
    }
}
