using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;


namespace ClemsonCommuteMVVM.Helpers
{
    public class DialogService : IDialogService
    {
        public async void ShowMessage(string message)
        {

            MessageDialog msgbox = new MessageDialog(message);
            await msgbox.ShowAsync();  
        }
    }
}
