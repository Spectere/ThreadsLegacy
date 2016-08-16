namespace Threads.Interpreter.Types {
    /// <summary>
    /// Represents a story's configuration.
    /// </summary>
    public class Configuration {
        /// <summary>
        /// The first page in the story.
        /// </summary>
        public Page FirstPage { get; set; }

        /// <summary>
        /// The global left margin of this story. All <see cref="Objects.Page.PageObject" />s will be moved over by this amount.
        /// </summary>
        public double StoryMarginLeft { get; set; }

        /// <summary>
        /// The global right margin of this story. All <see cref="Objects.Page.PageObject" />s will be moved over by this amount.
        /// </summary>
        public double StoryMarginRight { get; set; }
    }
}
