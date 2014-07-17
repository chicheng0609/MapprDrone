using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MapprDrone.WP8.Models;
using Newtonsoft.Json.Linq;
using Windows.Devices.Geolocation;

namespace MapprDrone.WP8.Services
{
    public class WeatherService
    {
        private const string WEATHER_PATH = @"http://api.wunderground.com/api//geolookup/conditions/q/";

        public WeatherModel CurrentWeather { get;private set; }

        private static WeatherService _instance;

        public static WeatherService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WeatherService();
                return _instance;
            }
            set { _instance = value; }
        }

        public WeatherService()
        {
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
                {
                    //User already gave us his agreement for using his position
                    if ((bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"] == true)
                    { 
                            Geolocator geolocator = new Geolocator();
                            geolocator.DesiredAccuracyInMeters = 50;
 
    try
    {
        Geoposition geoposition = await geolocator.GetGeopositionAsync(
             maximumAge: TimeSpan.FromMinutes(5),
             timeout: TimeSpan.FromSeconds(10));
                var wc = new WebClient();
                wc.DownloadStringCompleted += wc_DownloadStringCompleted;
                wc.DownloadStringAsync(new Uri(string.Format("{0}{1},{2}.json", WEATHER_PATH, geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude)));
    }
                        catch (Exception ex)
    {
        if ((uint)ex.HResult == 0x80004004)
        {
            MessageBox.Show("Geolocation is disabled in the Settings.");
        }

    }


                    }
                }
                // 37.8,-122.4
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void ConsentUpdated(bool allowed)
        {
            if (allowed)
                LoadData();
        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var jo = JObject.Parse(e.Result);
            var wm = new WeatherModel
            {
                LocationName = string.Concat((string)jo["current_observation"]["display_location"]["full"], ", ", (string)jo["current_observation"]["display_location"]["country"]),
                Temperature = (string)jo["current_observation"]["temperature_string"],
                WindSpeed = (string)jo["current_observation"]["wind_kph"],
                WeatherDescription = (string)jo["current_observation"]["weather"]
            };

            CurrentWeather = wm;
            if (WeatherUpdated != null)
                WeatherUpdated(wm);
        }

        public event Action<WeatherModel> WeatherUpdated;
    }
}
