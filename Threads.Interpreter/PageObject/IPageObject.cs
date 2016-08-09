using Threads.Interpreter.Types;

namespace Threads.Interpreter.PageObject {
    public interface IPageObject {
        /// <summary>
        /// The type of page object.
        /// </summary>
        PageObjectType Type { get; }

        /// <summary>
        /// A textual description of the object.
        /// </summary>
        string Text { get; }
    }
}
