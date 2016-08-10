namespace Threads.Marker {
    /// <summary>
    /// An interface describing a <see cref="TextSequence" /> command.
    /// </summary>
    public interface IInstruction {
        /// <summary>
        /// The type of instruction to perform.
        /// </summary>
        Command Command { get; }
    }
}
