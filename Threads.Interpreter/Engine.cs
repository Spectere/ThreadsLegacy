using System.Collections.Generic;
using StorySerializer = Threads.Interpreter.Schema.Story;
using System.IO;
using System.Xml.Serialization;
using Threads.Interpreter.Exceptions;
using Threads.Interpreter.Objects;
using Threads.Interpreter.Objects.Action;
using Threads.Interpreter.Objects.Page;
using Threads.Interpreter.Types;

namespace Threads.Interpreter {
    /// <summary>
    /// Represents a Threads story.
    /// </summary>
    public class Engine {
        /// <summary>
        /// The story file version that this interpreter is designed to read.
        /// </summary>
        public const int EngineVersion = 3;

        /// <summary>
        /// A raw view of the data included in the loaded story file.
        /// </summary>
        private StorySerializer _story;

        /// <summary>
        /// The currently active page in the story.
        /// </summary>
        public Page CurrentPage { get; internal set; }

        public List<IObject> DisplayList { get; }

        /// <summary>
        /// The validated form of the story file.
        /// </summary>
        public Story Story { get; private set; }

        /// <summary>
        /// Initializes a new engine. This creates a new <see cref="Story" />.
        /// </summary>
        public Engine() {
            DisplayList = new List<IObject>();
            Story = new Story();
        }

        /// <summary>
        /// Initializes a new engine. This creates a new <see cref="Story" /> object based on a saved file.
        /// </summary>
        /// <param name="filename">The path to the story file.</param>
        public Engine(string filename) {
            DisplayList = new List<IObject>();
            Load(filename);
        }

        /// <summary>
        /// Loads a story file into the interpreter and initializes the engine
        /// </summary>
        /// <param name="filename">The path to the story file.</param>
        public void Load(string filename) {
            // TODO: Write a proper exception handler.
            var serializer = new XmlSerializer(typeof(StorySerializer));

            var storyFile = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            _story = (StorySerializer)serializer.Deserialize(storyFile);
            Story = Transform.TransformStory(_story, this);
            storyFile.Close();

            Restart();
        }

        /// <summary>
        /// Processes all of the <see cref="Objects.Action.ActionObject" />s on the page and builds a display list for the player.
        /// </summary>
        private void ProcessPage() {
            DisplayList.Clear();
            var oldPage = CurrentPage;
            var idx = 0;

            while(true) {
                if(oldPage.Objects.Count <= idx) break;
                var thisObject = oldPage.Objects[idx++];

                // Evaluate the expressions (hide takes precidence).
                if(!Expression.Parse(thisObject.ShowIf, Story.Data)) continue;
                if(!string.IsNullOrWhiteSpace(thisObject.HideIf) && Expression.Parse(thisObject.HideIf, Story.Data)) continue;

                if(thisObject.GetType().BaseType == typeof(PageObject)) {
                    DisplayList.Add(thisObject);
                    continue;
                }

                ((ActionObject)thisObject).Activate();
                if(CurrentPage == oldPage) continue;

                // Page changed; process controls from the top.
                idx = 0;
                oldPage = CurrentPage;
            }
        }

        /// <summary>
        /// Restarts the game engine.
        /// </summary>
        public void Restart() {
            if(_story == null) throw new StoryNotLoadedException();
            CurrentPage = Story.Configuration.FirstPage;
            Story.Data = new Data();
            ProcessPage();
        }

        /// <summary>
        /// Saves the active story into a given file.
        /// </summary>
        /// <param name="filename">The path to save the story to.</param>
        public void Save(string filename) {
            // TODO: Write a proper exception handler.
            var serializer = new XmlSerializer(typeof(StorySerializer));

            using(var outputStream = new MemoryStream()) {
                serializer.Serialize(outputStream, Story.Export());

                outputStream.Position = 0;
                using(var fileStream = File.Open(filename, FileMode.Create))
                    outputStream.CopyTo(fileStream);
            }
        }

        /// <summary>
        /// Sends the player's choice to the interpreter.
        /// </summary>
        /// <param name="choice">The choice object that the player selected.</param>
        public void SubmitChoice(Choice choice) {
            CurrentPage = choice.Target;
            ProcessPage();
        }
    }
}
