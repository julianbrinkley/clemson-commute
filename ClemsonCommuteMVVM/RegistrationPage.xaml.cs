using ClemsonCommuteMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ClemsonCommuteMVVM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistrationPage : Page
    {

        string firstName = "Julian";
        string lastName = "Brinkley";
        string emailAddress = "julianlbrinkley@gmail.com";
        string userPassword = "P@ssw0rd";

        UserRepository ur = new UserRepository();

        public RegistrationPage()
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
            //if(!string.IsNullOrWhiteSpace(textboxFirstName.Text))
            //{
            //    firstName = textboxFirstName.Text;
            //}
            //else
            //{
            //    textFirstNameError.Visibility = Visibility.Visible;
            //}

            //if(!string.IsNullOrWhiteSpace(textboxLastName.Text))
            //{

            //     lastName = textboxLastName.Text;
            //}
            //else
            //{
            //    textboxLastName.Text = textboxLastName.Text;

            //}

            //if(!string.IsNullOrWhiteSpace(textboxEmailAddress.Text))
            //{

            //     emailAddress = textboxEmailAddress.Text;
            //}
            //else
            //{
            //    textEmailAddressError.Visibility = Visibility.Visible;
            //}


            //if(!string.IsNullOrWhiteSpace(textboxPassword.Text))
            //{

            //    userPassword = textPassword.Text;
            //}
            //else
            //{

            //    textPasswordError.Visibility = Visibility.Visible;
            //}
            
            //create user from textbox value

            //User u = new User
            //{

            //    Email = emailAddress,
            //    FirstName = firstName,
            //    LastName = lastName,
            //    Password = userPassword,
            //    UserId = 1

            //};


            //ur.Add(u);

            Frame.Navigate(typeof(CreateUserProfilePage));

            

        }

    }
}
