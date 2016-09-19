using System;
using System.Diagnostics.CodeAnalysis;
using Threads.Interpreter.Exceptions;

namespace Threads.Interpreter.Types {
    /// <summary>
    /// Represents a variable in a Threads story.
    /// </summary>
    [SuppressMessage("ReSharper", "PossibleInvalidCastException")]
    public class Variable {
        private enum ArithmeticOperation {
            Add,
            Subtract,
            Multiply,
            Divide,
            Modulus,
            Increment,
            Decrement
        }

        private enum ComparisonOperation {
            Equality,
            Inequality,
            GreaterThan,
            LessThan,
            GreaterThanOrEqualTo,
            LessThanOrEqualTo
        }

        public object Value { get; }

        public static implicit operator Variable(bool obj) { return new Variable(obj); }
        public static implicit operator Variable(long obj) { return new Variable(obj); }
        public static implicit operator Variable(decimal obj) { return new Variable(obj); }
        public static implicit operator Variable(double obj) { return new Variable(obj); }

        public static Variable operator +(Variable a, Variable b) { return Arithmetic(ArithmeticOperation.Add, a, b); }
        public static Variable operator -(Variable a, Variable b) { return Arithmetic(ArithmeticOperation.Subtract, a, b); }
        public static Variable operator *(Variable a, Variable b) { return Arithmetic(ArithmeticOperation.Multiply, a, b); }
        public static Variable operator /(Variable a, Variable b) { return Arithmetic(ArithmeticOperation.Divide, a, b); }
        public static Variable operator ++(Variable a) { return Arithmetic(ArithmeticOperation.Add, a, 1); }
        public static Variable operator --(Variable a) { return Arithmetic(ArithmeticOperation.Subtract, a, 1); }

        public static bool operator ==(Variable a, Variable b) { return Compare(ComparisonOperation.Equality, a, b); }
        public static bool operator !=(Variable a, Variable b) { return Compare(ComparisonOperation.Inequality, a, b); }
        public static bool operator >(Variable a, Variable b) { return Compare(ComparisonOperation.GreaterThan, a, b); }
        public static bool operator <(Variable a, Variable b) { return Compare(ComparisonOperation.LessThan, a, b); }
        public static bool operator <=(Variable a, Variable b) { return Compare(ComparisonOperation.GreaterThanOrEqualTo, a, b); }
        public static bool operator >=(Variable a, Variable b) { return Compare(ComparisonOperation.LessThanOrEqualTo, a, b); }

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

        private static Variable Arithmetic(ArithmeticOperation op, Variable var1, Variable var2) {
            return Arithmetic(op, var1.Value, var2.Value);
        }

        private static Variable Arithmetic(ArithmeticOperation op, object var1, object var2) {
            dynamic v1 = var1;
            dynamic v2 = var2;

            if(v1 == null || v2 == null)
                throw new NullReferenceException();

            // Throw exception for bools.
            if(v1 is bool || v2 is bool)
                throw new InvalidVariableOperationException($"Attempted to perform an {op} operation on a boolean value.");

            // Match up decimal/double types.
            TypeMatch(ref v1, ref v2);

            switch(op) {
                case ArithmeticOperation.Add:
                    return new Variable(v1 + v2);
                case ArithmeticOperation.Subtract:
                    return new Variable(v1 - v2);
                case ArithmeticOperation.Multiply:
                    return new Variable(v1 * v2);
                case ArithmeticOperation.Divide:
                    if(v1 is long && v2 is long)
                        v1 = (decimal)v1;
                    return new Variable(v1 / v2);
                case ArithmeticOperation.Modulus:
                    if(v1 is long && v2 is long)
                        v1 = (decimal)v1;
                    return new Variable(v1 % v2);
                case ArithmeticOperation.Increment:
                    return new Variable(v1 + 1);
                case ArithmeticOperation.Decrement:
                    return new Variable(v1 + 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(op), op, null);
            }
        }

        private static bool Compare(ComparisonOperation op, Variable var1, Variable var2) {
            return Compare(op, var1.Value, var2.Value);
        }

        private static bool Compare(ComparisonOperation op, object var1, object var2) {
            dynamic v1 = var1;
            dynamic v2 = var2;

            if(v1 is bool && v2 is bool)  // Handle boolean comparison in a separate method.
                return CompareBoolean(op, (bool)v1, (bool)v2);

            // Comparing booleans and other types is invalid.
            if(v1 is bool || v2 is bool)
                throw new InvalidVariableOperationException($"Attempted to compare a {v1.GetType().ToString()} with a {v2.GetType().ToString()}.");

            // Match up decimal/double types.
            TypeMatch(ref v1, ref v2);

            switch(op) {
                case ComparisonOperation.Equality:
                    return v1 == v2;
                case ComparisonOperation.Inequality:
                    return v1 != v2;
                case ComparisonOperation.GreaterThan:
                    return v1 > v2;
                case ComparisonOperation.LessThan:
                    return v1 < v2;
                case ComparisonOperation.GreaterThanOrEqualTo:
                    return v1 >= v2;
                case ComparisonOperation.LessThanOrEqualTo:
                    return v1 <= v2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(op), op, null);
            }
        }

        private static bool CompareBoolean(ComparisonOperation op, bool var1, bool var2) {
            switch(op) {
                case ComparisonOperation.Equality:
                    return var1 == var2;
                case ComparisonOperation.Inequality:
                    return var1 != var2;
                default:
                    throw new InvalidVariableOperationException($"Attempted to perform an {op} operation on a boolean value.");
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

        protected bool Equals(Variable other) {
            return Compare(ComparisonOperation.Equality, this, other);
        }

        public override bool Equals(object obj) {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;
            if(obj.GetType() != GetType()) return false;
            return Equals((Variable)obj);
        }

        public override int GetHashCode() {
            return Value?.GetHashCode() ?? 0;
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
                    // If this doesn't work, we're basically screwed.
                    return Convert.ToDouble(obj);
                }
            }
        }

        private static void TypeMatch(ref dynamic var1, ref dynamic var2) {
            // Decimals and Floats don't play well together. Ints/Floats and Int/Decimals both do.
            if(var1 is double && var2 is decimal)
                var2 = (double)var2;
            if(var1 is decimal && var2 is double)
                var1 = (double)var1;
        }
    }
}
