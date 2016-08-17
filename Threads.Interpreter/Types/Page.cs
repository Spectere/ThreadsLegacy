using System.Collections.Generic;
using System.Linq;
using Threads.Interpreter.Objects.Page;

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
        /// The <see cref="PageObject" />s associated with this page.
        /// </summary>
        public List<PageObject> Objects { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Page" /> class.
        /// </summary>
        public Page() {
            Objects = new List<PageObject>();
        }

        /// <summary>
        /// Exports this <see cref="Page" /> instance into an XML object.
        /// </summary>
        /// <returns>An XML <see cref="Schema.PageType" /> object.</returns>
        internal Schema.PageType Export() {
            return new Schema.PageType {
                Name = Name,
                Items = Objects.Select(e => e.Export()).ToArray()
            };
        }
    }
}
