using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PageObject = Threads.Interpreter.PageObject;

namespace Threads.Player.UIObjects {
    internal class UIImage : UIPageObject {
        public override PageObject.PageObjectType HandledType => PageObject.PageObjectType.Image;

        public UIImage(PageObject.Image imageObject) : base(imageObject) {
            if(File.Exists(imageObject.Source)) {
                var source = new Uri(imageObject.Source, UriKind.RelativeOrAbsolute);
                var container = new Viewbox {
                    Stretch = Stretch.Uniform,
                    StretchDirection = StretchDirection.DownOnly
                };
                var image = new System.Windows.Controls.Image {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Source = new BitmapImage(source)
                };

                image.Width = image.Source.Width;
                image.Height = image.Source.Height;

                container.Child = image;
                Content = container;
            } else {
                TextBlock.FontSize = 20.0;
                TextBlock.HorizontalAlignment = HorizontalAlignment.Center;

                Content = TextBlock;
            }
        }
    }
}
