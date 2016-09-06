using System;

namespace Threads.Editor.Objects.Page {
    internal class Paragraph : PageObject {
        public override string ObjectName => "Paragraph";
        public override string Description => "A basic paragraph object.";
        public override Type HandledType => typeof(Interpreter.Objects.Page.Paragraph);
        public Paragraph(Interpreter.Objects.IObject objectData, Interpreter.Types.Story storyData) : base(objectData, storyData) {}
    }
}
