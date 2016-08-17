namespace Threads.Interpreter.Objects.Page {
    /// <summary>
    /// A page object representing a choice in the story.
    /// </summary>
    public class Choice : PageObject {
        public sealed override PageObjectStyle DefaultStyle => new PageObjectStyle {
            MarginTop = 0.0,
            MarginBottom = 7.5
        };

        /// <summary>
        /// The target page that this choice leads to.
        /// </summary>
        public Types.Page Target { get; set; }

        /// <summary>
        /// The name of the target page that this choice leads to.
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// The shortcut key that activates this choice.
        /// </summary>
        public char? Shortcut { get; set; }

        internal override Schema.PageObject ExportObject() {
            return new Schema.ChoiceObject {
                Target = TargetName,
                Shortcut = Shortcut.ToString()
            };
        }
    }
}
