using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using BNV.Settings;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class RegisterRegisteredResultViewModel : ViewModelBase
    {
        private string _pattern = @"(?<=[\w]{4})[\w-\._\+%]*(?=[\w]{1}@)";

        public RegisterRegisteredResultViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            ReturnInitCommand = new Command(async () => await ReturnActionExecute());

        }

        public bool HasEmail { get; set; }

        public string EmailRegistered {get; set;}

        private async Task ReturnActionExecute()
        {
            await CloseSessionActionExecute();
        }

        public ICommand ReturnInitCommand { get; set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var email = parameters.GetValue<string>(KeyParams.EmailRegistered);
            if (!string.IsNullOrEmpty(email))
            {
                HasEmail = true;
                EmailRegistered = Regex.Replace(email, _pattern, m => new string('*', m.Length));
            }
            else
            {
                HasEmail = false;
            }

        }
    }
}
