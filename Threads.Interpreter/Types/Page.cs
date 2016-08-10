using System.Collections.Generic;
using Threads.Interpreter.PageObject;
using Threads.Marker;

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
        /// The plain text on this page.
        /// </summary>
        public List<IPageObject> Objects { get; set; }

        public Page() {
            Objects = new List<IPageObject>();
        }
    }
}
