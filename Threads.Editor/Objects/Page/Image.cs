using System;
using System.Windows.Controls;
using ThreadsImage = Threads.Interpreter.Objects.Page.Image;

namespace Threads.Editor.Objects.Page {
    internal class Image : PageObject {
        public new static string ObjectName => "Image";
        public new static string Description => "An object that displays images.";
        public override Type HandledType => typeof(ThreadsImage);
        public Image(Interpreter.Objects.Page.PageObject objectData) : base(objectData) {}

        protected override void BuildPageObjectEditor() {
            AppendRow(new Label { Content = "Path" },
                CreateBoundTextBox(ObjectData, "Source"));

            base.BuildPageObjectEditor();
        }
    }
}
