using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TS__Configurator
{

    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            NavView.SelectedItem = GeneralView;
        }

        private void NavView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            try
            {
                var item = (NavigationViewItem)args.SelectedItem;
                switch (item.Tag)
                {
                    case "MainPage":
                        contentFrame.Content = new MainPage();
                        break;
                    case "AboutPage":
                        contentFrame.Content = new AboutPage();
                        break;
                }
            }
            catch (System.Exception)
            {
                contentFrame.Content = "Error. Nothing to display.";
            }
        }
    }
}
