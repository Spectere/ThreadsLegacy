using System.Collections.Generic;
using System.Text;
using Threads.Marker.Commands;

namespace Threads.Marker {
    /// <summary>
    /// A sequence of text and styles.
    /// </summary>
    public class TextSequence {
        private string _markupText;

        /// <summary>
        /// A set of instructions that the implementation should follow, if possible.
        /// </summary>
        public List<IInstruction> Instructions { get; private set; }

        /// <summary>
        /// The unparsed Marker string.
        /// </summary>
        public string MarkupText {
            get { return _markupText; }
            set {
                _markupText = value;
                Instructions = Parser.Parse(_markupText);
            }
        }

        /// <summary>
        /// Initializes an instance of the <see cref="TextSequence" /> object.
        /// </summary>
        public TextSequence() : this("") {}

        /// <summary>
        /// Initializes an instance of the <see cref="TextSequence" /> object.
        /// </summary>
        /// <param name="formattedText">The text that should be used to prepopulate this <see cref="TextSequence" /> object.</param>
        public TextSequence(string formattedText) {
            MarkupText = formattedText;
        }

        /// <summary>
        /// Returns only the text within this sequence.
        /// </summary>
        /// <returns>The unformatted text contained in this sequence.</returns>
        public override string ToString() {
            var sb = new StringBuilder();

            foreach(var cmd in Instructions) {
                if(cmd.Command != Command.Text) continue;

                var textCmd = (TextCommand) cmd;
                sb.Append(textCmd.Text);
            }

            return sb.ToString();
        }
    }
}
