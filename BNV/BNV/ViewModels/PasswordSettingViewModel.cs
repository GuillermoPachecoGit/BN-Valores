using System;
using System.Linq;
using System.Windows.Input;
using BNV.Validator;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class PasswordSettingViewModel : ViewModelBase
    {
        public PasswordSettingViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            AcceptCommand = new Command(AcceptCommandExecute);
            Title = "Establecimiento de contraseña";

            NewPassword = new ValidatableObject<string>(propChangedCallBack, new PasswordValidator())
            {
                Value = string.Empty
            };
            IsNotMatch = false;
        }

        private async void AcceptCommandExecute(object obj)
        {
            if (string.IsNullOrEmpty(NewPassword.Value) || string.IsNullOrEmpty(ConfirmPassword))
            {
                IsEmpty = true;
                return;
            }
            IsEmpty = false;

            if (NewPassword.Value.Length < 8 && !NewPassword.Value.ToCharArray().Any(x => char.IsDigit(x)) || !NewPassword.Value.ToCharArray().Any(x => char.IsUpper(x)) || NewPassword.Value.ToLower().ToCharArray().Any(x => x == 'a' || x == 'e' || x == 'i' || x == 'o' || x == 'u'))
            {
                IsNotValid = true;
                return;
            }
            IsNotValid = false;

            if (NewPassword.Value != ConfirmPassword)
            {
                IsNotMatch = true;
                return;
            }
            IsNotMatch = false;

            await NavigationService.NavigateAsync("PasswordSettingResultPage");
        }

        public ValidatableObject<string> NewPassword { get; set; }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                SetProperty(ref _confirmPassword, value);
            }
        }

        public ICommand AcceptCommand { get; set; }

        Action propChangedCallBack => (AcceptCommand as Command).ChangeCanExecute;

        public bool IsEmpty { get; private set; }

        public bool IsNotMatch { get; private set; }
        public bool IsNotValid { get; private set; }
        public bool IsErrorLenght { get; private set; }
    }
}
