using StoryObject = Threads.Interpreter.Objects;
using System;
using System.Linq;
using System.Windows.Controls;
using Threads.Interpreter.Types;

namespace Threads.Editor.Objects.Action {
    internal class Redirect : ActionObject {
        public override string ObjectName => "Redirect";
        public override string Description => "Redirects the user to a specific page.";
        public override Type HandledType => typeof(StoryObject.Action.Redirect);
        public Redirect(StoryObject.IObject objectData, Story storyData) : base(objectData, storyData) {}

        protected override void BuildActionObjectEditor() {
            // If no page is selected, set it to a sensible value.
            var obj = (StoryObject.Action.Redirect)ObjectData;
            if(obj.Target == null) {
                obj.Target = StoryData.Pages.First();
            }

            var roomList = new ComboBox {
                ItemsSource = StoryData.Pages,
                SelectedItem = obj.Target
            };
            roomList.SelectionChanged += RoomList_SelectionChanged;
            AppendRow(new Label { Content = "Destination" }, roomList);
        }

        private void RoomList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(e.AddedItems.Count < 1) return;
            var thisObject = (StoryObject.Action.Redirect)ObjectData;
            thisObject.Target = (Interpreter.Types.Page)e.AddedItems[0];
            UpdateObjectName();
        }
    }
}
