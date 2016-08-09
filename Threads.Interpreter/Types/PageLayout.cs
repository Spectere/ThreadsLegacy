using System.Collections.Generic;
using Threads.Interpreter.PageObject;

namespace Threads.Interpreter.Types {
    public class PageLayout {
        public List<IPageObject> PageObjects { get; set; }

        public PageLayout() {
            PageObjects = new List<IPageObject>();
        }
    }
}
