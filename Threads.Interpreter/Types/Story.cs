using System.Collections.Generic;
using System.Linq;

namespace Threads.Interpreter.Types {
    /// <summary>
    /// Represents a story.
    /// </summary>
    public class Story {
        /// <summary>
        /// The version of the Threads format that this story was written for.
        /// </summary>
        public int Format { get; set; }

        /// <summary>
        /// The story's configuration block.
        /// </summary>
        public Configuration Configuration { get; set; }

        /// <summary>
        /// The story's information block.
        /// </summary>
        public Information Information { get; set; }

        /// <summary>
        /// The styles contained in the story.
        /// </summary>
        public ICollection<Style> Styles { get; set; }

        /// <summary>
        /// The pages contained in the story.
        /// </summary>
        public ICollection<Page> Pages { get; set; }

        /// <summary>
        /// The runtime data storage used by the story.
        /// </summary>
        public Data Data { get; set; }

        /// <summary>
        /// Creates a new <see cref="Story" />.
        /// </summary>
        public Story() {
            Format = Engine.EngineVersion;
            Information = new Information();
            Configuration = new Configuration();
            Styles = new List<Style>();
            Pages = new List<Page>();
            Data = new Data();
        }

        /// <summary>
        /// Exports this <see cref="Story" /> instance into an XML object.
        /// </summary>
        /// <returns>An XML <see cref="Schema.Story" /> object.</returns>
        internal Schema.Story Export() {
            return new Schema.Story {
                Configuration = Configuration.Export(),
                Format = Format.ToString(),
                Information = Information.Export(),
                Styles = Styles.Select(style => style.Export()).ToArray(),
                Pages = Pages.Select(page => page.Export()).ToArray()
            };
        }
    }
}
