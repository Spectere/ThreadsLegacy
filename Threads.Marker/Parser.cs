using System.Collections.Generic;
using System.Text;
using Threads.Marker.Commands;
using Threads.Marker.Commands.SubstitutionProperties;

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
            var substitution = false;
            foreach(var ch in text) {
                IInstruction command;
                if(substitution) {
                    // Get the contents of the substitution token and parse it.
                    if(ch == '}') {
                        substitution = false;
                        instructions.Add(ParseSubstitution(sb.ToString()));
                        sb = new StringBuilder();
                        continue;
                    }
                }

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
                    case Command.Substitution:
                        if(sb.Length > 0) {
                            instructions.Add(new TextCommand { Text = sb.ToString() });
                            sb = new StringBuilder();
                        }
                        substitution = true;
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

        private static SubstitutionCommand ParseSubstitution(string value) {
            var newCommand = new SubstitutionCommand();
            var baseTokens = value.Split('|');

            newCommand.Variable = baseTokens[0].Trim();
            if(baseTokens.Length <= 1) return newCommand;

            // Additional properties have been specified; handle them.
            var properties = baseTokens[1].Split(',');

            foreach(var property in properties) {
                if(string.IsNullOrWhiteSpace(property)) continue;
                var keyValue = property.Split('=');
                var onlyKey = keyValue.Length == 1;

                switch(keyValue[0].Trim().ToLower()) {
                    case "caps":
                        if(onlyKey) { newCommand.Caps = CapsProperty.None; continue; }
                        switch(keyValue[1].Trim().ToLower()) {
                            case "first":
                                newCommand.Caps = CapsProperty.First;
                                break;
                            case "upper":
                                newCommand.Caps = CapsProperty.Upper;
                                break;
                            case "lower":
                                newCommand.Caps = CapsProperty.Lower;
                                break;
                        }
                        break;
                    case "flag":
                        if(onlyKey) { newCommand.Flag = FlagProperty.TrueFalse; continue; }
                        switch(keyValue[1].Trim().ToLower()) {
                            case "truefalse":
                                newCommand.Flag = FlagProperty.TrueFalse;
                                break;
                            case "yesno":
                                newCommand.Flag = FlagProperty.YesNo;
                                break;
                            case "num":
                                newCommand.Flag = FlagProperty.OneZero;
                                break;
                        }
                        break;
                }
            }

            return newCommand;
        }
    }
}
