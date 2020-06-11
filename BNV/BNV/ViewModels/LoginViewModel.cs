using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BNV.Validator;
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

            Email = new ValidatableObject<string>
            (propChangedCallBack, new EmailValidator());
            Email.Value = string.Empty;

            Password = new ValidatableObject<string>
            (propChangedCallBack, new PasswordValidator());
            Password.Value = string.Empty;
        }

        Action propChangedCallBack => (SignInCommand as Command).ChangeCanExecute;

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
            if (string.IsNullOrEmpty(Email.Value) && Email.IsValid || string.IsNullOrEmpty(Password.Value) && Password.IsValid)
            {
                UserDialogs.Instance.Toast("Debe completar todos los campos", TimeSpan.FromSeconds(4));
                return;
            }

            Device.BeginInvokeOnMainThread(async () =>
            {

                try
                {
                    using (UserDialogs.Instance.Loading("Iniciando Sesión..."))
                    {
                        await Task.Delay(5);
                        await NavigationService.NavigateAsync("HomePage");
                    }
                }
                catch (Exception ex)
                {
                    var val = ex.Message;
                }
            });

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

        public ValidatableObject<string> Email { get; }

        public ValidatableObject<string> Password { get; }
    }
}
