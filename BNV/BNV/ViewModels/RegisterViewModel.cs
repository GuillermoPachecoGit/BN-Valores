using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        public RegisterViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            Title = "Register Page";

            AcceptCommand = new Command(async () => await AcceptActionExecute());
        }

        private async Task AcceptActionExecute()
        {
            await NavigationService.NavigateAsync("/RegisterResultPage");
        }

        public ICommand AcceptCommand { get; set; }
    }
}
