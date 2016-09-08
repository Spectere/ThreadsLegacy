using System.Collections.Generic;
using System.Text;
using Threads.Marker.Commands;

namespace Threads.Marker {
    /// <summary>
    /// The Marker text markup parser.
    /// </summary>
    public static class Parser {
        /// <summary>
        /// Parses text that has been marked up using Marker.
        /// </summary>
        /// <param name="text">The marked up string of text.</param>
        /// <returns>A <see cref="TextSequence" /> based on the marked up text.</returns>
        public static List<IInstruction> Parse(string text) {
            var instructions = new List<IInstruction>();
            if(string.IsNullOrWhiteSpace(text)) return instructions;

            var sb = new StringBuilder();
            var skip = false;
            foreach(var ch in text) {
                IInstruction command;
                if(skip) {
                    command = new NoneCommand();
                    skip = false;
                } else {
                    command = Token.TokenList.ContainsKey(ch) ? Token.TokenList[ch] : new NoneCommand();
                }

                switch(command.Command) {
                    case Command.Escape:
                        skip = true;
                        continue;
                    case Command.TextStyle:
                        if(sb.Length > 0) {
                            instructions.Add(new TextCommand { Text = sb.ToString() });
                            sb = new StringBuilder();
                        }
                        instructions.Add(command);
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }

            // If there is anything in our StringBuilder, flush it.
            if(sb.Length > 0)
                instructions.Add(new TextCommand { Text = sb.ToString() });
            
            return instructions;
        }
    }
}
