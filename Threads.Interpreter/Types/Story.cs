using System.Collections.Generic;

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
        /// The pages contained in the story.
        /// </summary>
        public IEnumerable<Page> Pages { get; set; }

        /// <summary>
        /// Creates a new <see cref="Story" />.
        /// </summary>
        public Story() {
            Format = Engine.EngineVersion;
            Information = new Information();
            Configuration = new Configuration();
            Pages = new List<Page>();
        }
    }
}
