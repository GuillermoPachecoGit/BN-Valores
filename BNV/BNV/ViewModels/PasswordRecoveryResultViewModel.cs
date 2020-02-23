using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class PasswordRecoveryResultViewModel : ViewModelBase
    {
        public PasswordRecoveryResultViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            Title = "Register Page";
            ReturnInitCommand = new Command(async () => await ReturnActionExecute());
            RequestPasswordAgainCommand = new Command(async () => await RequestPasswordAgainActionExecute());
        }

        private async Task ReturnActionExecute()
        {
            await NavigationService.GoBackToRootAsync();
        }

        public ICommand ReturnInitCommand { get; set; }

        private async Task RequestPasswordAgainActionExecute()
        {
            //Send the email again
        }

        public ICommand RequestPasswordAgainCommand { get; set; }
    }
}
