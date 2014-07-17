using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapprDrone.WP8.Services;

namespace MapprDrone.WP8.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DroneService _droneService;
        private bool _isConnected;

        private WeatherViewModel _weatherViewModel;

        public WeatherViewModel WeatherViewModel
        {
            get { return _weatherViewModel; }
            set { _weatherViewModel = value;
            NotifyPropertyChanged("WeatherViewModel");
            }
        }
	

        public bool IsConnected
        {
            get { return _isConnected; }
            set { _isConnected = value;
            NotifyPropertyChanged("IsConnected");
            }
        }

        public MainViewModel()
        {
            _droneService = DroneService.Instance;
            WeatherViewModel = new WeatherViewModel();
        }

        public async void ConnectOrDisconnect()
        {
            if (IsConnected)
            {
                _droneService.Disconnect();
                IsConnected = false;
            }
            else
               IsConnected= await _droneService.Connect();
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
