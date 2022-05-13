using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TS__Configurator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("M.UI.Xaml.Media.AcrylicBrush"))
            //{
                Microsoft.UI.Xaml.Media.AcrylicBrush myBrush = new Microsoft.UI.Xaml.Media.AcrylicBrush();
                myBrush.TintColor = Color.FromArgb(255, 202, 24, 37);
                myBrush.FallbackColor = Color.FromArgb(255, 202, 24, 37);
                myBrush.TintOpacity = 0.6;

                grid.Background = myBrush;
            //}
            //else
            //{
            //    SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(255, 202, 24, 37));

            //    grid.Fill = myBrush;
            //}
        }


        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
        }
    }
}
