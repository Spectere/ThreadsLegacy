namespace Threads.Interpreter.Objects {
    /// <summary>
    /// Represents an interface for a story object.
    /// </summary>
    public interface IObject {
        /// <summary>
        /// The internal name of the object.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// A set of conditions in which to hide this object.
        /// </summary>
        string HideIf { get; set; }

        /// <summary>
        /// A set of conditions in which to show this object.
        /// </summary>
        string ShowIf { get; set; }

        /// <summary>
        /// Exports this <see cref="IObject" /> instance into an XML object.
        /// </summary>
        /// <returns>An XML element representing this object.</returns>
        Schema.Object Export();
    }
}
