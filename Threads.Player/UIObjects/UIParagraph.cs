using System;
using Threads.Interpreter.Objects.Page;
using Threads.Interpreter.Types;

namespace Threads.Player.UIObjects {
    internal class UIParagraph : UIPageObject {
        public override Type HandledType => typeof(Paragraph);

        public UIParagraph(IPageObject pageObject, Data storyData, Style style) : base(pageObject, storyData, style) {
            Content = TextBlock;
        }
    }
}
