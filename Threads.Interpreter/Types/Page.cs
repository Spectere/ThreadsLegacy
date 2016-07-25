using System.Collections.Generic;

namespace Threads.Interpreter.Types {
    /// <summary>
    /// Represents a page in a story.
    /// </summary>
    public class Page {
        /// <summary>
        /// The name of this page.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The text on this page.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The available choices on this page.
        /// </summary>
        public List<Choice> Choices { get; set; }
    }
}
