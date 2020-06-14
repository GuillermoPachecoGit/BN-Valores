using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BNV.Validator;
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

            NewPassword = new ValidatableObject<string>(propChangedCallBack, new PasswordValidator())
            {
                Value = string.Empty
            };

            Password = new ValidatableObject<string>(propChangedCallBack, new PasswordValidator())
            {
                Value = string.Empty
            };
            IsMatch = true;
            IsTheSame = false;
        }

        private async Task ChangePasswordActionExecute()
        {

            if (string.IsNullOrEmpty(NewPassword.Value) || string.IsNullOrEmpty(ConfirmPassword) || string.IsNullOrEmpty(Password.Value))
            {
                IsEmpty = true;
                
                return;
            }

            if (NewPassword.Value == Password.Value)
            {
                IsTheSame = true;
                IsEmpty = false;
                return;
            }

            await NavigationService.NavigateAsync("ChangePasswordResultPage");
        }

        public ValidatableObject<string> NewPassword { get; set; }

        public ValidatableObject<string> Password { get; set; }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                SetProperty(ref _confirmPassword, value);
                IsTheSame = NewPassword.Value == Password.Value || string.IsNullOrEmpty(value);
                IsMatch = !string.IsNullOrEmpty(value) && value == NewPassword.Value || string.IsNullOrEmpty(value);
            }
        }

        Action propChangedCallBack => (ChangePasswordCommand as Command).ChangeCanExecute;

        public bool IsEmpty { get; private set; }

        public bool IsMatch { get; private set; }

        public ICommand ChangePasswordCommand { get; set; }
        public bool IsTheSame { get; private set; }
    }
}
