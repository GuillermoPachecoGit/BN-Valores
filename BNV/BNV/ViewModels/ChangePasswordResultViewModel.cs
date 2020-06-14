using System;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class ChangePasswordResultViewModel : ViewModelBase
    {
        public ChangePasswordResultViewModel(INavigationService navigationService)
            : base(navigationService) {

            Title = "Cambio de contraseña";
            ReturnLoginCommand = new Command(ReturnLoginCommandExecute);
           
        }

        private async void ReturnLoginCommandExecute()
        {
            await NavigationService.GoBackToRootAsync();
        }

        public ICommand ReturnLoginCommand { get; set; }
    }
}
