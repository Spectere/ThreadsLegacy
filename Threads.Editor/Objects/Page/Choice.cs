using StoryObject = Threads.Interpreter.Objects;
using System;
using System.Windows.Controls;

namespace Threads.Editor.Objects.Page {
    internal class Choice : PageObject {
        public override string ObjectName => "Choice";
        public override string Description => "An object that allows the player to choose their next page.";
        public override Type HandledType => typeof(StoryObject.Page.Choice);
        public Choice(StoryObject.IObject objectData, Interpreter.Types.Story storyData) : base(objectData, storyData) { }

        protected override void BuildPageObjectEditor() {
            var roomList = new ComboBox {
                ItemsSource = StoryData.Pages,
                SelectedItem = ((StoryObject.Page.Choice)ObjectData).Target
            };
            roomList.SelectionChanged += RoomList_SelectionChanged;
            AppendRow(new Label { Content = "Destination" }, roomList);
            AppendRow(new Label { Content = "Shortcut Key" },
                CreateBoundTextBox(ObjectData, "Shortcut"));
        }

        private void RoomList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(e.AddedItems.Count < 1) return;
            var thisObject = (StoryObject.Page.Choice)ObjectData;
            thisObject.Target = (Interpreter.Types.Page)e.AddedItems[0];
        }
    }
}
