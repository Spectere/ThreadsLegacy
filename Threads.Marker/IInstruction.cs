namespace Threads.Marker {
    /// <summary>
    /// An interface for an implementation instruction.
    /// </summary>
    public interface IInstruction {
        /// <summary>
        /// The instruction to perform.
        /// </summary>
        Command Command { get; }
    }
}
