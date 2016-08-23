using System.Diagnostics.CodeAnalysis;
using Threads.Marker;

namespace Threads.Interpreter.Objects.Page {
    /// <summary>
    /// A class implementing <see cref="IPageObject" />. This class must be inherited.
    /// </summary>
    public abstract class PageObject : IPageObject {
        /// <summary>
        /// The formatted <see cref="TextSequence" /> for this <see cref="PageObject" />.
        /// </summary>
        public virtual TextSequence FormattedText { get; set; }

        /// <summary>
        /// A textual description of this <see cref="PageObject" />.
        /// </summary>
        public virtual string Text => FormattedText.ToString();

        /// <summary>
        /// The default style for this <see cref="PageObject" />.
        /// </summary>
        public abstract PageObjectStyle DefaultStyle { get; }

        /// <summary>
        /// The style that should be applied to this <see cref="PageObject" />.
        /// </summary>
        public PageObjectStyle Style { get; set; }

        /// <summary>
        /// Initializes a new instance of this <see cref="PageObject"/>.
        /// </summary>
        protected PageObject() {
            // ReSharper disable once VirtualMemberCallInConstructor
            Style = DefaultStyle;
        }

        /// <summary>
        /// Exports this <see cref="PageObject" /> instance into an XML object.
        /// </summary>
        /// <returns>An XML <see cref="Schema.PageObject" /> object.</returns>
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        internal Schema.PageObject Export() {
            var xmlObject = ExportObject();

            // Export the text in this object.
            xmlObject.Value = FormattedText.OriginalText;

            // Only export style if it doesn't match the default.
            xmlObject.MarginBottomSpecified = Style.MarginBottom != DefaultStyle.MarginBottom;
            xmlObject.MarginLeftSpecified = Style.MarginLeft != DefaultStyle.MarginLeft;
            xmlObject.MarginRightSpecified = Style.MarginRight != DefaultStyle.MarginRight;
            xmlObject.MarginTopSpecified = Style.MarginTop != DefaultStyle.MarginTop;

            // Export the style values.
            xmlObject.MarginBottom = Style.MarginBottom;
            xmlObject.MarginLeft = Style.MarginLeft;
            xmlObject.MarginRight = Style.MarginRight;
            xmlObject.MarginTop = Style.MarginTop;

            return xmlObject;
        }

        /// <summary>
        /// Exports this <see cref="PageObject" /> instance into an XML object. This method must be implemented.
        /// </summary>
        /// <returns>An XML <see cref="Schema.PageObject" /> object.</returns>
        internal abstract Schema.PageObject ExportObject();
    }
}
