using Threads.Interpreter.Types;

namespace Threads.Interpreter.PageObject {
    /// <summary>
    /// A page object representing a choice in the story.
    /// </summary>
    public class Choice : PageObject {
        public override PageObjectType Type => PageObjectType.Choice;

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
