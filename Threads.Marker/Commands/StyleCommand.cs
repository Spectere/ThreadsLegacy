namespace Threads.Marker.Commands {
    /// <summary>
    /// An <see cref="IInstruction" /> that signifies a style change.
    /// </summary>
    public class StyleCommand : IInstruction {
        public Command Command => Command.TextStyle;

        /// <summary>
        /// The style that should be applied to the next run of text.
        /// </summary>
        public TextStyle TextStyle { get; set; }
    }
}
