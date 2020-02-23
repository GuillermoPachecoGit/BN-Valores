using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BNV.Validator;
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

            Email = new ValidatableObject<string>
            (propChangedCallBack, new EmailValidator());
            Email.Value = string.Empty;
        }

        Action propChangedCallBack => (ReturnInitCommand as Command).ChangeCanExecute;

        private async Task ReturnActionExecute()
        {
            await NavigationService.GoBackAsync();
        }

        public ICommand ReturnInitCommand { get; set; }

        private async Task ActionExecute()
        {
            //Send the email again

            if (string.IsNullOrEmpty(Email.Value) && Email.IsValid || string.IsNullOrEmpty(Cedula))
            {
                UserDialogs.Instance.Toast("Debe completar todos los campos", TimeSpan.FromSeconds(4));
                return;
            }

            await NavigationService.NavigateAsync("PasswordRecoveryResultPage");
        }

        public ValidatableObject<string> Email { get; }

        public string Cedula { get; set; }

        public ICommand SendPasswordCommand { get; set; }
    }
}
