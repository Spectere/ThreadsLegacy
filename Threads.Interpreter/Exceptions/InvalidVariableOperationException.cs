using System;

namespace Threads.Interpreter.Exceptions {
    /// <summary>
    /// Thrown when an invalid operation for a given type is performed on a <see cref="Types.Variable" />.
    /// </summary>
    public class InvalidVariableOperationException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidVariableOperationException" /> class.
        /// </summary>
        public InvalidVariableOperationException() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidVariableOperationException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public InvalidVariableOperationException(string message) : base(message) { }
    }
}
