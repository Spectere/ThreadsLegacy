using Threads.Marker;

namespace Threads.Interpreter.Types {
    /// <summary>
    /// Represents a choice in the story.
    /// </summary>
    public class Choice {
        /// <summary>
        /// The formatted text description of the choice.
        /// </summary>
        public TextSequence FormattedText { get; set; }

        /// <summary>
        /// The plain text description of the choice.
        /// </summary>
        public string Text => FormattedText.ToString();

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
