using System.Collections.Generic;
using System.Linq;

namespace Threads.Interpreter.Types {
    /// <summary>
    /// Represents the dynamic data that can change when a story is executed.
    /// </summary>
    public class Data {
        private readonly List<string> _flags = new List<string>();
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
            var v = _variables.First(e => e.Key == flag).Value.Value;

            // If the value is boolean, just return that.
            if(v is bool) return (bool)v;

            // If the value is a number, return true for non-zero values.
            
        }

        /// <summary>
        /// Sets a given flag.
        /// </summary>
        /// <param name="flag">The name of the flag to set.</param>
        public void SetFlag(string flag) {
            if(_flags.Contains(flag)) return;
            _flags.Add(flag);
        }
    }
}
