using ClemsonCommuteMVVM.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClemsonCommuteMVVM.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        INavigationService _navigationService;

        IDialogService _dialogService;

        //private RelayCommand _refreshCommand;

        //public RelayCommand RefreshCommand
        //{
        //    get
        //    {

        //        return _refreshCommand
        //            ?? (_refreshCommand = new RelayCommand(
        //                async () =>
        //                {
        //                    await Refresh();
        //                }));
        //    }

        //}

        ////Refresh method --probably won't need this for this page

        //private async Task Refresh()
        //{


        //}

        public MainPageViewModel(IDialogService dialogService, INavigationService navigationService)
        {

            _dialogService = dialogService;
            _navigationService = navigationService;

            goToRegistration();
        }

        public MainPageViewModel()
            : this(new DialogService(), new NavigationService())
        {

        }

        public async void goToRegistration()
        {
            //wait 2 seconds
            await Task.Delay(TimeSpan.FromSeconds(2));

            //show message box
            _dialogService.ShowMessage("Hello World");

            //wait 6 seconds
            await Task.Delay(TimeSpan.FromSeconds(6));

            //Navigate to registration
            _navigationService.NavigateTo(typeof(RegistrationPage));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged (string propertyName)
        {

            var handler = PropertyChanged;
            if(handler != null)
            {

                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }


        private Model.Model _selectedModel;

        public Model.Model SelectedModel
        {

            get { return _selectedModel; }
            set
            {
                if (_selectedModel == value) { return; }

                _selectedModel = value;
                RaisePropertyChanged("SelectedModel");

            }
        }

    }
}
