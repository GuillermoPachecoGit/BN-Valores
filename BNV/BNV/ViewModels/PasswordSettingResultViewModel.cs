using System;
using System.Windows.Input;
using BNV.Validator;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class PasswordSettingResultViewModel : ViewModelBase
    {
        public PasswordSettingResultViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            ReturnLoginCommand = new Command(ReturnLoginCommandExecute);
            Title = "Establecimiento de contraseña";
        }

        private async void ReturnLoginCommandExecute()
        {
            await NavigationService.NavigateAsync("LoginPage");
        }

        public ICommand ReturnLoginCommand { get; set; }
    }
}
