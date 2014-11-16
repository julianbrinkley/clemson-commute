using ClemsonCommuteMVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
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



        public ViewProfilePage()
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

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {

            //MessageDialog msgbox = new MessageDialog("Profile Created.");
            //await msgbox.ShowAsync();  

            if(!string.IsNullOrWhiteSpace(textboxProfileName.Text))
            {
                App.ProfileName = textboxProfileName.Text;
            }
            else
            {
                textblockProfileError.Visibility = Visibility.Visible;
            }


            App.SelectedModel = vehicleModel;

            //Go to Create Route Page
            Frame.Navigate(typeof(CreateRoute));

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




    }
}
