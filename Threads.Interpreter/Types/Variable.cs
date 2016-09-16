using System;
using System.Diagnostics.CodeAnalysis;

namespace Threads.Interpreter.Types {
    /// <summary>
    /// Represents a variable in a Threads story.
    /// </summary>
    [SuppressMessage("ReSharper", "PossibleInvalidCastException")]
    public class Variable {
        private enum Operation {
            Add,
            Subtract,
            Multiply,
            Divide,
            Modulus,
            Increment,
            Decrement
        }

        public object Value { get; }

        public static implicit operator Variable(bool obj) { return new Variable(obj); }
        public static implicit operator Variable(long obj) { return new Variable(obj); }
        public static implicit operator Variable(decimal obj) { return new Variable(obj); }
        public static implicit operator Variable(double obj) { return new Variable(obj); }

        public static Variable operator +(Variable a, Variable b) { return Arithmetic(Operation.Add, a, b); }
        public static Variable operator -(Variable a, Variable b) { return Arithmetic(Operation.Subtract, a, b); }
        public static Variable operator *(Variable a, Variable b) { return Arithmetic(Operation.Multiply, a, b); }
        public static Variable operator /(Variable a, Variable b) { return Arithmetic(Operation.Divide, a, b); }
        public static Variable operator ++(Variable a) { return Arithmetic(Operation.Add, a, 1); }
        public static Variable operator --(Variable a) { return Arithmetic(Operation.Subtract, a, 1); }

        /// <summary>
        /// Initializes a <see cref="Variable" /> with no value.
        /// </summary>
        public Variable() { Value = null; }

        /// <summary>
        /// Initializes a <see cref="Variable" /> with a <see cref="bool" /> value.
        /// </summary>
        public Variable(bool value) { Value = value; }

        /// <summary>
        /// Initializes a <see cref="Variable" /> with a <see cref="long" /> value.
        /// </summary>
        public Variable(long value) { Value = value; }

        /// <summary>
        /// Initializes a <see cref="Variable" /> with a <see cref="decimal" /> value.
        /// </summary>
        public Variable(decimal value) { Value = DetermineBestType(value); }

        /// <summary>
        /// Initializes a <see cref="Variable" /> with a <see cref="double" /> value.
        /// </summary>
        public Variable(double value) { Value = DetermineBestType(value); }

        private static Variable Arithmetic(Operation op, Variable var1, Variable var2) {
            return Arithmetic(op, var1.Value, var2.Value);
        }

        private static Variable Arithmetic(Operation op, object var1, object var2) {
            dynamic v1 = var1;
            dynamic v2 = var2;

            if(v1 == null || v2 == null)
                throw new NullReferenceException();

            // Decimals and Floats don't play well together. Ints/Floats and Int/Decimals both do.
            if(v1 is double && v2 is decimal)
                v2 = (double)v2;
            if(v1 is decimal && v2 is double)
                v1 = (double)v1;

            switch(op) {
                case Operation.Add:
                    return new Variable(v1 + v2);
                case Operation.Subtract:
                    return new Variable(v1 - v2);
                case Operation.Multiply:
                    return new Variable(v1 * v2);
                case Operation.Divide:
                    if(v1 is long && v2 is long)
                        v1 = (decimal)v1;
                    return new Variable(v1 / v2);
                case Operation.Modulus:
                    if(v1 is long && v2 is long)
                        v1 = (decimal)v1;
                    return new Variable(v1 % v2);
                case Operation.Increment:
                    return new Variable(v1 + 1);
                case Operation.Decrement:
                    return new Variable(v1 + 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(op), op, null);
            }
        }

        private static object DetermineBestType(object obj) {
            if(obj is decimal)
                return (decimal)obj % 1 == 0 ? TryIntegerConversion(obj) : obj;
            if(obj is double)
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                return (double)obj % 1 == 0 ? TryIntegerConversion(obj) : obj;

            return obj;
        }

        public override string ToString() {
            return Value.ToString();
        }

        private static object TryIntegerConversion(object obj) {
            try {
                return Convert.ToInt64(obj);
            } catch(OverflowException) {
                try {
                    return Convert.ToDecimal(obj);
                } catch(OverflowException) {
                    return Convert.ToDouble(obj);
                }
            }
        }
    }
}
