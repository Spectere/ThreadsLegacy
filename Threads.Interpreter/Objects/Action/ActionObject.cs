using System;

namespace Threads.Interpreter.Objects.Action {
    public abstract class ActionObject : IActionObject {
        public string Name { get; set; }
        public abstract void Activate();

        public object Export() {
            throw new NotImplementedException();
        }
    }
}
