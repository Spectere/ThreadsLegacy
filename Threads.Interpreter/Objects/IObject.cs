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
        /// Exports this <see cref="IObject" /> instance into an XML object.
        /// </summary>
        /// <returns>An XML element representing this object.</returns>
        Schema.Object Export();
    }
}
