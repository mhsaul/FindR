using FindR.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FindR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Results : Page
    {
        public Results()
        {
            this.InitializeComponent();
            listBox.ItemsSource = App.ViewModel.Places;
            
        }
        
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await App.ViewModel.GetPlaces((string)e.Parameter);
            UpdateMap();
            ZoomMap();
        }

        async void ZoomMap()
        {
            var geoloc = new Geolocator();
            resultsMap.Center = (await geoloc.GetGeopositionAsync()).Coordinate.Point;
            resultsMap.ZoomLevel = 15;

        }

        private void Places_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateMap();
        }

        private void UpdateMap()
        {
            resultsMap.Children.Clear();
            foreach (PlaceViewModel p in App.ViewModel.Places)
            {
                MapIcon icon = new MapIcon();
                icon.Location = new Geopoint(new BasicGeoposition() { Latitude = p.Lat, Longitude = p.Lon });
                icon.Title = App.ViewModel.Places.IndexOf(p).ToString();

                resultsMap.MapElements.Add(icon);
            }
        }

        private void SeletionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedIndex == -1)
            {
                return;
            }
            Frame.Navigate(typeof(Place), listBox.SelectedIndex);
            listBox.SelectedIndex = -1;
        }
    }
}
