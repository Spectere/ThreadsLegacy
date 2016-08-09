using System.Collections.Generic;
using Threads.Marker.Commands;

namespace Threads.Marker {
    internal static class Token {
        public static Dictionary<char, IInstruction> TokenList = new Dictionary<char, IInstruction> {
            { '\\', new EscapeCommand() },
            { '*', new StyleCommand { TextStyle = TextStyle.Bold } },
            { '_', new StyleCommand { TextStyle = TextStyle.Italic } }
        };
    }
}
