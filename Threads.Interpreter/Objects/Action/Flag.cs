using System;
using Threads.Interpreter.Types;

namespace Threads.Interpreter.Objects.Action {
    public class Flag : ActionObject {
        private Data _data;

        /// <summary>
        /// Determines how the flag should be set when this <see cref="ActionObject" /> is activated.
        /// </summary>
        public FlagAction Setting { get; set; }

        /// <summary>
        /// A set of actions that the <see cref="Flag" /> <see cref="ActionObject" /> can perform on a given flag.
        /// </summary>
        public enum FlagAction {
            /// <summary>Sets the named flag.</summary>
            Set,

            /// <summary>Unsets the named flag.</summary>
            Unset,

            /// <summary>Toggles the named flag.</summary>
            Toggle
        }

        /// <summary>
        /// Initializes a new <see cref="Flag" /> <see cref="ActionObject" />.
        /// </summary>
        /// <param name="data">The <see cref="Data" /> object for the active story.</param>
        public Flag(Data data) {
            _data = data;
        }

        public override void Activate() {
            switch(Setting) {
                case FlagAction.Set:
                    _data.SetFlag(Name);
                    break;
                case FlagAction.Unset:
                    _data.ClearFlag(Name);
                    break;
                case FlagAction.Toggle:
                    if(_data.GetFlag(Name)) _data.ClearFlag(Name);
                    else _data.SetFlag(Name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
