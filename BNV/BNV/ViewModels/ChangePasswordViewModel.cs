using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class ChangePasswordViewModel : ViewModelBase
    {
        public ChangePasswordViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Cambio de contraseña";

            ChangePasswordCommand = new Command(async () => await ChangePasswordActionExecute());
        }

        private async Task ChangePasswordActionExecute()
        {
            await NavigationService.GoBackAsync();
        }

        public ICommand ChangePasswordCommand { get; set; }

    }
}
