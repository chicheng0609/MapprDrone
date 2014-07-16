using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace MapprDrone.WP8
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        //rate
        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MarketplaceReviewTask mrt = new MarketplaceReviewTask();
            mrt.Show();
        }

        // twitter
        private void Grid_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("https://twitter.com/hlabadi");
            wbt.Show();
        }

        // github
        private void Grid_Tap_2(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("https://github.com/hlabadi/MapprDrone");
            wbt.Show();
        }
    }
}