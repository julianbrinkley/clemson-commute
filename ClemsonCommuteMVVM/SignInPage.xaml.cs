using ClemsonCommuteMVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

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




                    if(sb.Length < 28)
                    {
                        User u = new User
                        {

                            Email = emailAddress,
                            FirstName = firstName,
                            LastName = lastName,
                            Password = userPassword,
                            UserId = 1

                        };

                        await saveUser(u);

                        localSettings.Values["loggedIn"] = "True";

                        localSettings.Values["User"] = App.CurrentUser.Email.ToString();

                        localSettings.Values["UserID"] = App.CurrentUser.UserId;

                        Frame.Navigate(typeof(HomePage));
                    }
                    else
                    {


                        var messageDialog = new Windows.UI.Popups.MessageDialog(sb.ToString());
                        await messageDialog.ShowAsync();

                    }



                    break;

                case 1:

                    await deserializeJsonAsync();

                    if(App.CurrentUser != null)//if user exists
                    {

                        
                        localSettings.Values["loggedIn"] = "True";
                        localSettings.Values["User"] = App.CurrentUser.Email.ToString();
                        localSettings.Values["UserID"] = App.CurrentUser.UserId;
                        Frame.Navigate(typeof(HomePage));
                    }
                    else //show message box
                    {
                       var loginError = new Windows.UI.Popups.MessageDialog("Username / password not found. Pleae try again.");
                        await loginError.ShowAsync();
                    }

                    break;
            }
        }

        //deserialize Json
        private async Task deserializeJsonAsync()
        {

            string content = String.Empty;

            List<User> myUsers;

            var jsonSerializer = new DataContractJsonSerializer(typeof(List<User>));

            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("user_data.json");

            myUsers = (List<User>)jsonSerializer.ReadObject(myStream);


            try
            {
                User user = (from u in myUsers
                             where u.Email == textboxUsername.Text &
                             u.Password == textboxEnterPassword.Text
                             select u).FirstOrDefault();

                App.CurrentUser = user;

            }
            catch(Exception x)
            {
                Debug.WriteLine("Username, password not found");
            }


        }

        //Save User
        private async Task saveUser(User u)
        {

            var myUsers = new List<User>();

            myUsers.Add(u);

            var serializer = new DataContractJsonSerializer(typeof(List<User>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                "user_data.json", CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, myUsers);
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
