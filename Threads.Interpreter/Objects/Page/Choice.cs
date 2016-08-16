﻿namespace Threads.Interpreter.Objects.Page {
    /// <summary>
    /// A page object representing a choice in the story.
    /// </summary>
    public class Choice : PageObject {
        /// <summary>
        /// The target page that this choice leads to.
        /// </summary>
        public Types.Page Target { get; set; }

        /// <summary>
        /// The name of the target page that this choice leads to.
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// The shortcut key that activates this choice.
        /// </summary>
        public char Shortcut { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Choice" /> <see cref="PageObject" />.
        /// </summary>
        public Choice() {
            // Set default style.
            Style.MarginTop = 0.0;
            Style.MarginBottom = 7.5;
        }
    }
}
