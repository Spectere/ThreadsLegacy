namespace Threads.Marker.Commands {
    /// <summary>
    /// A null <see cref="IInstruction" />. This instruction should do nothing.
    /// </summary>
    public class NoneCommand : IInstruction {
        public Command Command => Command.None;
    }
}
