using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Login";

            SignInCommand = new Command(async () => await SignInActionExecute());
            SignUpCommand = new Command(async () => await SignUpActionExecute());
            RecoveryCommand = new Command(async () => await RecoveryActionExecute());
        }

        private async Task RecoveryActionExecute()
        {
            await NavigationService.NavigateAsync("PasswordRecoveryPage", null, false, false);
        }

        private async Task SignUpActionExecute()
        {
          await  NavigationService.NavigateAsync("RegisterPage",null, false, false);
        }

        private async Task SignInActionExecute()
        {
            await NavigationService.NavigateAsync("HomePage", null, false, false);
        }

        public ICommand SignUpCommand { get; set; }

        public ICommand SignInCommand { get; set; }

        public ICommand RecoveryCommand { get; set; }

        private string _userName;
        public string Username
        {
            get { return _userName; }
            set { _userName = value; RaisePropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(); }
        }
    }
}
