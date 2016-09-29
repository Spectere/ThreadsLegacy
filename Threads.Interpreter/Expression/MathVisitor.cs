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

        public override Variable VisitAddSub(MathParser.AddSubContext context) {
            var left = Visit(context.expr(0));
            var right = Visit(context.expr(1));

            switch(context.op.Type) {
                case MathLexer.ADD:
                    return left + right;
                case MathLexer.SUB:
                    return left - right;
                default:
                    throw new Exception("Invalid add/sub operation.");
            }
        }

        public override Variable VisitComparison(MathParser.ComparisonContext context) {
            var left = Visit(context.expr(0));
            var right = Visit(context.expr(1));

            var l = context.expr(0);
            var r = context.expr(1);

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

        public override Variable VisitMulDiv(MathParser.MulDivContext context) {
            var left = Visit(context.expr(0));
            var right = Visit(context.expr(1));

            switch(context.op.Type) {
                case MathLexer.MUL:
                    return left * right;
                case MathLexer.DIV:
                    return left / right;
                case MathLexer.MOD:
                    return left % right;
                default:
                    throw new Exception("Invalid mul/div/mod operation.");
            }
        }

        public override Variable VisitNegate(MathParser.NegateContext context) {
            var value = Visit(context.expr());
            return !value;
        }

        public override Variable VisitParen(MathParser.ParenContext context) {
            return Visit(context.expr());
        }

        public override Variable VisitSign(MathParser.SignContext context) {
            var value = Visit(context.expr());

            switch(context.sign.Type) {
                case MathLexer.ADD:
                    return value;
                case MathLexer.SUB:
                    return -value;
                default:
                    throw new Exception("Unknown sign.");
            }
        }

        public override Variable VisitVariable(MathParser.VariableContext context) {
            return Data.GetVariable(context.variable.Text);
        }
    }
}
