using System.Text;
using Threads.Marker.Commands;

namespace Threads.Marker {
    public static class Parser {
        public static TextSequence Parse(string text) {
            var ts = new TextSequence();
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
                            ts.Instructions.Add(new TextCommand { Text = sb.ToString() });
                            sb = new StringBuilder();
                        }
                        ts.Instructions.Add(command);
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }

            // If there is anything in our StringBuilder, flush it.
            if(sb.Length > 0)
                ts.Instructions.Add(new TextCommand { Text = sb.ToString() });
            
            return ts;
        }
    }
}
