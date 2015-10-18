using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FindR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private void AddPlace_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPlace));
        }

        private void Bathroom_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Results), "bathroom");
        }

        private void Bike_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Results),"bike");
        }

        private void Water_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Results), "water");
        }

        private void Wifi_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Results),"wifi");
        }
    }
}
