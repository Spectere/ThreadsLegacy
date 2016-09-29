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

        public override Variable VisitBoolean(MathParser.BooleanContext context) {
            switch(context.truth.Text) {
                case "true":
                    return new Variable(true);
                case "false":
                    return new Variable(false);
                default:
                    throw new Exception("Invalid boolean value.");  // FileNotFound?
            }
        }

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

        public override Variable VisitNumber(MathParser.NumberContext context) {
            return new Variable(context.number.Text);
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

        public override Variable VisitString(MathParser.StringContext context) {
            var text = context.@string.Text;
            text = text.Substring(1, text.Length - 2)
                       .Replace("\\\"", "\"")
                       .Replace("\\'", "'");
            return new Variable(text);
        }

        public override Variable VisitVariable(MathParser.VariableContext context) {
            return Data.GetVariable(context.variable.Text);
        }
    }
}
