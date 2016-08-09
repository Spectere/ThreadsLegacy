namespace Threads.Marker.Commands {
    /// <summary>
    /// A collection of simple text.
    /// </summary>
    public class EscapeCommand : IInstruction {
        public Command Command => Command.Escape;
    }
}
