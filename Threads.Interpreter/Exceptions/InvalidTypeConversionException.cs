using System;

namespace Threads.Interpreter.Exceptions {
    /// <summary>
    /// Thrown when an invalid operation for a given type is performed on a <see cref="Types.Variable" />.
    /// </summary>
    public class InvalidTypeConversion : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTypeConversion" /> class.
        /// </summary>
        public InvalidTypeConversion() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTypeConversion" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public InvalidTypeConversion(string message) : base(message) { }
    }
}
