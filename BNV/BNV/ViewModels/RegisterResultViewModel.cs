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
        }

        private async Task ReturnActionExecute()
        {
            await NavigationService.NavigateAsync("LoginPage");
        }

        public ICommand ReturnInitCommand { get; set; }
    }
}
