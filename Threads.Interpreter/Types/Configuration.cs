using System.Diagnostics.CodeAnalysis;

namespace Threads.Interpreter.Types {
    /// <summary>
    /// Represents a story's configuration.
    /// </summary>
    public class Configuration {
        public const double DefaultStoryMarginLeft = 40.0;
        public const double DefaultStoryMarginRight = 40.0;

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
            StoryMarginLeft = DefaultStoryMarginLeft;
            StoryMarginRight = DefaultStoryMarginRight;
        }

        /// <summary>
        /// Exports this <see cref="Configuration" /> instance into an XML object.
        /// </summary>
        /// <returns>An XML <see cref="Schema.ConfigurationType" /> object.</returns>
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        internal Schema.ConfigurationType Export() {
            return new Schema.ConfigurationType {
                FirstPage = FirstPage.Name,
                StoryMarginLeftSpecified = StoryMarginLeft != DefaultStoryMarginLeft,
                StoryMarginLeft = StoryMarginLeft,
                StoryMarginRightSpecified = StoryMarginRight != DefaultStoryMarginRight,
                StoryMarginRight = StoryMarginRight
            };
        }
    }
}
