using Antlr4.Runtime;
using Threads.Interpreter.Expression;
using Threads.Interpreter.Types;

namespace Threads.Interpreter {
    /// <summary>
    /// Uses the ANTLR-generated lexer/parser to determine the final result of a given expression.
    /// </summary>
    internal static class ExpressionHandler {
        /// <summary>
        /// Evaluates an expression and returns a <see cref="Variable" />.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <param name="data">The <see cref="Data" /> object containing all of the story variables.</param>
        /// <returns>A <see cref="Variable" /> instance containing the results of the evaluation.</returns>
        private static Variable Evaluate(string expression, Data data) {
            var lexer = new MathLexer(new AntlrInputStream(expression));
            var parser = new MathParser(new CommonTokenStream(lexer));
            var visitor = new MathVisitor { Data = data };
            return visitor.Visit(parser.expr());
        }

        /// <summary>
        /// Evaluates an expression and returns a boolean value.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <param name="data">The <see cref="Data" /> object containing all of the story variables.</param>
        /// <returns><c>true</c> if the <paramref name="expression" /> is true, otherwise <c>false</c>.</returns>
        internal static bool EvaluateBoolean(string expression, Data data) {
            if(string.IsNullOrWhiteSpace(expression)) return true;
            return Evaluate(expression, data) == true;
        }

        /// <summary>
        /// Evaluates an expression and returns its final value in a <see cref="Variable" />.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <param name="data">The <see cref="Data" /> object containing all of the story variables.</param>
        /// <returns>A <see cref="Variable " /> containing the final result of the calculation.</returns>
        internal static Variable EvaluateNumeric(string expression, Data data) {
            return string.IsNullOrWhiteSpace(expression) ? new Variable(0) : Evaluate(expression, data);
        }
    }
}
