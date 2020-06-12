using System;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class RegisterIdentificationViewModel : ViewModelBase
    {
        public RegisterIdentificationViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            ValidateCommand = new Command(ValidateCommandExecute);
        }

        private async void ValidateCommandExecute(object obj)
        {
            await NavigationService.NavigateAsync("RegisterPage");
        }

        public ICommand ValidateCommand { get; set; }
    }
}
