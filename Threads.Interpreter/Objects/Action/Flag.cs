﻿using System;
using Threads.Interpreter.Schema;

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
            Clear,

            /// <summary>Toggles the named flag.</summary>
            Toggle
        }

        public override void Activate() {
            var data = Engine.Story.Data;
            switch(Setting) {
                case FlagAction.Set:
                    data.SetFlag(Name);
                    break;
                case FlagAction.Clear:
                    data.ClearFlag(Name);
                    break;
                case FlagAction.Toggle:
                    if(data.GetFlag(Name)) data.ClearFlag(Name);
                    else data.SetFlag(Name);
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
                case FlagAction.Clear:
                    setting = FlagObjectSetting.clear;
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

        public override string ToString() {
            var baseName = base.ToString();
            return string.Format($"{baseName} [{Setting}]");
        }
    }
}
