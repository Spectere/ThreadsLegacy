namespace Threads.Interpreter.Objects.Page {
    /// <summary>
    /// Styling that should be applied to a given page object.
    /// </summary>
    public class PageObjectStyle {
        /// <summary>
        /// The bottom margin of this <see cref="PageObject" />.
        /// </summary>
        public double MarginBottom { get; set; }

        /// <summary>
        /// The left margin of this <see cref="PageObject" />.
        /// </summary>
        public double MarginLeft { get; set; }

        /// <summary>
        /// The right margin of this <see cref="PageObject" />.
        /// </summary>
        public double MarginRight { get; set; }

        /// <summary>
        /// The top margin of this <see cref="PageObject" />.
        /// </summary>
        public double MarginTop { get; set; }
    }
}
