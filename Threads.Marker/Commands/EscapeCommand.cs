namespace Threads.Marker.Commands {
    /// <summary>
    /// An <see cref="IInstruction"/> that signifies that the next character should be returned verbatim.
    /// </summary>
    public class EscapeCommand : IInstruction {
        public Command Command => Command.Escape;
    }
}
