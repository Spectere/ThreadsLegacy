using Threads.Interpreter.Types;

namespace Threads.Interpreter.Expression {
    /// <summary>
    /// Implements the visitor pattern for the expression parser.
    /// </summary>
    internal class MathVisitor : MathBaseVisitor<Variable> {
        /// <summary>
        /// The <see cref="Data" /> object associated with the loaded story.
        /// </summary>
        internal Data Data { get; set; }
    }
}
