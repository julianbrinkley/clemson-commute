using ClemsonCommuteMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



namespace ClemsonCommuteMVVM
{

    public sealed partial class CreateUserProfilePage : Page
    {

        //UserRepository ur = new UserRepository();


        public CreateUserProfilePage(IUserRepository data)
        {
            this.InitializeComponent();

            //getUser();

        }

        //public async void getUser()
        //{
        //    if (!DesignMode.DesignModeEnabled)
        //    {
        //        var users = await ur.Load();

        //        User user = (from u in users
        //                     where u.UserId == 1
        //                     select u).FirstOrDefault();

        //        textblockUserName.Text = user.FirstName + " " + user.LastName;
        //    }

        //}



        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //}

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
