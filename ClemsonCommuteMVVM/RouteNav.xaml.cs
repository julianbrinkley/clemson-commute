using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
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
    public sealed partial class RouteNav : Page
    {
        public RouteNav()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            GeofenceMonitor.Current.GeofenceStateChanged += GeofenceStateChanged;

            var messageDialog = new Windows.UI.Popups.MessageDialog("Are you sure?");

            messageDialog.Commands.Add(new UICommand(
                "Delete",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));

            messageDialog.Commands.Add(new UICommand(
                "Cancel",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));

            await messageDialog.ShowAsync();



            //Add new Geofence for ICAR

            var iCar = new Geopoint(new BasicGeoposition() { Latitude = 34.815534, Longitude = -82.325498 });


            //AddFence("2", iCar);
        }

        private void map1_Loaded(object sender, RoutedEventArgs e)
        {
            ZoomToMyLocation();

        }

        private async void ZoomToMyLocation()
        {

            var gl = new Geolocator() { DesiredAccuracy = PositionAccuracy.High };
            var location = await gl.GetGeopositionAsync(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(5));



            var pin = new MapIcon()
            {
                Location = location.Coordinate.Point,
                Title = "You are here!",
                Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/nav.png")),
                NormalizedAnchorPoint = new Point() { X = 0.32, Y = 0.78 },
                
            };
            map1.MapElements.Add(pin);
            await map1.TrySetViewAsync(location.Coordinate.Point, 16, 0, 0, MapAnimationKind.Bow);
        }

        private void CommandInvokedHandler(IUICommand command)
        {

            if(command.Label == "Delete")
            {


            }
        }


        //Create GeoFence around ICAR
        public void AddFence(string key, Geopoint position)
        {
            //Replace if it already exists
            Geocircle geoCircle = new Geocircle(position.Position, 25);

            bool singleUse = false;

            MonitoredGeofenceStates mask = 0;
            //monitor entered and exited states
            mask |= MonitoredGeofenceStates.Entered;
            mask |= MonitoredGeofenceStates.Exited;

            var geoFence = new Geofence(key, geoCircle, mask, singleUse, TimeSpan.FromSeconds(1));
            GeofenceMonitor.Current.Geofences.Add(geoFence);

        }

        private async void GeofenceStateChanged(GeofenceMonitor sender, object args)
        {

            var reports = sender.ReadReports();

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (GeofenceStateChangeReport report in reports)
                {
                    GeofenceState state = report.NewState;

                    Geofence geofence = report.Geofence;

                    if (state == GeofenceState.Removed)
                    {
                        // remove the geofence from the geofences collection
                        GeofenceMonitor.Current.Geofences.Remove(geofence);
                    }
                    else if (state == GeofenceState.Entered)
                    {
                        // Your app takes action based on the entered event

                        // NOTE: You might want to write your app to take particular
                        // action based on whether the app has internet connectivity.

                        var messageDialog = new Windows.UI.Popups.MessageDialog("You are at ICAR");

                    }
                    else if (state == GeofenceState.Exited)
                    {
                        // Your app takes action based on the exited event

                        // NOTE: You might want to write your app to take particular
                        // action based on whether the app has internet connectivity.

                        var messageDialog = new Windows.UI.Popups.MessageDialog("You left ICAR");

                    }
                }
            });

            //if (sender.Geofences.Any())
            //{
            //    var reports = sender.ReadReports();
            //    foreach (var report in reports)
            //    {
            //        switch(report.NewState)
            //        {

            //            case GeofenceState.Entered:
            //                {
            //                    var messageDialog = new Windows.UI.Popups.MessageDialog("You are at ICAR");
            //                    break;

            //                }
            //            case GeofenceState.Exited:
            //                {
            //                    var messageDialog = new Windows.UI.Popups.MessageDialog("You left ICAR");
            //                    break;

            //                }

            //        }

            //    }

            //}

        }

    }
}
