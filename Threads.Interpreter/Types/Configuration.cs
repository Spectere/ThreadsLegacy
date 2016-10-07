using System.Diagnostics.CodeAnalysis;
using Threads.Interpreter.Schema;

namespace Threads.Interpreter.Types {
    /// <summary>
    /// Represents a story's configuration.
    /// </summary>
    public class Configuration {
        /// <summary>
        /// The page-specific styles that should be applied to this object.
        /// </summary>
        public PageStyle PageStyle { get; set; }

        /// <summary>
        /// The default page-specific styles that should be applied to this object.
        /// </summary>
        public PageStyle DefaultPageStyle => StaticData.DefaultStyles.DefaultPageStyle;

        /// <summary>
        /// The default style that all objects in this story should use by default.
        /// </summary>
        public Style Style { get; set; }

        /// <summary>
        /// The default style that Threads uses for all objects.
        /// </summary>
        public Style DefaultStyle => StaticData.DefaultStyles.Style[typeof(Story)];

        /// <summary>
        /// The first page in the story.
        /// </summary>
        public Page FirstPage { get; set; }

        /// <summary>
        /// The global left margin of this story. All <see cref="Objects.Page.PageObject" />s will be moved over by this amount.
        /// </summary>
        public double StoryMarginLeft { get; set; }

        /// <summary>
        /// The global right margin of this story. All <see cref="Objects.Page.PageObject" />s will be moved over by this amount.
        /// </summary>
        public double StoryMarginRight { get; set; }

        /// <summary>
        /// Creates a new <see cref="Configuration" /> object.
        /// </summary>
        public Configuration() {
            PageStyle = new PageStyle();
        }

        /// <summary>
        /// Exports this <see cref="Configuration" /> instance into an XML object.
        /// </summary>
        /// <returns>An XML <see cref="Schema.ConfigurationType" /> object.</returns>
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        internal Schema.ConfigurationType Export() {
            return new Schema.ConfigurationType {
                FirstPage = FirstPage?.Name,
                DefaultStyle = new ConfigurationTypeDefaultStyle()
            };
        }
    }
}
