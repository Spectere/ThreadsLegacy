using System;
using System.Collections.Generic;
using Threads.Interpreter.Objects.Page;
using Threads.Interpreter.Types;

namespace Threads.Interpreter.StaticData {
    public static class DefaultStyles {
        public static Dictionary<Type, Style> Style => new Dictionary<Type, Style> {
            {
                typeof(Story),
                new Style()
            }, {
                typeof(Page),
                new Style()
            }, {
                typeof(Paragraph),
                new Style {
                    MarginTop = 0.0,
                    MarginBottom = 20.0
                }
            }, {
                typeof(Choice),
                new Style {
                    MarginTop = 0.0,
                    MarginBottom = 7.5
                }
            }, {
                typeof(Image),
                new Style {
                    MarginTop = 0.0,
                    MarginBottom = 20.0
                }
            }
        };

        public static PageStyle DefaultPageStyle => new PageStyle {
            PageMarginBottom = 0.0,
            PageMarginLeft = 40.0,
            PageMarginRight = 40.0,
            PageMarginTop = 0.0
        };
    }
}
