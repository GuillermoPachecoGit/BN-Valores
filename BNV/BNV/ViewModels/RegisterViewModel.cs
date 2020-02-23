using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BNV.Validator;
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
            Email = new ValidatableObject<string>
            (propChangedCallBack, new EmailValidator());
            Email.Value = string.Empty;
        }

        Action propChangedCallBack => (AcceptCommand as Command).ChangeCanExecute;

        private async Task AcceptActionExecute()
        {
            if (string.IsNullOrEmpty(Email.Value) && Email.IsValid || string.IsNullOrEmpty(Cedula) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(PhoneNumber))
            {
                UserDialogs.Instance.Toast("Debe completar todos los campos", TimeSpan.FromSeconds(4));
                return;
            }

            await NavigationService.NavigateAsync("RegisterResultPage");
        }

        public ICommand AcceptCommand { get; set; }

        public ValidatableObject<string> Email { get; }

        public string Cedula { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
