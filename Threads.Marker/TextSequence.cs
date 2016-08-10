using System.Collections.Generic;
using System.Text;
using Threads.Marker.Commands;

namespace Threads.Marker {
    /// <summary>
    /// A sequence of text and styles.
    /// </summary>
    public class TextSequence {
        /// <summary>
        /// A set of instructions that the implementation should follow, if possible.
        /// </summary>
        public List<IInstruction> Instructions { get; set; }

        /// <summary>
        /// Initializes an instance of the <see cref="TextSequence" /> object.
        /// </summary>
        public TextSequence() {
            Instructions = new List<IInstruction>();
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
