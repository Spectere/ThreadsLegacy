using PageObject = Threads.Interpreter.Objects.Page.PageObject;
using Threads.Interpreter.Schema;

namespace Threads.Interpreter.Types {
    /// <summary>
    /// Represents a defined style.
    /// </summary>
    public class Style {
        private Style _inherits;

        /// <summary>
        /// The name of this <see cref="Style" />.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The <see cref="Style" /> that this style inherits from, if any.
        /// </summary>
        public Style Inherits {
            get { return _inherits; }
            set {
                _inherits = value;
                InheritsName = _inherits.Name;
            }
        }

        /// <summary>
        /// The name of the <see cref="Style" /> that this style inherits from, if applicable.
        /// </summary>
        public string InheritsName { get; set; }

        public double? MarginBottom { get; set; }
        public double? MarginLeft { get; set; }
        public double? MarginRight { get; set; }
        public double? MarginTop { get; set; }

        /// <summary>
        /// Evaluates the final <see cref="Style" /> that the interpreter should use when drawing a particular <see cref="PageObject" />.
        /// </summary>
        /// <param name="story">The active <see cref="Story" /> object.</param>
        /// <param name="page">The current <see cref="Page" /> object.</param>
        /// <param name="obj">The <see cref="PageObject" /> that should be drawn.</param>
        /// <returns></returns>
        internal static Style CalculateStyle(Story story, Page page, PageObject obj) {
            var result = new Style();

            // 1) Default object style.
            ProcessStyleInheritance(obj.DefaultStyle, ref result);

            // 2) Default story style.
            ProcessStyleInheritance(story.Configuration.Style, ref result);

            // 3) Page styles.
            ProcessStyleInheritance(page.Style, ref result);

            // 4) The object style.
            ProcessStyleInheritance(obj.Style, ref result);

            return result;
        }

        /// <summary>
        /// Exports this <see cref="Style" /> instance into an XML object.
        /// </summary>
        /// <returns>An XML <see cref="StyleType" /> object.</returns>
        internal StyleType Export() {
            var newStyle = new StyleType {
                Name = Name,
                Inherits = Inherits?.Name,

                MarginBottomSpecified = MarginBottom.HasValue,
                MarginLeftSpecified = MarginLeft.HasValue,
                MarginRightSpecified = MarginRight.HasValue,
                MarginTopSpecified = MarginTop.HasValue
            };

            if(MarginBottom.HasValue) newStyle.MarginBottom = MarginBottom.Value;
            if(MarginLeft.HasValue) newStyle.MarginLeft = MarginLeft.Value;
            if(MarginRight.HasValue) newStyle.MarginRight = MarginRight.Value;
            if(MarginTop.HasValue) newStyle.MarginTop = MarginTop.Value;

            return newStyle;
        }

        private static void OverlayStyles(Style overlay, ref Style baseStyle) {
            if(overlay == null) return;

            if(overlay.MarginBottom.HasValue) baseStyle.MarginBottom = overlay.MarginBottom;
            if(overlay.MarginLeft.HasValue) baseStyle.MarginLeft = overlay.MarginLeft;
            if(overlay.MarginRight.HasValue) baseStyle.MarginRight = overlay.MarginRight;
            if(overlay.MarginTop.HasValue) baseStyle.MarginTop = overlay.MarginTop;
        }

        private static void ProcessStyleInheritance(Style overlay, ref Style baseStyle) {
            if(overlay == null) return;
            if(overlay.Inherits != null) ProcessStyleInheritance(overlay.Inherits, ref baseStyle);
            OverlayStyles(overlay, ref baseStyle);
        }
    }
}
