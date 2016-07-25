using StorySerializer = Threads.Interpreter.Schema.Story;
using Threads.Interpreter.Types;

namespace Threads.Interpreter {
    internal static class Transform {
        /// <summary>
        /// Transforms a deserialized XML into a validated set of story objects.
        /// </summary>
        /// <param name="storyData">A set of deserialized story data.</param>
        /// <returns>A <see cref="Story"/> object containing the validated story data.</returns>
        internal static Story TransformStory(StorySerializer storyData) {
            return null;
        }
    }
}
