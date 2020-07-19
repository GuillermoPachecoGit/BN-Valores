using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BNV.Settings;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class RegisterRegisteredResultViewModel : ViewModelBase
    {
        private string _email = " {0}***********.com, ";

        public RegisterRegisteredResultViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            ReturnInitCommand = new Command(async () => await ReturnActionExecute());

        }

        public bool HasEmail { get; set; }

        public string EmailRegistered {get; set;}

        private async Task ReturnActionExecute()
        {
            await NavigationService.NavigateAsync("LoginPage");
        }

        public ICommand ReturnInitCommand { get; set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var email = parameters.GetValue<string>(KeyParams.EmailRegistered);
            if (!string.IsNullOrEmpty(email))
            {
                HasEmail = true;
                EmailRegistered = string.Format(_email, email.Substring(0, 4));
            }
            else
            {
                HasEmail = false;
            }

        }
    }
}
