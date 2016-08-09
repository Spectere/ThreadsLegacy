namespace Threads.Marker.Commands {
    /// <summary>
    /// A collection of simple text.
    /// </summary>
    public class NoneCommand : IInstruction {
        public Command Command => Command.None;
    }
}
