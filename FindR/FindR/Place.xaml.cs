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
    public sealed partial class Place : Page
    {
        public Place()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = App.ViewModel.Places[(int)e.Parameter];
            var place = (DataContext as PlaceViewModel);
            var cent = new Geopoint(new BasicGeoposition() { Latitude = place.Lat, Longitude = place.Lon });
            map.Center = cent;

            MapIcon icon = new MapIcon();
            icon.Location = new Geopoint(new BasicGeoposition() { Latitude = place.Lat, Longitude = place.Lon });
            icon.Title = place.Name;

            map.MapElements.Add(icon);
        }
    }
}
