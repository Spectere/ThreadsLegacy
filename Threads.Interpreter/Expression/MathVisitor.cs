using System;
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

        public override Variable VisitComparison(MathParser.ComparisonContext context) {
            var left = Visit(context.expr(0));
            var right = Visit(context.expr(1));

            switch(context.op.Type) {
                case MathLexer.EQ:
                    return left == right;
                case MathLexer.NEQ:
                    return left != right;
                case MathLexer.GT:
                    return left > right;
                case MathLexer.GTEQ:
                    return left >= right;
                case MathLexer.LT:
                    return left < right;
                case MathLexer.LTEQ:
                    return left <= right;
                default:
                    throw new Exception("Unknown comparison operator.");
            }
        }

        public override Variable VisitConditional(MathParser.ConditionalContext context) {
            var left = Visit(context.expr(0));
            var right = Visit(context.expr(1));

            switch(context.op.Type) {
                case MathLexer.CONDAND:
                    return left && right;
                case MathLexer.CONDOR:
                    return left || right;
                default:
                    throw new Exception("Unknown conditional operator.");
            }
        }

        public override Variable VisitLiteral(MathParser.LiteralContext context) {
            return new Variable(context.literal.Text);
        }

        public override Variable VisitNegate(MathParser.NegateContext context) {
            var value = Visit(context.expr());
            return !value;
        }

        public override Variable VisitParen(MathParser.ParenContext context) {
            return Visit(context.expr());
        }

        public override Variable VisitVariable(MathParser.VariableContext context) {
            return Data.GetVariable(context.variable.Text);
        }
    }
}
