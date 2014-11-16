using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace ClemsonCommuteMVVM
{

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            goToRegistration();
        }



        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{

        //}

        public async void goToRegistration()
        {
            //wait 2 seconds
            //await Task.Delay(TimeSpan.FromSeconds(2));

            //show message box
            //MessageDialog msgbox = new MessageDialog("Hello World");
            //await msgbox.ShowAsync();  

            ////wait 6 seconds
            await Task.Delay(TimeSpan.FromSeconds(2));


            Frame.Navigate(typeof(RegistrationPage));
        }
    }
}
