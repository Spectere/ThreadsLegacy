using Threads.Interpreter.Types;

namespace Threads.Interpreter {
    internal static class Expression {
        /// <summary>
        /// Parses an expression and returns the result of the evaluation.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <param name="data">The <see cref="Data" /> object from the story <see cref="Engine" />.</param>
        /// <returns>The boolean result of the given expression.</returns>
        internal static bool Parse(string expression, Data data) {
            return string.IsNullOrWhiteSpace(expression) || data.GetFlag(expression);
        }
    }
}
