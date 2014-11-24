using ClemsonCommuteMVVM.Model;
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
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ClemsonCommuteMVVM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DistanceCheck : Page
    {
        public DistanceCheck()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void btnCheckDistance_Click(object sender, RoutedEventArgs e)
        {
            //Location icar = new Location() { Latitude = 34.815534F, Longitude = -82.325498F }; 

            Location mcadams = new Location() { Latitude = 34.675874F, Longitude = -82.834545F };

            //Location p3 = new Location() { Latitude = 34.678315F, Longitude = -82.846544F };

            Location c1 = new Location() { Latitude = 34.673101F, Longitude = -82.829540F };

            double distance = TrackingHelper.CalculateDistance(mcadams, c1);

            tbResults.Text = distance.ToString();
        }
            

    }
}


