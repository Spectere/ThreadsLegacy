using System;

namespace Threads.Editor.Objects.Page {
    internal class Paragraph : PageObject {
        public new static string ObjectName => "Paragraph";
        public new static string Description => "A basic paragraph object.";
        public override Type HandledType => typeof(Interpreter.Objects.Page.Paragraph);
        public Paragraph(Interpreter.Objects.Page.PageObject objectData, Interpreter.Types.Story storyData) : base(objectData, storyData) {}
    }
}
