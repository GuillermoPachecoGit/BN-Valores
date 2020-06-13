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
            await NavigationService.NavigateAsync("RegisterResultPage");
        }

        public ICommand AcceptCommand { get; set; }

        public ValidatableObject<string> Email { get; }

        public DateTime Birthday  { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string Nationality { get; set; }

    }
}
