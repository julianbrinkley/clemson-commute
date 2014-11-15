using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
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
    public sealed partial class CreateRoute : Page
    {
        public CreateRoute()
        {
            this.InitializeComponent();

            if(App.ProfileName != null)
            {
                textblockProfile.Text = App.ProfileName + " " + "Profile";
            }

            if(App.SelectedModel != null)
            {


                //Set Vehicle Data in Profile
                Model.Model vehicleModel = new Model.Model(); ;

                vehicleModel = App.SelectedModel;

                string vehYear = vehicleModel.Year;

                string vehMake = vehicleModel.VehicleMake;

                string vehModel = vehicleModel.VehicleModel;

                linkButtonProfile.Content = String.Format("{0} {1} {2}", vehYear, vehMake, vehModel);
            }


        }


        private void map1_Loaded(object sender, RoutedEventArgs e)
        {
            ZoomToMalmoe();
        }
         
        private async void ZoomToMalmoe()
        {
            //var malmoe = new Geopoint(new BasicGeoposition() { Latitude = 55.5868550870444, Longitude = 13.0115601917735 });
            //await map1.TrySetViewAsync(malmoe, 11.4086892086054, 0, 0, MapAnimationKind.Bow);

            var gl = new Geolocator() { DesiredAccuracy = PositionAccuracy.High };
            var location = await gl.GetGeopositionAsync(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(5));

            var pin = new MapIcon()
            {
                Location = location.Coordinate.Point,
                Title = "You are here!",
                Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/pin.png")),
                NormalizedAnchorPoint = new Point() { X = 0.32, Y = 0.78 },
            };
            map1.MapElements.Add(pin);
            await map1.TrySetViewAsync(location.Coordinate.Point, 16, 0, 0, MapAnimationKind.Bow);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void comboboxDestination_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);  
        }
    }
}
