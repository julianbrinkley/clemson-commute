using ClemsonCommuteMVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.ApplicationModel;
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


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ClemsonCommuteMVVM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewProfilePage : Page
    {
         Model.ModelYear modelYear = new Model.ModelYear();

         Model.Make vehicleMake = new Model.Make();

         Model.Model vehicleModel = new Model.Model();

        private static List<Profile> myProfiles = new List<Profile>(); 

         //Json FileName
         private const string JSONFILENAME = "user_data.json";

         //UserRepository ur = new UserRepository();


        public ViewProfilePage()
        {
            this.InitializeComponent();


        }


        //}
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            await deserializeJsonAsync();

        }

        //deserialize Json
        private async Task deserializeJsonAsync()
        {

            string content = String.Empty;

            List<User> myUsers;

            var jsonSerializer = new DataContractJsonSerializer(typeof(List<User>));

            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(JSONFILENAME);

            myUsers = (List<User>)jsonSerializer.ReadObject(myStream);


            User user = (from u in myUsers
                         where u.UserId == 1
                         select u).FirstOrDefault();


            textblockUser.Text = string.Format("User: {0} {1}", user.FirstName, user.LastName);
        }



        private async void linkbuttonVehicle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var service = new Model.YearService();

            var list = (await service.Refresh()).ToList();

            yearsList.ItemsSource = list;

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var element = (RadioButton)sender;
            var mYear = element.DataContext as Model.ModelYear;

            if(mYear == null)
            {
                return;
            }


            modelYear = mYear;

            linkButtonYear.Content = modelYear.Year;

            linkButtonMake.IsEnabled = true;

            vehYearFlyout.Hide();




        }

        private void btn_Cancel(object sender, RoutedEventArgs e)
        {
            vehYearFlyout.Hide();
        }

        private async void linkButtonMake_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var service = new Model.MakesService();

            string year = linkButtonYear.Content.ToString();

            var list = (await service.Refresh(year)).ToList();

            makesList.ItemsSource = list;

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            var element = (RadioButton)sender;
            var vehMake = element.DataContext as Model.Make;

            if (vehMake == null)
            {
                return;
            }


            vehicleMake = vehMake;

            linkButtonMake.Content = vehicleMake.VehicleMake;

            linkButtonModel.IsEnabled = true;

            vehMakeFlyout.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vehMakeFlyout.Hide();
        }

        private async void linkButtonModel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var service = new Model.ModelService();

            string year = linkButtonYear.Content.ToString();

            string make = linkButtonMake.Content.ToString();

            var list = (await service.Refresh(year, make)).ToList();

            modelsList.ItemsSource = list;

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            var element = (RadioButton)sender;
            var vehModel = element.DataContext as Model.Model;

            if (vehModel == null)
            {
                return;
            }


            vehicleModel = vehModel;

            linkButtonModel.Content = vehicleModel.VehicleModel;

            vehModelFlyout.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vehModelFlyout.Hide();
        }

        private void rbgParkPass_Checked(object sender, RoutedEventArgs e)
        {
            if (rbParkFaculty.IsChecked == true)
            {
                linkButtonParking.Content = "Faculty";
                parkPassFlyout.Hide();
            }
            else if (rbParkStaff.IsChecked == true)
            {
                linkButtonParking.Content = "Staff";
                parkPassFlyout.Hide();
            }
            else if (rbParkStudent.IsChecked == true)
            {
                linkButtonParking.Content = "Student";
                parkPassFlyout.Hide();
            }
            else if (rbParkVendor.IsChecked == true)
            {
                linkButtonParking.Content = "Vendor";
                parkPassFlyout.Hide();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            parkPassFlyout.Hide();

        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //MessageDialog msgbox = new MessageDialog("Profile Created.");
            //await msgbox.ShowAsync();  

            if (!string.IsNullOrWhiteSpace(textboxProfileName.Text))
            {
                App.ProfileName = textboxProfileName.Text;
            }
            else
            {
                textblockProfileError.Visibility = Visibility.Visible;
            }


            App.SelectedModel = vehicleModel;

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            Object userID = localSettings.Values["UserID"];

            //Create profile

            Profile p = new Profile()
            {
                ProfileID = myProfiles.Count() + 1,
                ProfileName = textboxProfileName.Text,
                VehicleMake =  linkButtonMake.Content.ToString(),
                VehModel = linkButtonModel.Content.ToString(),
                VehicleYear =  Convert.ToInt32(linkButtonYear.Content.ToString()),
                UserId =     Convert.ToInt32(userID)      
            };

            try
            {
                await saveProfile(p);
            }
            catch(Exception x)
            {

            }


            //Go to Create Route Page
            Frame.Navigate(typeof(CreateRoute));

        }

        private async Task saveProfile(Profile p)
        {


            string content = String.Empty;

            var jsonSerializer = new DataContractJsonSerializer(typeof(List<Profile>));



           var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("profiles.json");


            List<Profile> originalList = (List<Profile>)jsonSerializer.ReadObject(myStream);


            //aaa

            var profiles = new List<Profile>();

            profiles.Add(p);

            profiles.AddRange(originalList);

            var serializer = new DataContractJsonSerializer(typeof(List<Profile>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                "profiles.json", CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, profiles);
            }

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
            catch (Exception x)
            {

            }


        }



    }
}
