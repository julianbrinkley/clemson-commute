using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClemsonCommuteMVVM.Model
{
    public interface IUserRepository
    {

        Task Add(User user);
        Task Remove(User user);
        Task Update(User user);
        Task<ObservableCollection<User>> Load();



    }
}
