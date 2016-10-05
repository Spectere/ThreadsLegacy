using Threads.Interpreter.Schema;

namespace Threads.Interpreter.Types {
    /// <summary>
    /// Represents a defined style.
    /// </summary>
    public class Style {
        public string Name { get; set; }
        public Style Inherits { get; set; }

        public double? MarginBottom { get; set; }
        public double? MarginLeft { get; set; }
        public double? MarginRight { get; set; }
        public double? MarginTop { get; set; }

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
    }
}
