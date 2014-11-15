using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ClemsonCommuteMVVM.Helpers
{
   public class NavigationService : INavigationService
    {

       private Frame _mainFrame;

       public void GoBack()
       {
           if(EnsureMainFrame() && _mainFrame.CanGoBack)
           {           
               _mainFrame.GoBack();

           }


       }


       public void NavigateTo(Object o)
       {
           if(EnsureMainFrame())
           {
               _mainFrame.Navigate(o.GetType());

           }

       }


       private bool EnsureMainFrame()
       {

           if (_mainFrame != null) { return true; }

           _mainFrame = Window.Current.Content as Frame; //may need to change this was Application.Current.Rootvisual before WP 8.1
           return _mainFrame != null;

       }
 


    }
}
