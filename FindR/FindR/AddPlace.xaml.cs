using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;
using System.Net.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FindR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddPlace : Page
    {
        double? lat;
        double? lon;
        public AddPlace()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            locMap.Center = (await new Geolocator().GetGeopositionAsync()).Coordinate.Point;
        }

        private void EnableMap()
        {
            locMap.IsEnabled = true;
            locMap.IsTapEnabled = true;
        }

        private void DisableMap()
        {
            locMap.IsEnabled = false;
            locMap.IsTapEnabled = false;
        }

        private void Map_Tapped(MapControl sender, MapInputEventArgs args)
        {
            if (!sender.IsEnabled)
                return;

            lat = args.Location.Position.Latitude;
            lon = args.Location.Position.Latitude;

            MapIcon icon = new MapIcon();
            icon.Location = args.Location;
            icon.Title = "";
            sender.MapElements.Clear();

            sender.MapElements.Add(icon);
        }

        private async void Location_Toggled(object sender, RoutedEventArgs e)
        {
            var toggle = sender as ToggleSwitch;
            if (toggle.IsOn)
            {
                locMap.MapElements.Clear();
                DisableMap();

                var locator = new Geolocator();
                var location = await locator.GetGeopositionAsync();
                lat = location.Coordinate.Point.Position.Latitude;
                lon = location.Coordinate.Point.Position.Longitude;

            }
            else
            {
                lat = null;
                lon = null;

                EnableMap();
            }
        }

        private async void Submit_Tap(object sender, TappedRoutedEventArgs e)
        {
            if (lat != null && lon != null && !string.IsNullOrWhiteSpace(nameBox.Text) && typeBox.SelectedIndex != -1)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://173.250.206.173:8080/");
                    var param = new Dictionary<string, string>();
                    param.Add("lat", lat.ToString());
                    param.Add("long", lon.ToString());
                    param.Add("name", nameBox.Text);
                    param.Add("details", detailsBox.Text);
                    string type;
                    switch (typeBox.SelectedIndex)
                    {
                        case 0:
                            type = "bathroom";
                            break;
                        case 1:
                            type = "bike";
                            break;
                        case 2:
                            type = "water";
                            break;
                        case 3:
                            type = "wifi";
                            break;
                        default:
                            type = "";
                            break;
                    }
                    param.Add("type", type);

                    try
                    {
                        this.IsEnabled = false;
                        var resp = await client.PostAsync("findR/php/input/postLocation.php", new FormUrlEncodedContent(param));
                        if (resp.IsSuccessStatusCode)
                        {
                            new MessageDialog("Sucessfully added this location to our database!", "Added Place").ShowAsync();

                            if (Frame.CanGoBack)
                            {
                                Frame.GoBack();
                            }
                            else
                            {
                                App.Current.Exit();
                            }
                            return;
                        }
                    }
                    catch
                    {

                    }
                    this.IsEnabled = true;
                    new MessageDialog("This is probably a network error (make sure you have internet) or our server is down.", "Error Adding").ShowAsync();

                }
            }
            else
            {
                new MessageDialog("There is information missing for this place!", "Can't Add This").ShowAsync();
            }
        }

        private void Cancel_Tap(object sender, TappedRoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            else
            {
                App.Current.Exit();
            }
        }
    }
}
