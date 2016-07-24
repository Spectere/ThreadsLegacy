﻿using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Threads.Interpreter.Schema;

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
        public Story Story { get; private set; }

        /// <summary>
        /// A reference to the current page in the story.
        /// </summary>
        public PageType CurrentPage { get; private set; }

        /// <summary>
        /// Loads a story file into the interpreter and initializes the engine
        /// </summary>
        /// <param name="filename">The path to the story file.</param>
        public void Load(string filename) {
            // TODO: Write a proper exception handler.
            var serializer = new XmlSerializer(typeof(Story));

            Story = (Story)serializer.Deserialize(File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read));

            Restart();
        }

        /// <summary>
        /// Restarts the game engine.
        /// </summary>
        public void Restart() {
            if(Story == null) throw new StoryNotLoadedException();
            CurrentPage = Story.Pages.First(e => e.Name == Story.Configuration.FirstPage);
        }

        /// <summary>
        /// Sends the player's choice to the interpreter.
        /// </summary>
        /// <param name="choice">The choice object that the player selected.</param>
        /// <returns>The page that the choice leads to.</returns>
        public PageType SubmitChoice(PageTypeChoice choice) {
            CurrentPage = Story.Pages.First(e => e.Name == choice.Target);
            return CurrentPage;
        }
    }
}