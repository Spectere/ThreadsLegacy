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

        internal bool EvaluateBoolean(string expression) {
            var lexer = new MathLexer(new AntlrInputStream(expression));
            var parser = new MathParser(new CommonTokenStream(lexer));

            return true;
        }
    }
}
