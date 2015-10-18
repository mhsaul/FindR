using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindR.ViewModels
{
    public class PlaceViewModel : INotifyPropertyChanged
    {
        private int _id;
        public int Id {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    NotifyPropertyChanged(nameof(Id));
                    _id = value;
                }
            }
        }


        public int index = -1; 

        double _lat;
        public double Lat
        {
            get { return _lat; }
            set
            {
                if (_lat != value)
                {
                    NotifyPropertyChanged(nameof(Lat));
                    _lat = value;
                }
            }
        }

        double _lon;
        public double Lon
        {
            get { return _lon; }
            set
            {
                if (_lon != value)
                {
                    NotifyPropertyChanged(nameof(Lon));
                    _lon = value;
                }
            }
        }

        string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    NotifyPropertyChanged(nameof(Name));
                    _name = value;
                }
            }
        }

        string _details;
        public string Details
        {
            get { return _details; }
            set
            {
                if (_details != value)
                {
                    NotifyPropertyChanged(nameof(Details));
                    _details = value;
                }
            }
        }

        string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    NotifyPropertyChanged(nameof(Type));
                    _type = value;
                }
            }
        }

        public ObservableCollection<string> Comments { get; private set; } = new ObservableCollection<string>();

        public override string ToString()
        {
            return $"{index}: {Name}";
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
