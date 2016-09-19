using System;

namespace Threads.Interpreter.Exceptions {
    /// <summary>
    /// Thrown when an expression contains too many left or right parenthesis.
    /// </summary>
    public class UnbalancedParenthesisException : Exception {}
}
