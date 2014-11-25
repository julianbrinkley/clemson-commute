using ClemsonCommuteMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
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

        string firstName;
        string lastName;
        string emailAddress;
        string userPassword;


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

        private async void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            //Create string builder to display errors.
            StringBuilder sb = new StringBuilder("Required field(s) missing: ");

            if (!string.IsNullOrWhiteSpace(textboxFirstName.Text))
            {
                firstName = textboxFirstName.Text;
            }
            else
            {
                textFirstNameError.Visibility = Visibility.Visible;
                return;
            }

            if (!string.IsNullOrWhiteSpace(textboxLastName.Text))
            {

                lastName = textboxLastName.Text;
            }
            else
            {
                textboxLastName.Text = textboxLastName.Text;
                return;

            }

            if (!string.IsNullOrWhiteSpace(textboxEmailAddress.Text))
            {

                emailAddress = textboxEmailAddress.Text;
            }
            else
            {
                textEmailAddressError.Visibility = Visibility.Visible;
                return;
            }


            if (!string.IsNullOrWhiteSpace(textboxPassword.Text))
            {

                userPassword = textPassword.Text;

            }
            else
            {

                textPasswordError.Visibility = Visibility.Visible;
                return;
            }
            

            User u = new User
            {

                Email = emailAddress,
                FirstName = firstName,
                LastName = lastName,
                Password = userPassword,
                UserId = 1

            };


            //Serialize
            await saveUser(u);

            Frame.Navigate(typeof(ViewProfilePage));

            

        }

        //Json FileName
        private const string JSONFILENAME = "user_data.json";

        //Save User
        private async Task saveUser(User u)
        {

            var myUsers = new List<User>();

            myUsers.Add(u);

            var serializer = new DataContractJsonSerializer(typeof(List<User>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                JSONFILENAME, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, myUsers);
            }

        }
    }
}
