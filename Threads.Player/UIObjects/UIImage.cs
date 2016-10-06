using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Threads.Interpreter.Types;
using Style = Threads.Interpreter.Types.Style;
using PageObjectImage = Threads.Interpreter.Objects.Page.Image;
using WpfImage = System.Windows.Controls.Image;

namespace Threads.Player.UIObjects {
    internal class UIImage : UIPageObject {
        public override Type HandledType => typeof(PageObjectImage);

        public UIImage(PageObjectImage imageObject, Data storyData, Style style) : base(imageObject, storyData, style) {
            if(File.Exists(imageObject.Source)) {
                // Image exists; display that.
                var source = new Uri(imageObject.Source, UriKind.RelativeOrAbsolute);
                var container = new Viewbox {
                    Stretch = Stretch.Uniform,
                    StretchDirection = StretchDirection.DownOnly
                };
                var image = new WpfImage {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Source = new BitmapImage(source)
                };

                image.Width = image.Source.Width;
                image.Height = image.Source.Height;

                container.Child = image;
                Content = container;
            } else {
                // Image doesn't exist; display text instead.
                TextBlock.FontSize = 20.0;
                TextBlock.HorizontalAlignment = HorizontalAlignment.Center;

                Content = TextBlock;
            }
        }
    }
}
