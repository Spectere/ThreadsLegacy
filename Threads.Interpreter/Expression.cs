using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Threads.Interpreter.Exceptions;
using Threads.Interpreter.Types;

namespace Threads.Interpreter {
    internal static class Expression {
        private static readonly List<string> Delimiters = new List<string> { "(", ")", "!", "&", "|" };

        /// <summary>
        /// Parses an expression and returns the result of the evaluation.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <param name="data">The <see cref="Data" /> object from the story <see cref="Engine" />.</param>
        /// <returns>The boolean result of the given expression.</returns>
        internal static bool Parse(string expression, Data data) {
            if(string.IsNullOrWhiteSpace(expression)) return true;

            // Sanity check: make sure the number of open parens matches the number of closed ones.
            if(expression.Count(l => l == '(') != expression.Count(r => r == ')'))
                throw new UnbalancedParenthesisException();

            // Remove spaces from expression.
            var work = expression.Replace(" ", "");
            work = EvaluateVariables(work, data);

            return EvaluateBoolean(work);
        }

        /// <summary>
        /// Evaluates an expression and returns the results of the evaluation.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <param name="data">The <see cref="Data" /> object from the story <see cref="Engine" />.</param>
        /// <returns>The result of the given expression.</returns>
        internal static Variable Evaluate(string expression, Data data) {
            if(string.IsNullOrWhiteSpace(expression)) return new Variable(true);

            // Sanity check: make sure the number of open parens matches the number of closed ones.
            if(expression.Count(l => l == '(') != expression.Count(r => r == ')'))
                throw new UnbalancedParenthesisException();

            // Remove spaces from expression.
            var work = expression.Replace(" ", "");
        }

        private static bool EvaluateBoolean(string expression) {
            var work = expression;

            while(work.Contains('(')) {
                Console.WriteLine(work);
                var startChar = -1;
                for(var i = 0; i < work.Length; i++) {
                    if(work[i] == '(') {
                        startChar = i;
                        continue;
                    }

                    if(work[i] != ')' || startChar == -1) continue;

                    var eval = EvaluateSubExpression(work.Substring(startChar + 1, i - startChar - 1));
                    work = work.Substring(0, startChar) + (eval ? "1" : "0") + work.Substring(i + 1);
                    break;
                }
            }

            Console.WriteLine(work);
            return EvaluateSubExpression(work);
        }

        private static bool EvaluateSubExpression(string expression) {
            Console.WriteLine("SubExpIn: " + expression);
            var tokens = SplitExpression(expression);

            var negateNext = false;
            var currentOperator = "";
            bool? lastValue = null;
            foreach(var s in tokens) {
                if(string.IsNullOrWhiteSpace(s)) continue;

                // ReSharper disable once SwitchStatementMissingSomeCases
                switch(s) {
                    case "!":
                        negateNext = true;
                        continue;
                    case "|":
                    case "&":
                        if(negateNext || !lastValue.HasValue) throw new InvalidOperatorException();
                        currentOperator = s;
                        continue;
                }

                int value;
                if(!int.TryParse(s, out value)) continue;

                var truth = value > 0;
                truth = negateNext ? !truth : truth;
                negateNext = false;
                if(lastValue.HasValue) {
                    switch(currentOperator) {
                        case "|":
                            lastValue = lastValue.Value || truth;
                            currentOperator = "";
                            break;
                        case "&":
                            lastValue = lastValue.Value && truth;
                            currentOperator = "";
                            break;
                        default:
                            throw new NotImplementedException("Operator not implemented.");
                    }
                } else {
                    lastValue = truth;
                }
            }

            return lastValue ?? true;
        }

        private static string EvaluateVariables(string expression, Data data) {
            var sb = new StringBuilder();
            var tokens = SplitExpression(expression);
            foreach(var s in tokens) {
                if(string.IsNullOrWhiteSpace(s)) continue;
                if(Delimiters.Contains(s)) {
                    sb.Append(s);
                    continue;
                }
                sb.Append(data.GetFlag(s) ? 1 : 0);
            }
            return sb.ToString();
        }

        private static IEnumerable<string> SplitExpression(string expression) {
            var pattern = string.Format($"({string.Join("|", Delimiters.Select(Regex.Escape).ToArray())})");
            return Regex.Split(expression, pattern);
        }
    }
}
