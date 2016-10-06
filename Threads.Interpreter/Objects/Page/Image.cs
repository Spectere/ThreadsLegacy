using Threads.Interpreter.Types;

namespace Threads.Interpreter.Objects.Page {
    public class Image : PageObject {
        public sealed override Style DefaultStyle => new Style {
            MarginTop = 0.0,
            MarginBottom = 20.0
        };

        /// <summary>
        /// The filename of the image that should be loaded.
        /// </summary>
        public string Source { get; set; }

        internal override Schema.PageObject ExportObject() {
            return new Schema.ImageObject {
                Source = Source,
                Value = FormattedText.MarkupText
            };
        }
    }
}
