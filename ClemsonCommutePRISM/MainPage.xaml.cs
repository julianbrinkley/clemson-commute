using ClemsonCommutePRISM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
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



namespace ClemsonCommutePRISM
{

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {


            this.InitializeComponent();

  
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            textblockHello.Text = "Hello World";

            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }


        private const string XMLFILENAME = "data.xml";
        private const string JSONFILENAME = "data.json";

        private async void readButton_Click(object sender, RoutedEventArgs e)
        {
            //await readXMLAsync();
            //await readJsonAsync();
            await deserializeJsonAsync();
        }

        private async void writeButton_Click(object sender, RoutedEventArgs e)
        {
            //build object graph and write it to disk
            //await writeXMLAsync();
            await writeJsonAsync();
        }


        //Write Json
        private async Task writeJsonAsync()
        {

            var myUsers = buildObjectGraph();

            var serializer = new DataContractJsonSerializer(typeof(List<User>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                JSONFILENAME, CreationCollisionOption.ReplaceExisting))
                {
                serializer.WriteObject(stream, myUsers);
            }
            resultTextBlock.Text = "Write succeeded";

        }


        //Read Json
        private async Task readJsonAsync()
        {

            string content = String.Empty;

            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(JSONFILENAME);
            using (StreamReader reader = new StreamReader(myStream))
            {

                content = await reader.ReadToEndAsync();
            }

            resultTextBlock.Text = content;

        }

        //deserialize Json
        private async Task deserializeJsonAsync()
        {

            string content = String.Empty;

            List<User> myUsers;

            var jsonSerializer = new DataContractJsonSerializer(typeof(List<User>));

            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(JSONFILENAME);

            myUsers = (List<User>)jsonSerializer.ReadObject(myStream);

            foreach(var u in myUsers)
            {

                content += String.Format("Firstname: {0}, Lastname: {1}, Email: {2}, Pass: {3}", u.FirstName, u.LastName, u.Email, u.Password);
            }

            resultTextBlock.Text = content;
        }

        //Write XML
        private async Task writeXMLAsync()
        {

            var myUsers = buildObjectGraph();

            var serializer = new DataContractSerializer(typeof(List<User>));

            using (var stream =  await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                    XMLFILENAME, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, myUsers);
            }

            resultTextBlock.Text = "Write succeeded";

        }


        //Read XML
        private async Task readXMLAsync()
        {

            string content = String.Empty;

            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(XMLFILENAME);
            using (StreamReader reader = new StreamReader(myStream))
            {

                content = await reader.ReadToEndAsync();
            }

            resultTextBlock.Text = content;

        }

        //Build objectGraph
        private List<User> buildObjectGraph()
        {
            var myUsers = new List<User>();

            myUsers.Add(new User() { FirstName = "Julian", LastName = "Brinkley", Email = "julianlbrinkley@gmail.com", Password = "pass", UserId = 1 });

            myUsers.Add(new User() { FirstName = "Jimmy", LastName = "Johnson", Email = "jimmyjohnson@gmail.com", Password = "pass", UserId = 2 });

            myUsers.Add(new User() { FirstName = "Phil", LastName = "Gomez", Email = "philgomez@gmail.com", Password = "pass", UserId = 3 });

            return myUsers;

        }

        private void calcDistance_Click(object sender, RoutedEventArgs e)
        {

            



        }
    }
}
