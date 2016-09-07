using System.Windows;
using System.Windows.Controls;

namespace Threads.Editor {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App {
        private void App_OnStartup(object sender, StartupEventArgs e) {
            EventManager.RegisterClassHandler(typeof(TextBox), UIElement.GotFocusEvent, new RoutedEventHandler(TextBox_GotFocus));
        }

        private static void TextBox_GotFocus(object sender, RoutedEventArgs e) {
            (sender as TextBox)?.SelectAll();
        }
    }
}
