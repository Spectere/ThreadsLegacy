using System;

namespace Threads.Interpreter.Exceptions {
    /// <summary>
    /// Thrown when an expression contains too many left or right parenthesis.
    /// </summary>
    public class UnbalancedParenthesisException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnbalancedParenthesisException" /> class.
        /// </summary>
        public UnbalancedParenthesisException() {}
    }
}
