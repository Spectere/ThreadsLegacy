using Threads.Interpreter.Objects.Page;

namespace Threads.Player.UIObjects {
    internal class UIParagraph : UIPageObject {
        public override PageObjectType HandledType => PageObjectType.Paragraph;

        public UIParagraph(IPageObject pageObject) : base(pageObject) {
            Content = TextBlock;
        }
    }
}
