using ClemsonCommuteMVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class SignInPage : Page
    {
        string firstName;
        string lastName;
        string emailAddress;
        string userPassword;

        public SignInPage()
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
            foreach (PivotItem item in pivotAccounts.Items.ToList())
            {
                if (item.Visibility == Visibility.Collapsed)
                    pivotAccounts.Items.Remove(item);
            }
        }

        private async void btnSign_Click(object sender, RoutedEventArgs e)
        {

            switch (pivotAccounts.SelectedIndex)
            {
                case 0:


                    //Create string builder to display errors.
                    StringBuilder sb = new StringBuilder("Required field(s) missing: ");

                    if (!string.IsNullOrWhiteSpace(textboxFirstName.Text))
                    {
                        firstName = textboxFirstName.Text;
                    }
                    else
                    {
                        sb.Append("First Name");
                    }

                    if (!string.IsNullOrWhiteSpace(textboxLastName.Text))
                    {

                        lastName = textboxLastName.Text;
                    }
                    else
                    {
                        sb.Append(", Last Name");


                    }

                    if (!string.IsNullOrWhiteSpace(textboxEmailAddress.Text))
                    {

                        emailAddress = textboxEmailAddress.Text;
                    }
                    else
                    {
                        sb.Append(", Email Address");

                    }


                    if (!string.IsNullOrWhiteSpace(textboxPassword.Text))
                    {

                        userPassword = textboxPassword.Text;

                    }
                    else
                    {

                        sb.Append(", Password");

                    }


                    //User u = new User
                    //{

                    //    Email = emailAddress,
                    //    FirstName = firstName,
                    //    LastName = lastName,
                    //    Password = userPassword,
                    //    UserId = 1

                    //};
                    var messageDialog = new Windows.UI.Popups.MessageDialog(sb.ToString());
                    await messageDialog.ShowAsync();

                    break;

                case 1:
                    //var messageDialog2 = new Windows.UI.Popups.MessageDialog("Sign In?");
                    //await messageDialog2.ShowAsync();

                    var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    localSettings.Values["loggedIn"] = "True";
                    Frame.Navigate(typeof(HomePage));
                    break;
            }
        }

        private void linkbuttonPassword_Click(object sender, RoutedEventArgs e)
        {


            pivotSignIn.Visibility = Visibility.Collapsed;
            pivotSignUp.Visibility = Visibility.Collapsed;

            foreach (PivotItem item in pivotAccounts.Items.ToList())
            {
                if (item.Visibility == Visibility.Collapsed)
                    pivotAccounts.Items.Remove(item);
            }

            pivotAccounts.Items.Add(pivotPassword);
            pivotPassword.Visibility = Visibility.Visible;



        }
    }
}
