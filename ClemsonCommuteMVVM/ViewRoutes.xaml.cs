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

       private static List<Route> myRoutes = new List<Route>(); //the routes you need to get to your final destination


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

            //Location finalDestination = new Location() { Latitude = 34.675874F, Longitude = -82.834545F }; //McAdams
            //Location finalDestination = new Location() { Latitude = 34.673101F, Longitude = -82.829540F }; //C-1 Parking lot
            Location finalDestination = new Location() { Latitude = 34.678315F, Longitude = -82.846544F };//P3

            
            routesList.ItemsSource = getClosestRoutes(allRoutes, finalDestination);


        }


        //returns a list of the current routes.
        private List<Route> getClosestRoutes(List<Route> allRoutes, Location destination)
        {


            var routeTrack = new List<int>(); //keeps track of the id of the route

            //Location startLocation = new Location() { Latitude = 34.815534F, Longitude = -82.325498F }; //my current location

            //Location startLocation = new Location() { Latitude = 34.819979F, Longitude = -82.307278F }; //201 Carolina Point

            Location startLocation = new Location() { Latitude = 34.815534F, Longitude = -82.325498F }; //icar

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
                        Debug.WriteLine("Route number with a stop equal to where I'm going:" + r.RouteID.ToString());
                    }
                }
            }


            //myRoutes.AddRange(getClosestRoutes(routeTrack, allRoutes));

            returnClass rc = new returnClass();

            rc = getClosestStops(routeTrack, allRoutes); //get the closest stop to me from routes with a stop where I'm going

            //Stop closestStop = getClosestStops(routeTrack, allRoutes).FirstOrDefault();



            if(startLocation.Equals(rc.Stops.FirstOrDefault().Location) ) //if this stop is close to me add to route to my route and return
            {
                //int closestRouteID = 
                //Route closestRoute = allRoutes.Find( x => x.RouteID == 1);
                //myRoutes.Add(rc.Routes.FirstOrDefault());
                Debug.WriteLine("Should stop here...");

                myRoutes.Add(rc.Routes.FirstOrDefault());

                return myRoutes;
            }
            else //else run this one more time until this is finished
            {

                //Stop x = rc.Stops.FirstOrDefault();
                myRoutes.Add(rc.Routes.FirstOrDefault());
                endLocation = rc.Stops.FirstOrDefault().Location;
                getClosestRoutes(allRoutes, endLocation);
                //Debug.WriteLine("Else statement....shoudlnt get here...");

            }

            //if the end point of this route is not near my current location I need to find an extra route
            //so the "end" of the route is now the new stopping point

           return myRoutes;


        }

        private returnClass getClosestStops(List <int> routeTrack, List<Route> allRoutes)
        {
            var closestRoutes = new List<Route>();

            Stop[] closestStop = new Stop[1]; //array holding closest stop

            returnClass rc = new returnClass();

            var closestStops = new List<Stop>();


            double minDistance = 100000000.00;

            //Location currentLocation = new Location() { Latitude = 34.819979F, Longitude = -82.307278F }; //201 Carolina Point

            Location currentLocation = new Location() { Latitude = 34.815534F, Longitude = -82.325498F }; //icar


            foreach (Route rt in allRoutes)
            {

                        foreach(Stop s in rt.Stops)
                        {
                            double currentMinimum = getDistance(currentLocation, s.Location); //need to implement

                            Debug.WriteLine("Route " + rt.RouteID + " Stop: " + s.StopID  + " Current minimum: " + currentMinimum.ToString() + " Minimum Distance: " + minDistance.ToString());

                            //the stop needs to be closer AND have a lower time(I think)
                            if( currentMinimum < minDistance & !closestStops.Any(stop => stop.StopID == s.StopID) & routeTrack.Any( item => item == rt.RouteID))
                            {
                                //closestRoute.Insert(0, rt);
                                closestStops.Insert(0, s);
                                rc.Stops = closestStops;

                                closestRoutes.Insert(0, rt);
                                rc.Routes = closestRoutes;

                                closestStop[0] = s;

                                minDistance = currentMinimum;

                                Debug.WriteLine("The Closest stop to ME on the" + rt.RouteName +  " route is " + ((Stop)closestStop[0]).StopID);

                                Debug.WriteLine("Added route " +  rt.RouteID.ToString());

                                //I will need to go back and add a stop number to this
                                //also need to increase efficiency...as it stands it loops through the list twice

                    }

                }

            }

            Debug.WriteLine("The ULTIMATE Closest stop to ICAR is " + ((Stop)closestStop[0]).StopID);

            return rc;
        }

        private class returnClass
        {
            public List<Stop> Stops { get; set; }
            public List<Route> Routes { get; set; }

        }
    }
}
