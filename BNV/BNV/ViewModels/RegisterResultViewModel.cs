using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BNV.Validator;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class RegisterResultViewModel : ViewModelBase
    {
        public RegisterResultViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            ReturnInitCommand = new Command(async () => await ReturnActionExecute());
            RequestPasswordAgainCommand = new Command(async () => await RequestPasswordAgainActionExecute());
        }

        private async Task ReturnActionExecute()
        {
            await NavigationService.NavigateAsync("LoginPage");
        }

        public ICommand ReturnInitCommand { get; set; }

        private async Task RequestPasswordAgainActionExecute()
        {
            //Send the email again
        }

        public ICommand RequestPasswordAgainCommand { get; set; }
    }
}
