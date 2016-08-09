using System.Collections.Generic;
using Threads.Marker;

namespace Threads.Interpreter.Types {
    /// <summary>
    /// Represents a page in a story.
    /// </summary>
    public class Page {
        /// <summary>
        /// The formatted text on this page.
        /// </summary>
        public TextSequence FormattedText { get; set; }

        /// <summary>
        /// The name of this page.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The plain text on this page.
        /// </summary>
        public string Text => FormattedText.ToString();

        /// <summary>
        /// The available choices on this page.
        /// </summary>
        public List<Choice> Choices { get; set; }
    }
}
