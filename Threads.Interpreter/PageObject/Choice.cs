using Threads.Interpreter.Types;
using Threads.Marker;

namespace Threads.Interpreter.PageObject {
    /// <summary>
    /// A page object representing a choice in the story.
    /// </summary>
    public class Choice : IPageObject {
        public PageObjectType Type => PageObjectType.Choice;

        public string Text => FormattedText.ToString();

        /// <summary>
        /// The formatted text sequence for this <see cref="Choice" />.
        /// </summary>
        public TextSequence FormattedText { get; set; }

        /// <summary>
        /// The target page that this choice leads to.
        /// </summary>
        public Page Target { get; set; }

        /// <summary>
        /// The name of the target page that this choice leads to.
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// The shortcut key that activates this choice.
        /// </summary>
        public char Shortcut { get; set; }
    }
}
