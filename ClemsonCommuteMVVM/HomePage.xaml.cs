using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class HomePage : Page
    {


        public HomePage()
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
            

           var localSettings =  Windows.Storage.ApplicationData.Current.LocalSettings;

           //localSettings.Values["loggedIn"] = "False";

           Object value = localSettings.Values["loggedIn"];

            if (value.ToString() == "False")
            {
                //barCommandBar.SecondaryCommands.RemoveAt(0);
                AppBarButton btnSignIn = new AppBarButton();
                btnSignIn.Label = "sign in / sign out";
                btnSignIn.Click += btnSignIn_Click;
                barCommandBar.SecondaryCommands.Insert(0, btnSignIn);
            }
            else
            {

                //add a button that shows the user
                AppBarButton btnLoggedInAs = new AppBarButton();
                btnLoggedInAs.Label = "signed in as julianlbrinkley@gmail.com";
                btnLoggedInAs.Click += btnLogOut_Click;
                
                AppBarButton btnLogout = new AppBarButton();
                btnLogout.Label = "logout";
                btnLogout.Click += btnLogOut_Click;

                barCommandBar.SecondaryCommands.Insert(0, btnLoggedInAs);
                barCommandBar.SecondaryCommands.Insert(1, btnLogout);
            }


        }

        private void textboxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Background = Brushes.Transparent//
            

        }



        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {            
            Frame.Navigate(typeof(SettingsPage));

        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
           var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

           localSettings.Values["loggedIn"] = "False";


           Frame.Navigate(typeof(HomePage));
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {

            //loggedInValue = localSettings.Values["loggedIn"];

            Frame.Navigate(typeof(SignInPage));
        }




    }
}

