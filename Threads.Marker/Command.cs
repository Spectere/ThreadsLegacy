namespace Threads.Marker {
    /// <summary>
    /// Indicates the type of command that is being passed.
    /// </summary>
    public enum Command {
        /// <summary>
        /// A null command.
        /// </summary>
        None,

        /// <summary>
        /// A run of plain text.
        /// </summary>
        Text,

        /// <summary>
        /// A command indicating that the next character should be returned verbatim.
        /// </summary>
        Escape,

        /// <summary>
        /// A style to apply to the next <see cref="Text" /> run.
        /// </summary>
        TextStyle,

        /// <summary>
        /// Indicates that the interpreter will need to replace this token with the contents of a variable.
        /// </summary>
        Substitution
    }
}
