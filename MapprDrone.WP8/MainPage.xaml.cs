using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MapprDrone.WP8.Resources;
using MapprDrone.WP8.ViewModels;
using System.IO.IsolatedStorage;
using MapprDrone.WP8.Services;

namespace MapprDrone.WP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        private MainViewModel _vm;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            _vm = DataContext as MainViewModel;
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
            {
                //User already gave us his agreement for using his position
                if ((bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"] == true)
                    
                    return;
            }

    //Ask for user agreement in using his position
            else
            {
                MessageBoxResult result =
                            MessageBox.Show("To determine the weather at your current position, mapprdrone needs to access your location. Can mapprdone use your location?",
                            "Location",
                            MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
                    WeatherService.Instance.ConsentUpdated(true);
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
                }

                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }
        

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/FlyPage.xaml", UriKind.Relative));
        }

        private void Grid_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/HistoryPage.xaml", UriKind.Relative));
        }

        private void Grid_Tap_2(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        private void Grid_Tap_3(object sender, System.Windows.Input.GestureEventArgs e)
        {
            _vm.ConnectOrDisconnect();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}