using System;
using Threads.Interpreter.Objects.Page;

namespace Threads.Player.UIObjects {
    internal class UIParagraph : UIPageObject {
        public override Type HandledType => typeof(Paragraph);

        public UIParagraph(IPageObject pageObject) : base(pageObject) {
            Content = TextBlock;
        }
    }
}
