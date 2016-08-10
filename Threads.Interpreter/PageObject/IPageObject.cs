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

        /// <summary>
        /// The style that should be applied to this page object.
        /// </summary>
        PageObjectStyle Style { get; set; }
    }
}
