using System;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        public WelcomeViewModel(INavigationService navigationService)
          : base(navigationService)
        {

            RegisterCommand = new Command(RegisterCommandExecute);
            LoginCommand = new Command(LoginCommandExecute);
        }

        private async void LoginCommandExecute(object obj)
        {
            await NavigationService.NavigateAsync("LoginPage");
        }

        private async void RegisterCommandExecute(object obj)
        {
            await NavigationService.NavigateAsync("RegisterIdentificationPage");
        }

        public ICommand RegisterCommand { get; set; }

        public ICommand LoginCommand { get; set; }
    }
}
