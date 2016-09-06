using System.Collections.Generic;

namespace Threads.Interpreter.Types {
    /// <summary>
    /// Represents the dynamic data that can change when a story is executed.
    /// </summary>
    public class Data {
        private readonly List<string> _flags = new List<string>();

        /// <summary>
        /// Clears the given flag.
        /// </summary>
        /// <param name="flag">The name of the flag to clear.</param>
        public void ClearFlag(string flag) {
            if(!_flags.Contains(flag)) return;
            _flags.Remove(flag);
        }

        /// <summary>
        /// Gets the status of the specified flag.
        /// </summary>
        /// <param name="flag">The name of the flag.</param>
        /// <returns><c>true</c> if the flag is set, <c>false</c> if it is not.</returns>
        public bool GetFlag(string flag) {
            return _flags.Contains(flag);
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
