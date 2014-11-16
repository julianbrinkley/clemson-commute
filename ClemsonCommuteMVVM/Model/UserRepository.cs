using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ClemsonCommuteMVVM.Model
{
    public class UserRepository : IUserRepository
    {
        //Local folder for Json file storage
        StorageFolder folder = ApplicationData.Current.LocalFolder;

        string fileName = "users.json";

        ObservableCollection<User> users;


        private void Initialize()
        {
            //not needed actually unless I add SqlLite
        }


        public UserRepository()
        {

            Initialize();
        }


        //Add method
        public Task Add(User u)
        {

            users.Add(u);
            return WriteToFile();

        }


        //Remove method
        public Task Remove(User u)
        {

            users.Remove(u);
            return WriteToFile();

        }



        //Update method
        public Task Update(User u)
        {

            //Find old user if exists
            var oldUser = users.FirstOrDefault(ou => ou.UserId == u.UserId);

            if (oldUser == null)
            {

                throw new System.ArgumentException("User does not exists.");

            }
            users.Remove(oldUser);
            users.Add(u);
            return WriteToFile();

        }




        //Load method
        public async Task<ObservableCollection<User>> Load()
        {
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

            string fileContents = string.Empty;

            if (file != null)
            {
                fileContents = await FileIO.ReadTextAsync(file);

            }


            //Deserialize
            IList<User> JsonUsers =
                JsonConvert.DeserializeObject<List<User>>(fileContents) ?? new List<User>();

            users = new ObservableCollection<User>(JsonUsers);

            return users;


        }


        //Writes to Json file

        private Task WriteToFile()
        {

            return Task.Run(async () =>
            {

                string JSON = JsonConvert.SerializeObject(users);
                var file = await OpenFileAsync();
                await FileIO.WriteTextAsync(file, JSON);

            });
        }


        private async Task<StorageFile> OpenFileAsync()
        {

            return await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

        }

    }
}
