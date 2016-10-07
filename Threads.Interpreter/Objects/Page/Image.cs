using Threads.Interpreter.StaticData;
using Threads.Interpreter.Types;

namespace Threads.Interpreter.Objects.Page {
    public class Image : PageObject {
        public sealed override Style DefaultStyle => DefaultStyles.Style[typeof(Image)];

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
