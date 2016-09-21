using System.Collections.Generic;
using System.Linq;

namespace Threads.Interpreter.Types {
    /// <summary>
    /// Represents the dynamic data that can change when a story is executed.
    /// </summary>
    public class Data {
        private readonly Dictionary<string, Variable> _variables = new Dictionary<string, Variable>();

        /// <summary>
        /// Clears the given flag.
        /// </summary>
        /// <param name="flag">The name of the flag to clear.</param>
        public void ClearFlag(string flag) {
            if(!_variables.ContainsKey(flag)) return;
            _variables.Remove(flag);
        }

        /// <summary>
        /// Gets the status of the specified flag.
        /// </summary>
        /// <param name="flag">The name of the flag.</param>
        /// <returns><c>true</c> if the flag is set, <c>false</c> if it is not.</returns>
        public bool GetFlag(string flag) {
            if(!_variables.ContainsKey(flag)) return false;

            // Fetch the variable.
            var v = _variables[flag];

            // If the value is boolean, just return that.
            if(v.Value is bool) return (bool)v.Value;

            // If the value is a number, return true for non-zero values.
            return v != 0;
        }

        /// <summary>
        /// Gets the <see cref="Variable" /> object with the given <paramref name="name" />.
        /// </summary>
        /// <param name="name">The name of the <see cref="Variable" /> to retrieve.</param>
        /// <returns>The requested <see cref="Variable" />. If this variable does not exist, a new <see cref="Variable" /> with a zero value will be returned.</returns>
        public Variable GetVariable(string name) {
            return !_variables.ContainsKey(name) ? new Variable(0L) : _variables.First(n => n.Key == name).Value;
        }

        /// <summary>
        /// Sets a given flag.
        /// </summary>
        /// <param name="flag">The name of the flag to set.</param>
        public void SetFlag(string flag) {
            if(!_variables.ContainsKey(flag)) {
                _variables.Add(flag, new Variable(true));
                return;
            }

            if(_variables[flag] == false) _variables[flag] = true;
        }

        /// <summary>
        /// Sets the given <see cref="Variable" /> to a certain value.
        /// </summary>
        /// <param name="name">The name of the <see cref="Variable" /> to set.</param>
        /// <param name="value">The value to store in the specified <see cref="Variable" />.</param>
        public void SetVariable(string name, decimal value) {
            SetVariable(name, new Variable(value));
        }

        /// <summary>
        /// Sets the given <see cref="Variable" /> to a certain value.
        /// </summary>
        /// <param name="name">The name of the <see cref="Variable" /> to set.</param>
        /// <param name="value">The value to store in the specified <see cref="Variable" />.</param>
        public void SetVariable(string name, double value) {
            SetVariable(name, new Variable(value));
        }

        /// <summary>
        /// Sets the given <see cref="Variable" /> to a certain value.
        /// </summary>
        /// <param name="name">The name of the <see cref="Variable" /> to set.</param>
        /// <param name="value">The value to store in the specified <see cref="Variable" />.</param>
        public void SetVariable(string name, long value) {
            SetVariable(name, new Variable(value));
        }

        /// <summary>
        /// Sets the given <see cref="Variable" /> to a certain value.
        /// </summary>
        /// <param name="name">The name of the <see cref="Variable" /> to set.</param>
        /// <param name="value">A <see cref="Variable" /> to store in the variables collection.</param>
        public void SetVariable(string name, Variable value) {
            if(_variables.ContainsKey(name))
                _variables[name] = value;
            else
                _variables.Add(name, value);
        }
    }
}
