using System;
using Threads.Interpreter.Schema;

namespace Threads.Interpreter.Objects.Action {
    public class Variable : ActionObject {
        /// <summary>
        /// The expression that this <see cref="ActionObject" /> should evaluate when it is activated.
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// Determines how the <see cref="Types.Variable" /> should be maniuplated when this <see cref="ActionObject" /> is activated.
        /// </summary>
        public VariableAction Operation { get; set; }

        /// <summary>
        /// A set of actions that the <see cref="Variable" /> <see cref="ActionObject" /> can perform on a given <see cref="Variable" />.
        /// </summary>
        public enum VariableAction {
            Set,
            Add,
            Subtract,
            Multiply,
            Divide,
            Modulus
        }

        public override void Activate() {
            var data = Engine.Story.Data;
            var oldVar = data.GetVariable(Name);
            var eval = ExpressionHandler.EvaluateNumeric(Expression, data);
            switch(Operation) {
                case VariableAction.Set:
                    data.SetVariable(Name, eval);
                    break;
                case VariableAction.Add:
                    data.SetVariable(Name, oldVar + eval);
                    break;
                case VariableAction.Subtract:
                    data.SetVariable(Name, oldVar - eval);
                    break;
                case VariableAction.Multiply:
                    data.SetVariable(Name, oldVar * eval);
                    break;
                case VariableAction.Divide:
                    data.SetVariable(Name, oldVar / eval);
                    break;
                case VariableAction.Modulus:
                    data.SetVariable(Name, oldVar % eval);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal override Schema.ActionObject ExportObject() {
            var setting = VariableObjectOperation.set;
            var settingSpecified = true;
            switch(Operation) {
                case VariableAction.Set:
                    setting = VariableObjectOperation.set;
                    break;
                case VariableAction.Add:
                    setting = VariableObjectOperation.add;
                    break;
                case VariableAction.Subtract:
                    setting = VariableObjectOperation.subtract;
                    break;
                case VariableAction.Multiply:
                    setting = VariableObjectOperation.multiply;
                    break;
                case VariableAction.Divide:
                    setting = VariableObjectOperation.divide;
                    break;
                case VariableAction.Modulus:
                    setting = VariableObjectOperation.modulus;
                    break;
                default:
                    settingSpecified = false;
                    break;
            }

            return new VariableObject {
                Expression = Expression,
                Operation = setting,
                OperationSpecified = settingSpecified
            };
        }

        public override string ToString() {
            var baseName = base.ToString();
            return $"{baseName} [{Operation}]: {Expression}";
        }
    }
}
