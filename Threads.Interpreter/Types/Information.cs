namespace Threads.Interpreter.Types {
    /// <summary>
    /// The information block of the loaded story.
    /// </summary>
    public class Information {
        /// <summary>
        /// The name of the story.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The author of the story.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The revision of the story file.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The story author's web site.
        /// </summary>
        public string Website { get; set; }
    }
}
