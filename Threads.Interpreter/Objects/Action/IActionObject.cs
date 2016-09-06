namespace Threads.Interpreter.Objects.Action {
    public interface IActionObject : IObject {
        /// <summary>
        /// Activates the action object.
        /// </summary>
        void Activate();
    }
}
