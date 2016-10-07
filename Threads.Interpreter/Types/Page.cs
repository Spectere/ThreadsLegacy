using System.Collections.Generic;
using System.Linq;
using Threads.Interpreter.Objects;
using Threads.Interpreter.Schema;

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
        /// The page-specific styles that should be applied to this object.
        /// </summary>
        public PageStyle PageStyle { get; set; }

        /// <summary>
        /// The default page-specific styles that should be applied to this object.
        /// </summary>
        public PageStyle DefaultPageStyle => StaticData.DefaultStyles.DefaultPageStyle;
        
        /// <summary>
        /// The style that should be applied to all objects in this page.
        /// </summary>
        public Style Style { get; set; }

        /// <summary>
        /// The default page style.
        /// </summary>
        public Style DefaultStyle => StaticData.DefaultStyles.Style[typeof(Page)];

        /// <summary>
        /// The <see cref="IObject" />s associated with this page.
        /// </summary>
        public List<IObject> Objects { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Page" /> class.
        /// </summary>
        public Page() {
            Objects = new List<IObject>();
        }

        /// <summary>
        /// Exports this <see cref="Page" /> instance into an XML object.
        /// </summary>
        /// <returns>An XML <see cref="PageType" /> object.</returns>
        internal PageType Export() {
            return new PageType {
                Name = Name,
                Items = Objects.Select(e => e.Export()).ToArray()
            };
        }

        /// <summary>
        /// Returns the name of this <see cref="Page" />.
        /// </summary>
        /// <returns>The name of this <see cref="Page" />.</returns>
        public override string ToString() {
            return Name;
        }
    }
}
