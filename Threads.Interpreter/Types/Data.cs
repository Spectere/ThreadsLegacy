using System.Collections.Generic;

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
    }
}
