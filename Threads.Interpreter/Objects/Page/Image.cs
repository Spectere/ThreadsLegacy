namespace Threads.Interpreter.Objects.Page {
    public class Image : PageObject {
        public override PageObjectType Type => PageObjectType.Image;

        /// <summary>
        /// The filename of the image that should be loaded.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Image" /> <see cref="PageObject" />.
        /// </summary>
        public Image() {
            // Set default style.
            Style.MarginTop = 0.0;
            Style.MarginBottom = 20.0;
        }
    }
}
