namespace Threads.Interpreter.PageObject {
    /// <summary>
    /// Styling that should be applied to a given page object.
    /// </summary>
    public class PageObjectStyle {
        /// <summary>
        /// The top margin of this <see cref="PageObject" />.
        /// </summary>
        public double MarginTop { get; set; }

        /// <summary>
        /// The bottom margin of this <see cref="PageObject" />.
        /// </summary>
        public double MarginBottom { get; set; }
    }
}
