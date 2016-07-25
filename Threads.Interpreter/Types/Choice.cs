namespace Threads.Interpreter.Types {
    /// <summary>
    /// Represents a choice in the story.
    /// </summary>
    public class Choice {
        /// <summary>
        /// The textual description of the choice.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The target page that this choice leads to.
        /// </summary>
        public Page Target { get; set; }

        /// <summary>
        /// The shortcut key that activates this choice.
        /// </summary>
        public char Shortcut { get; set; }
    }
}
