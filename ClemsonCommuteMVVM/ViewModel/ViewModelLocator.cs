using ClemsonCommuteMVVM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClemsonCommuteMVVM.ViewModel
{
   public class ViewModelLocator
    {


       public MainPageViewModel Main
       {
           get;
           private set;

       }

       public ViewModelLocator()
       {
           IDialogService dialogService = new DialogService();

           INavigationService navigationService = new Helpers.NavigationService();

           Main = new MainPageViewModel(dialogService, navigationService);

       }


    }
}
