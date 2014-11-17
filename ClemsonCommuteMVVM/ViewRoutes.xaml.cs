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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace ClemsonCommuteMVVM
{

    public sealed partial class ViewRoutes : Page
    {



        public ViewRoutes()
        {
            this.InitializeComponent();
        }


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await deserializeJsonAsync();





        }
        //deserialize Json
        private async Task deserializeJsonAsync()
        {
            List<Route> allRoutes;

            var jsonSerializer = new DataContractJsonSerializer(typeof(List<Route>));

            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("routes.json");

            allRoutes = (List<Route>)jsonSerializer.ReadObject(myStream);

            routesList.ItemsSource = getClosestRoutes(allRoutes);
        }


        //returns a list of the current routes.
        private List<Route> getClosestRoutes(List<Route> allRoutes)
        {
            //Need to find the closes route to my Current Location that gets within .5 miles of Destination.

            //Current Location Lat/Long

            //Destination Lat/Long
            Location destination = new Location() { LocationName = "McAdams Hall", Latitude = 34.675874F, Longitude = -82.834545F };



             //Loop through all routes and find one that goes to my desired destination
           foreach(Route r in allRoutes)
            {

                foreach(Stop s in r.Stops)
                {
                    //this works but need to set a threshold...insteat of lat & long EQUALS lat & long within 1 mile of, 2 miles of etc.
                    
                    if(destination.Latitude == s.Latitude & destination.Longitude == s.Longitude)
                    {
                        var myRoutes = new List<Route>();
                        myRoutes.Add(r);
                        return myRoutes;
                    }
                }


            }


           return allRoutes;

            //Loop through routes collection and find ones that are within 5 miles of me
            //foreach()
            //{


            //}
        }
    }
}
