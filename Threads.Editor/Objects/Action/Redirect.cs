using StoryObject = Threads.Interpreter.Objects;
using System;
using System.Windows.Controls;
using Threads.Interpreter.Types;

namespace Threads.Editor.Objects.Action {
    internal class Redirect : ActionObject {
        public override string ObjectName => "Redirect";
        public override string Description => "Redirects the user to a specific page.";
        public override Type HandledType => typeof(StoryObject.Action.Redirect);
        public Redirect(StoryObject.IObject objectData, Story storyData) : base(objectData, storyData) {}

        protected override void BuildActionObjectEditor() {
            var roomList = new ComboBox {
                ItemsSource = StoryData.Pages,
                SelectedItem = ((StoryObject.Page.Choice)ObjectData).Target
            };
            roomList.SelectionChanged += RoomList_SelectionChanged;
        }

        private void RoomList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(e.AddedItems.Count < 1) return;
            var thisObject = (StoryObject.Action.Redirect)ObjectData;
            thisObject.Target = (Interpreter.Types.Page)e.AddedItems[0];
        }
    }
}
