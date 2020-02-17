using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class PasswordRecoveryViewModel : ViewModelBase
    {
        public PasswordRecoveryViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            Title = "Register Page";
            ReturnInitCommand = new Command(async () => await ReturnActionExecute());
            SendPasswordCommand = new Command(async () => await ActionExecute());
        }


        private async Task ReturnActionExecute()
        {
            await NavigationService.GoBackAsync();
        }

        public ICommand ReturnInitCommand { get; set; }

        private async Task ActionExecute()
        {
            //Send the email again
            await NavigationService.NavigateAsync("/PasswordRecoveryResultPage");
        }

        public ICommand SendPasswordCommand { get; set; }
    }
}
