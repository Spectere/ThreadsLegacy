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

        private enum BitwiseOperation {
            And,
            Or
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

        public static implicit operator bool(Variable obj) { return obj != 0; }
        public static implicit operator decimal(Variable obj) { return GetDecimal(obj); }
        public static implicit operator double(Variable obj) { return GetDouble(obj); }
        public static implicit operator long(Variable obj) { return GetLong(obj); }
        public static implicit operator string(Variable obj) { return obj.ToString(); }

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

        public static Variable operator &(Variable a, Variable b) { return Bitwise(BitwiseOperation.And, a, b); }
        public static Variable operator |(Variable a, Variable b) { return Bitwise(BitwiseOperation.Or, a, b); }

        public static bool operator true(Variable a) { return a != 0; }
        public static bool operator false(Variable a) { return a == 0; }

        /// <summary>
        /// Initializes a <see cref="Variable" /> with no value.
        /// </summary>
        public Variable() { Value = null; }

        /// <summary>
        /// Initializes a <see cref="Variable" /> with a <see cref="bool" /> value.
        /// </summary>
        /// <param name="value">The value to initialize this <see cref="Variable" /> to.</param>
        public Variable(bool value) { Value = value; }

        /// <summary>
        /// Initializes a <see cref="Variable" /> with a <see cref="long" /> value.
        /// </summary>
        /// <param name="value">The value to initialize this <see cref="Variable" /> to.</param>
        public Variable(long value) { Value = value; }

        /// <summary>
        /// Initializes a <see cref="Variable" /> with a <see cref="decimal" /> value.
        /// </summary>
        /// <param name="value">The value to initialize this <see cref="Variable" /> to.</param>
        public Variable(decimal value) { Value = DetermineBestType(value); }

        /// <summary>
        /// Initializes a <see cref="Variable" /> with a <see cref="double" /> value.
        /// </summary>
        /// <param name="value">The value to initialize this <see cref="Variable" /> to.</param>
        public Variable(double value) { Value = DetermineBestType(value); }

        /// <summary>
        /// Initializes a <see cref="Variable" /> with a <see cref="string" /> value. The value specified is converted to the most appropriate format.
        /// </summary>
        /// <param name="value">The value to initialize this <see cref="Variable" /> to.</param>
        public Variable(string value) {
            // Try to convert it to an integer.
            long tryLong;
            if(long.TryParse(value, out tryLong)) {
                Value = tryLong;
                return;
            }

            // Decimal next.
            decimal tryDecimal;
            if(decimal.TryParse(value, out tryDecimal)) {
                Value = tryDecimal;
                return;
            }

            // Now a double.
            double tryDouble;
            if(double.TryParse(value, out tryDouble)) {
                Value = tryDouble;
                return;
            }

            // And, finally, a string.
            Value = value;
        }

        private static Variable Arithmetic(ArithmeticOperation op, Variable var1, Variable var2) {
            return Arithmetic(op, var1?.Value, var2?.Value);
        }

        private static Variable Arithmetic(ArithmeticOperation op, object var1, object var2) {
            dynamic v1 = var1;
            dynamic v2 = var2;

            if(v1 == null || v2 == null)
                throw new NullReferenceException();

            // Throw exception for bools.
            if(v1 is bool || v2 is bool)
                throw new InvalidVariableOperationException($"Attempted to perform a(n) {op} operation on a boolean value.");

            // Only addition (concatenation) can be performed on strings.
            if((v1 is string || v2 is string) && op != ArithmeticOperation.Add)
                throw new InvalidVariableOperationException($"Attempted to perform a(n) {op} operation on a string value.");

            // Match up decimal/double types.
            TypeMatch(ref v1, ref v2);

            // Convert longs to decimal/double before doing any operations.
            if(v1 is long && v2 is double) v1 = Convert.ToDouble(v1);
            else if(v2 is long && v1 is double) v2 = Convert.ToDouble(v2);
            else if(v1 is long && v2 is decimal) v1 = Convert.ToDecimal(v1);
            else if(v2 is long && v1 is decimal) v2 = Convert.ToDecimal(v2);
            else if(v1 is long && v2 is long) {
                v1 = Convert.ToDecimal(v1);
                v2 = Convert.ToDecimal(v2);
            }

            switch(op) {
                case ArithmeticOperation.Add:
                    try {
                        return new Variable(checked(v1 + v2));
                    } catch(OverflowException) {
                        return new Variable(Convert.ToDouble(v1) + Convert.ToDouble(v2));
                    }
                case ArithmeticOperation.Subtract:
                    try {
                        return new Variable(checked(v1 - v2));
                    } catch(OverflowException) {
                        return new Variable(Convert.ToDouble(v1) - Convert.ToDouble(v2));
                    }
                case ArithmeticOperation.Multiply:
                    try {
                        return new Variable(checked(v1 * v2));
                    } catch(OverflowException) {
                        return new Variable(Convert.ToDouble(v1) * Convert.ToDouble(v2));
                    }
                case ArithmeticOperation.Divide:
                    if(v1 is long && v2 is long)
                        v1 = (decimal)v1;

                    try {
                        return new Variable(checked(v1 / v2));
                    } catch(OverflowException) {
                        return new Variable(Convert.ToDouble(v1) / Convert.ToDouble(v2));

                    }
                case ArithmeticOperation.Modulus:
                    try {
                        return new Variable(checked(v1 / v2));
                    } catch(OverflowException) {
                        return new Variable(Convert.ToDouble(v1) % Convert.ToDouble(v2));

                    }
                case ArithmeticOperation.Increment:
                    try {
                        return new Variable(checked(v1 + 1));
                    } catch(OverflowException) {
                        return new Variable(Convert.ToDouble(v1) + 1);
                    }
                case ArithmeticOperation.Decrement:
                    try {
                        return new Variable(checked(v1 - 1));
                    } catch(OverflowException) {
                        return new Variable(Convert.ToDouble(v1) - 1);
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(op), op, null);
            }
        }

        private static Variable Bitwise(BitwiseOperation op, Variable var1, Variable var2) {
            return Bitwise(op, var1?.Value, var2?.Value);
        }

        private static Variable Bitwise(BitwiseOperation op, object var1, object var2) {
            dynamic v1 = var1;
            dynamic v2 = var2;

            if(v1 is bool && !(v2 is bool))
                v1 = v1 ? 1 : 0;
            if(v2 is bool && !(v1 is bool))
                v2 = v2 ? 1 : 0;

            switch(op) {
                case BitwiseOperation.And:
                    return new Variable(v1 & v2);
                case BitwiseOperation.Or:
                    return new Variable(v1 | v2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(op), op, null);
            }
        }

        private static bool Compare(ComparisonOperation op, Variable var1, Variable var2) {
            return Compare(op, var1?.Value, var2?.Value);
        }

        private static bool Compare(ComparisonOperation op, object var1, object var2) {
            dynamic v1 = var1;
            dynamic v2 = var2;

            if(v1 == null && v2 == null) return op == ComparisonOperation.Equality || op == ComparisonOperation.GreaterThanOrEqualTo || op == ComparisonOperation.LessThanOrEqualTo;
            if(v1 == null || v2 == null) return op == ComparisonOperation.Inequality;

            if(v1 is bool && v2 is bool) // Handle boolean comparison in a separate method.
                return CompareBoolean(op, (bool)v1, (bool)v2);

            // Handle comparisons between boolean and numeric values.
            if(v1 is bool)
                return CompareBoolean(op, (bool)v1, v2 != 0);
            if(v2 is bool)
                return CompareBoolean(op, (bool)v2, v1 != 0);

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
            // ReSharper disable once SwitchStatementMissingSomeCases
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

        private static decimal GetDecimal(Variable var) {
            dynamic value = var.Value;

            if(value is bool) return value ? 1 : 0;
            if(value is long || value is decimal) return value;
            if(value is double) return Convert.ToDecimal(value);
            throw new Exception($"Unsupported type: {value.GetType()}");
        }

        private static double GetDouble(Variable var) {
            dynamic value = var.Value;

            if(value is bool) return value ? 1 : 0;
            if(value is long || value is double) return value;
            if(value is decimal) return Convert.ToDouble(value);
            throw new Exception($"Unsupported type: {value.GetType()}");
        }

        private static long GetLong(Variable var) {
            dynamic value = var.Value;

            if(value is bool) return value ? 1 : 0;
            if(value is long) return value;
            if(value is decimal || value is double) {
                value = Math.Round(value);
                return Convert.ToInt64(value);
            }
            throw new Exception($"Unsupported type: {value.GetType()}");
        }

        public override string ToString() {
            return Value?.ToString() ?? "";
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
