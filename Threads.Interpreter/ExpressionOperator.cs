namespace Threads.Interpreter {
    internal class ExpressionOperator : IExpressionNode {
        /// <summary>
        /// A list of supported operators.
        /// </summary>
        internal enum Operator {
            Add,
            Subtract,
            Multiply,
            Divide,
            ConditionalAnd,
            ConditionalOr,
            EqualTo,
            NotEqualTo,
            GreaterThan,
            LessThan,
            GreaterThanOrEqualTo,
            LessThanOrEqualTo
        }

        /// <summary>
        /// The operation that this <see cref="ExpressionOperator" /> represents.
        /// </summary>
        internal Operator Operation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionOperator" /> class.
        /// </summary>
        /// <param name="op">The operation that this instance represents.</param>
        internal ExpressionOperator(Operator op) {
            Operation = op;
        }
    }
}
