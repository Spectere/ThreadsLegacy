using System;
using Threads.Interpreter.Types;

namespace Threads.Interpreter.Objects.Action {
    public class Redirect : ActionObject {
        private Story _story;

        /// <summary>
        /// The target <see cref="Types.Page" /> that this redirect should point to.
        /// </summary>
        public Types.Page Target { get; set; }

        /// <summary>
        /// Initializes a new <see cref="Redirect" /> <see cref="ActionObject" />.
        /// </summary>
        /// <param name="story">A reference to the active story.</param>
        public Redirect(Story story) {
            _story = story;
        }

        public override void Activate() {
            throw new NotImplementedException();
        }
    }
}
