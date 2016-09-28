using StoryObject = Threads.Interpreter.Objects.Action;
using System;
using System.Windows.Controls;
using Threads.Interpreter.Objects;
using Threads.Interpreter.Types;

namespace Threads.Editor.Objects.Action {
    internal class Variable : ActionObject {
        public override string ObjectName => "Variable";
        public override string Description => "Manipulates a game variable.";
        public override Type HandledType => typeof(StoryObject.Variable);
        public Variable(IObject objectData, Story storyData) : base(objectData, storyData) { }

        protected override void BuildActionObjectEditor() {
            var opList = new ComboBox();
            opList.Items.Add(StoryObject.Variable.VariableAction.Set);
            opList.Items.Add(StoryObject.Variable.VariableAction.Add);
            opList.Items.Add(StoryObject.Variable.VariableAction.Subtract);
            opList.Items.Add(StoryObject.Variable.VariableAction.Multiply);
            opList.Items.Add(StoryObject.Variable.VariableAction.Divide);
            opList.Items.Add(StoryObject.Variable.VariableAction.Modulus);
            opList.SelectedItem = ((StoryObject.Variable)ObjectData).Operation;
            opList.SelectionChanged += (sender, args) => {
                                           if(args.AddedItems.Count < 1) return;
                                           var thisObject = (StoryObject.Variable)ObjectData;
                                           thisObject.Operation = (StoryObject.Variable.VariableAction)args.AddedItems[0];
                                           UpdateObjectName();
                                       };

            AppendRow(new Label { Content = "Operation" }, opList);
            AppendRow(new Label { Content = "Expression" },
                      CreateBoundTextBox(ObjectData, "Expression"));
        }
    }
}
