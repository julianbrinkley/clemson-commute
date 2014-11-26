using ClemsonCommuteMVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage;
using Windows.Storage.Streams;
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
    public sealed partial class CreateRoute : Page
    {
        private static List<Profile> myProfiles = new List<Profile>();

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


 



        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await deserializeProfiles();

        }

        private void comboboxDestination_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);  
        }

        private void linkButtonDestination_Tapped(object sender, TappedRoutedEventArgs e)
        {


            var list = getLocations();

            destinationsList.ItemsSource = list;

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);


        }


        private void linkButtonArrivalTime_Click(object sender, RoutedEventArgs e)
        {
            var list = getArrivalTimes();

            arrivalTimeList.ItemsSource = list;

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }


        private void linkButtonParking_Tapped(object sender, TappedRoutedEventArgs e)
        {

            var list = getParkingPass();

            parkingList.ItemsSource = list;

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }




        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

            var element = (RadioButton)sender;
            var desLocation = element.DataContext as Location;

            if (desLocation == null)
            {
                return;
            }


            //modelYear = mYear;

            linkButtonDestination.Content = desLocation.Latitude;

            destFlyout.Hide();



        }

        private void rbParking_Checked(object sender, RoutedEventArgs e)
        {
            var element = (RadioButton)sender;
            var desParking = element.DataContext as CreatedListItem;

            if (desParking == null)
            {
                return;
            }

            linkButtonParking.Content = desParking.DisplayName;

            parkFlyout.Hide();
        }

        private void rbArrival_Checked(object sender, RoutedEventArgs e)
        {
            var element = (RadioButton)sender;
            var desArrival = element.DataContext as CreatedListItem;

            if (desArrival == null)
            {
                return;
            }


            linkButtonArrivalTime.Content = desArrival.DisplayName;

            arriFlyout.Hide();


        }




        //Temporary helper method for destinations
        private List<Location> getLocations()
        {
            var myLocations = new List<Location>();

            myLocations.Add(new Location() { Latitude = 34.675848F, Longitude= -82.834577F}); //McAdams Hall

            myLocations.Add(new Location() { Latitude = 34.676390F, Longitude = -82.832145F}); //Hendrix

            return myLocations;

        }




        //helper method for parking passes
        private List<CreatedListItem> getParkingPass()
        {
            var parkPasses = new List<CreatedListItem>();

            parkPasses.Add(new CreatedListItem{DisplayName="Free", value="Free"});
            parkPasses.Add(new CreatedListItem { DisplayName = "Metered", value = "Metered" });
            parkPasses.Add(new CreatedListItem { DisplayName = "Pass", value = "Pass" });

            return parkPasses;

        }



        //helper method for arrival times
        private List<CreatedListItem> getArrivalTimes()
        {


            var parkPasses = new List<CreatedListItem>();


            DateTime currentTime = DateTime.Now;





                        
            parkPasses.Add(new CreatedListItem { DisplayName = "It Doesn't Matter", value = "It Doesn't Matter" });

            for (int i = 1; i < 10; i++ )
            {

                DateTime hourFromNow =  currentTime.AddHours(i);            
                
                string newTime = hourFromNow.ToString("h tt");                
                
                parkPasses.Add(new CreatedListItem { DisplayName = newTime, value = Convert.ToString(i) });

            }

            return parkPasses;

        }

        private void btnCreateRoute_Click(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(ViewRoutes));


        }

        private async void linkButtonProfile_Click(object sender, RoutedEventArgs e)
        {
            var localSettings =  Windows.Storage.ApplicationData.Current.LocalSettings;

           Object userID = localSettings.Values["UserID"];

            await deserializeProfiles();

            if(myProfiles.Count() == 0)
            {
                //HyperlinkButton btnCreateProfile = new HyperlinkButton();
                //btnCreateProfile.Content = "create profile";
                //ListViewItem item = new ListViewItem();
                //item.
                //profilesList.Items.Add(btnCreateProfile);
                
            }
            else
            {
                //profilesList.ItemsSource =  (from p in myProfiles
                //                             where p.UserId == Convert.ToInt32(userID)
                //                             select p).ToList();

                profilesList.ItemsSource = myProfiles;
            }
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }


        //deserialize Json
        private async Task deserializeProfiles()
        {

            string content = String.Empty;

            var jsonSerializer = new DataContractJsonSerializer(typeof(List<Profile>));


            try
            { 
                var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("profiles.json");


                   myProfiles = (List<Profile>)jsonSerializer.ReadObject(myStream);
                

            }
            catch(Exception x)
            {

            }




        }

        private void hyperlinkCreateProfile_Click(object sender, RoutedEventArgs e)
        {
            //string pageName = "profile";
            Frame.Navigate(typeof(ViewProfilePage));
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ViewProfilePage));
        }






    }
}
