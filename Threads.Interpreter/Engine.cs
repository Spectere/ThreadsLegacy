using StorySerializer = Threads.Interpreter.Schema.Story;
using System.IO;
using System.Xml.Serialization;
using Threads.Interpreter.Exceptions;
using Threads.Interpreter.Types;

namespace Threads.Interpreter {
    /// <summary>
    /// Represents a Threads story.
    /// </summary>
    public class Engine {
        /// <summary>
        /// The story file version that this interpreter is designed to read.
        /// </summary>
        public const int EngineVersion = 0;

        /// <summary>
        /// A raw view of the data included in the loaded story file.
        /// </summary>
        private StorySerializer _story;

        public Page CurrentPage { get; private set; }

        /// <summary>
        /// The validated form of the story file.
        /// </summary>
        public Story Story { get; private set; }

        /// <summary>
        /// Loads a story file into the interpreter and initializes the engine
        /// </summary>
        /// <param name="filename">The path to the story file.</param>
        public void Load(string filename) {
            // TODO: Write a proper exception handler.
            var serializer = new XmlSerializer(typeof(StorySerializer));

            _story = (StorySerializer)serializer.Deserialize(File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read));
            Story = Transform.TransformStory(_story);

            Restart();
        }

        /// <summary>
        /// Restarts the game engine.
        /// </summary>
        public void Restart() {
            if(_story == null) throw new StoryNotLoadedException();
            CurrentPage = Story.Configuration.FirstPage;
        }

        /// <summary>
        /// Sends the player's choice to the interpreter.
        /// </summary>
        /// <param name="choice">The choice object that the player selected.</param>
        /// <returns>The page that the choice leads to.</returns>
        public Page SubmitChoice(Choice choice) {
            CurrentPage = choice.Target;
            return CurrentPage;
        }
    }
}
