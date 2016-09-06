using System;
using System.Windows.Controls;
using ThreadsImage = Threads.Interpreter.Objects.Page.Image;

namespace Threads.Editor.Objects.Page {
    internal class Image : PageObject {
        public override string ObjectName => "Image";
        public override string Description => "An object that displays images.";
        public override Type HandledType => typeof(ThreadsImage);
        public Image(Interpreter.Objects.IObject objectData, Interpreter.Types.Story storyData) : base(objectData, storyData) {}

        protected override void BuildPageObjectEditor() {
            AppendRow(new Label { Content = "Path" },
                CreateBoundTextBox(ObjectData, "Source"));

            base.BuildPageObjectEditor();
        }
    }
}
