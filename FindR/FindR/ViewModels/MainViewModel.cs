using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace FindR.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PlaceViewModel> _places;
        public ObservableCollection<PlaceViewModel> Places
        {
            get
            {
                if (_places == null)
                    _places = new ObservableCollection<PlaceViewModel>();
                return _places;
            }
        }

        public async Task<List<PlaceViewModel>> GetPlaces()
        {
            List<PlaceViewModel> places = new List<PlaceViewModel>();
            using (var client = new HttpClient())
            {
                var geo = new Geolocator();
                var loc = (await geo.GetGeopositionAsync()).Coordinate.Point.Position;
                var param = new Dictionary<string, string>();
                param.Add("lat", loc.Latitude.ToString());
                param.Add("long", loc.Longitude.ToString());
                param.Add("distance", 10.ToString());
                var resp = await client.PostAsync("http://173.250.206.173:8080/findR/php/output/getNodeData.php", new FormUrlEncodedContent(param));
                var content = await resp.Content.ReadAsStringAsync();

                var items = content.Split(new string[] { "<br>" }, StringSplitOptions.None);
                foreach (string item in items)
                {
                    if (string.IsNullOrWhiteSpace(item))
                    {
                        continue;
                    }
                    var components = item.Split(new string[] { "$$$$$" }, StringSplitOptions.None);
                    PlaceViewModel place = new PlaceViewModel();
                    place.Lat = double.Parse(components[1]);
                    place.Lon = double.Parse(components[2]);
                    place.Type = components[3];
                    place.Name = components[4];
                    place.Details = components[5];

                    var comments = components[8].Split(new string[] { "~~~~~" }, StringSplitOptions.None);
                    for(int i=0;i<comments.Length;i+=2)
                    {
                        place.Comments.Add(comments[i]);
                    }

                    places.Add(place);
                }
            }
            return places;
        }

        public async Task<bool> GetPlaces(string type)
        {
            var places = await GetPlaces();
            places = places.Where(x => x.Type.Contains(type)).ToList();
            Places.Clear();
            foreach (var p in places)
            {
                Places.Add(p);
                p.index = Places.IndexOf(p);
            }
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
