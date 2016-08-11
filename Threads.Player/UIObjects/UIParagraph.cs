using Threads.Interpreter.PageObject;

namespace Threads.Player.UIObjects {
    internal class UIParagraph : UIPageObject {
        public override PageObjectType HandledType => PageObjectType.Paragraph;

        public UIParagraph(IPageObject pageObject) : base(pageObject) {
            Content = TextBlock;
        }
    }
}
