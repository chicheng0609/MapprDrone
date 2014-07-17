using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapprDrone.WP8.Models;
using MapprDrone.WP8.Services;

namespace MapprDrone.WP8.ViewModels
{
    public enum WeatherImage
    {
        Chance,
        Overcast,
        Rain,
        Snow,
        Sun,
        Thunder
    }

    public class WeatherViewModel : INotifyPropertyChanged
    {
        private WeatherImage _weatherIcon;
        private WeatherService _weatherService;

        public WeatherImage WeatherIcon
        {
            get { return _weatherIcon; }
            set
            {
                _weatherIcon = value;
                NotifyPropertyChanged("WeatherIcon");
            }
        }

        private string _location;

        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                NotifyPropertyChanged("Location");
            }
        }

        private string _windSpeed;

        public string WindSpeed
        {
            get { return _windSpeed; }
            set
            {
                _windSpeed = value;
                NotifyPropertyChanged("WindSpeed");
            }
        }

        private string _temperature;

        public string Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                NotifyPropertyChanged("Temperature");
            }
        }

        public WeatherViewModel()
        {
            _weatherService = WeatherService.Instance;
            _weatherService.WeatherUpdated += _weatherService_WeatherUpdated;
            if (_weatherService.CurrentWeather != null)
                ProcessModel(_weatherService.CurrentWeather);
        }

        private void ProcessModel(WeatherModel model)
        {
            Location = model.LocationName;
            WindSpeed = string.Concat(model.WindSpeed, " km/h wind");
            Temperature = model.Temperature;
        }

        void _weatherService_WeatherUpdated(WeatherModel weather)
        {
            ProcessModel(weather);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
