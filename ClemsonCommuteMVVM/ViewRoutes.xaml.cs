using ClemsonCommuteMVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private double getDistance(Location currentLocation, Location locToCheck)
        {

            float xVal = currentLocation.Latitude - locToCheck.Latitude;

            float yval = currentLocation.Longitude - locToCheck.Longitude;

            return Math.Sqrt((xVal * xVal) + (yval + yval));

        }

        //deserialize Json
        private async Task deserializeJsonAsync()
        {
            List<Route> allRoutes;

            var jsonSerializer = new DataContractJsonSerializer(typeof(List<Route>));

            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("routes.json");

            allRoutes = (List<Route>)jsonSerializer.ReadObject(myStream);

            Location finalDestination = new Location() { Latitude = 34.675874F, Longitude = -82.834545F }; //McAdams
            //Location finalDestination = new Location() { Latitude = 34.673101F, Longitude = -82.829540F }; //C-1 Parking lot
            
            routesList.ItemsSource = getClosestRoutes(allRoutes, finalDestination);


        }


        //returns a list of the current routes.
        private List<Route> getClosestRoutes(List<Route> allRoutes, Location destination)
        {
            var myRoutes = new List<Route>(); //the routes you need to get to your final destination

            var routeTrack = new List<int>(); //keeps track of the id of the route

            //Location startLocation = new Location() { Latitude = 34.815534F, Longitude = -82.325498F }; //my current location

            Location startLocation = new Location() { Latitude = 34.819979F, Longitude = -82.307278F }; //201 Carolina Point

            Location endLocation = destination; //McAdams


            foreach (Route r in allRoutes)
            {

                foreach (Stop s in r.Stops)
                {

                    //find all routes that go where I want to go
                    if (endLocation.Equals(s.Location)) 
                    {
                        //myRoutes.Add(r);
                        routeTrack.Add(r.RouteID);
                        Debug.WriteLine(r.RouteID.ToString());
                    }
                }
            }

            //this should actually return the closest stop. if they're numbered I can get the route from the stop ID
            myRoutes.AddRange(getClosestRoutes(routeTrack, allRoutes));

            //then I can check to see if this stop is close to me...if not I will need to run get closestRoute with endpoint
            //previous stop id.

            //if(//the route ends where I am then add to myroutes and return)
            //{

            //}
            //else //else if I need another route. 
            //{

            //}

            //if the end point of this route is not near my current location I need to find an extra route
            //so the "end" of the route is now the new stopping point

           return myRoutes;


        }

        private List<Route> getClosestRoutes(List<int> routeTrack, List<Route> allRoutes)
        {
            var closestRoute = new List<Route>();

            double minDistance = 100000000.00;

            Location currentLocation = new Location() { Latitude = 34.819979F, Longitude = -82.307278F }; //201 Carolina Point

            foreach (Route rt in allRoutes)
            {
                foreach (int id in routeTrack)
                {

                    if (rt.RouteID == id)
                    {
                        foreach(Stop s in rt.Stops)
                        {
                            Debug.WriteLine(String.Format("Current Location: {0}, Location to Check: {1}", currentLocation.Latitude, s.Location.Latitude));
                            double currentMinimum = getDistance(currentLocation, s.Location); //need to implement

                            Debug.WriteLine("Current minimum: " + currentMinimum.ToString() + "Minimum Distance: " + minDistance.ToString());

                            if( currentMinimum < minDistance & !closestRoute.Any(route => route.RouteID == id))
                            {
                                closestRoute.Insert(0, rt);
                                Debug.WriteLine("Added route " +  rt.RouteID.ToString());

                                //I will need to go back and add a stop number to this
                                //also need to increase efficiency...as it stands it loops through the list twice

                            }


                        }

                    }

                }

            }



            return closestRoute;
        }
    }
}
