using System;
using Threads.Interpreter.Schema;
using Threads.Interpreter.Types;

namespace Threads.Interpreter.Objects.Action {
    public class Flag : ActionObject {
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

        public override void Activate() {
            switch(Setting) {
                case FlagAction.Set:
                    Story.Data.SetFlag(Name);
                    break;
                case FlagAction.Unset:
                    Story.Data.ClearFlag(Name);
                    break;
                case FlagAction.Toggle:
                    if(Story.Data.GetFlag(Name)) Story.Data.ClearFlag(Name);
                    else Story.Data.SetFlag(Name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal override Schema.ActionObject ExportObject() {
            var setting = FlagObjectSetting.toggle;
            var settingSpecified = true;
            switch(Setting) {
                case FlagAction.Set:
                    setting = FlagObjectSetting.set;
                    break;
                case FlagAction.Unset:
                    setting = FlagObjectSetting.unset;
                    break;
                case FlagAction.Toggle:
                    setting = FlagObjectSetting.toggle;
                    break;
                default:
                    settingSpecified = false;
                    break;
            }

            return new FlagObject {
                Setting = setting,
                SettingSpecified = settingSpecified
            };
        }
    }
}
