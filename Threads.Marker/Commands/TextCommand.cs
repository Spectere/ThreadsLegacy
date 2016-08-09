namespace Threads.Marker.Commands {
    /// <summary>
    /// A collection of simple text.
    /// </summary>
    public class TextCommand : IInstruction {
        public Command Command => Command.Text;
        public string Text { get; set; }
    }
}
