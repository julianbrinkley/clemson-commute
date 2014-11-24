using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI;
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
    public sealed partial class StopsPage : Page
    {
        public StopsPage()
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

        private void map1_Loaded(object sender, RoutedEventArgs e)
        {

            showRoute();
        }

        private async void showRoute()
        {
            var gl = new Geolocator() { DesiredAccuracy = PositionAccuracy.High };
            var location = await gl.GetGeopositionAsync(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(5));


            await map1.TrySetViewAsync(location.Coordinate.Point, 16, 0, 0, MapAnimationKind.Bow);

            //var endPoint = new Geopoint(new BasicGeoposition() { Latitude = 55.5868550870444, Longitude = 13.0115601917735 });

            var startPoint = new Geopoint(new BasicGeoposition() { Latitude = 34.819979F, Longitude = -82.307278F });

            var endPoint = new Geopoint(new BasicGeoposition() { Latitude = 34.815534, Longitude = -82.325498 });

            //       var finderResult = await MapRouteFinder.GetDrivingRouteAsync(location, endPoint,
            //MapRouteOptimization.Distance,
            //MapRouteRestrictions.DirtRoads | MapRouteRestrictions.Ferries, 180);

            var finderResult = await MapRouteFinder.GetDrivingRouteAsync(startPoint, endPoint);
            if (finderResult.Status != MapRouteFinderStatus.Success) return;
            var route = finderResult.Route;
            var mapRouteView = new MapRouteView(route);
            //mapRouteView.RouteColor = new Color() { R = 0000, B = 0000, G = 0000 };

            mapRouteView.RouteColor = Colors.Black;
            map1.Routes.Add(mapRouteView);
            var firstRouteLeg = route.Legs[0];
            var desc = "";
            foreach (var routeManeuver in firstRouteLeg.Maneuvers)
            {
                desc += routeManeuver.LengthInMeters.ToString() + "m : " +
                    routeManeuver.InstructionText + "\n";
            }
            //await new MessageDialog(desc).ShowAsync();

            textblockRoute.Text = desc;

        }
         
    }
}
