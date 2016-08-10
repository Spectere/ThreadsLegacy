namespace Threads.Marker.Commands {
    /// <summary>
    /// A run of plain text.
    /// </summary>
    public class TextCommand : IInstruction {
        public Command Command => Command.Text;

        /// <summary>
        /// The text contained in this instruction.
        /// </summary>
        public string Text { get; set; }
    }
}
