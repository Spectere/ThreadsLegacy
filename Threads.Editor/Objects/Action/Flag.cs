using StoryObject = Threads.Interpreter.Objects.Action;
using System;
using System.Windows.Controls;
using Threads.Interpreter.Objects;
using Threads.Interpreter.Types;

namespace Threads.Editor.Objects.Action {
    internal class Flag : ActionObject {
        public override string ObjectName => "Flag";
        public override string Description => "Manipulates an internal flag in the game engine.";
        public override Type HandledType => typeof(StoryObject.Flag);
        public Flag(IObject objectData, Story storyData) : base(objectData, storyData) { }

        protected override void BuildActionObjectEditor() {
            var settingList = new ComboBox();
            settingList.Items.Add(StoryObject.Flag.FlagAction.Set);
            settingList.Items.Add(StoryObject.Flag.FlagAction.Unset);
            settingList.Items.Add(StoryObject.Flag.FlagAction.Toggle);
            settingList.SelectionChanged += (sender, args) => {
                                                if(args.AddedItems.Count < 1) return;
                                                var thisObject = (StoryObject.Flag)ObjectData;
                                                thisObject.Setting = (StoryObject.Flag.FlagAction)args.AddedItems[0];
                                            };

            AppendRow(new Label { Content = "Setting" }, settingList);
        }
    }
}
