using Antlr4.Runtime;
using Threads.Interpreter.Expression;
using Threads.Interpreter.Types;

namespace Threads.Interpreter {
    /// <summary>
    /// Uses the ANTLR-generated lexer/parser to determine the final result of a given expression.
    /// </summary>
    internal class ExpressionHandler {
        /// <summary>
        /// The <see cref="Data" /> instance that variables and other story data should be pulled from.
        /// </summary>
        internal Data Data { get; set; }

        /// <summary>
        /// Evaluates an expression and returns a <see cref="Variable" />.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <returns>A <see cref="Variable" /> instance containing the results of the evaluation.</returns>
        private Variable Evaluate(string expression) {
            var lexer = new MathLexer(new AntlrInputStream(expression));
            var parser = new MathParser(new CommonTokenStream(lexer));
            var visitor = new MathVisitor { Data = Data };
            return visitor.Visit(parser.expr());
        }

        /// <summary>
        /// Evaluates an expression and returns a boolean value.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <returns><c>true</c> if the <paramref name="expression" /> is true, otherwise <c>false</c>.</returns>
        internal bool EvaluateBoolean(string expression) {
            if(string.IsNullOrWhiteSpace(expression)) return true;
            return Evaluate(expression) == true;
        }

        /// <summary>
        /// Evaluates an expression and returns its final value in a <see cref="Variable" />.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <returns>A <see cref="Variable " /> containing the final result of the calculation.</returns>
        internal Variable EvaluateNumeric(string expression) {
            return string.IsNullOrWhiteSpace(expression) ? new Variable(0) : Evaluate(expression);
        }
    }
}
