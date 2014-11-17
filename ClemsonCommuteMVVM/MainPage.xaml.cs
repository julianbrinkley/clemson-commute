using ClemsonCommuteMVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ClemsonCommuteMVVM
{

    public sealed partial class MainPage : Page
    {


        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            goToRegistration();
        }



        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{

        //}

        public async void goToRegistration()
        {
            //wait 2 seconds
            //await Task.Delay(TimeSpan.FromSeconds(2));

            //show message box
            //MessageDialog msgbox = new MessageDialog("Hello World");
            //await msgbox.ShowAsync();  

            ////wait 6 seconds
            await Task.Delay(TimeSpan.FromSeconds(2));

            await saveRoutes();

            Frame.Navigate(typeof(RegistrationPage));
        }

        //Json FileName
        private const string JSONFILENAME = "routes.json";

        //Save User
        private async Task saveRoutes()
        {

            var myRoutes = buildObjectGraph();

            var serializer = new DataContractJsonSerializer(typeof(List<Route>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                JSONFILENAME, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, myRoutes);
            }

        }

        //Build objectGraph
        private List<Route> buildObjectGraph()
        {
            var myRoutes = new List<Route>();

            var stops1 = new List<Stop>();

            var stops2 = new List<Stop>();

            stops1.Add(new Stop(){DepartureTime = (DateTime.Now), Latitude=34.815534F, Longitude=-82.325498F}); //ICAR

            stops1.Add(new Stop() { DepartureTime = (DateTime.Now), Latitude = 34.675874F, Longitude = -82.834545F }); //McAdams



            stops2.Add(new Stop() { DepartureTime = (DateTime.Now), Latitude = 34.815534F, Longitude = -82.325498F }); //ICAR

            stops2.Add(new Stop() { DepartureTime = (DateTime.Now), Latitude = 34.850975F, Longitude = -82.398995F }); //Greenville One-Clemson
            
            //Stops for GTA route
            myRoutes.Add(new Route() {ProviderName="GTA", RouteID = 1, RouteName = " ICAR/Clemson", Stops = stops1});

            myRoutes.Add(new Route() { ProviderName = "Tiger Connect", RouteID = 2, RouteName = "ICar/Greenville 1", Stops = stops2 });


            return myRoutes;

        }
    }
}
