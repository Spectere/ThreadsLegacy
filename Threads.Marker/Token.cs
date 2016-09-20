using System.Collections.Generic;
using Threads.Marker.Commands;

namespace Threads.Marker {
    /// <summary>
    /// Contains a <see cref="Dictionary{TKey,TValue}"/> containing a collection of valid Marker tokens and their associated <see cref="IInstruction"/> commands.
    /// </summary>
    internal static class Token {
        /// <summary>
        /// Contains a collection of valid Marker tokens and their associated <see cref="IInstruction"/> commands.
        /// </summary>
        public static Dictionary<char, IInstruction> TokenList = new Dictionary<char, IInstruction> {
            { '\\', new EscapeCommand() },
            { '*', new StyleCommand { TextStyle = TextStyle.Bold } },
            { '_', new StyleCommand { TextStyle = TextStyle.Italic } },
            { '{', new SubstitutionCommand() }
        };
    }
}
