using System;
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
            IsMatch = true;
        }

        private async void AcceptCommandExecute(object obj)
        {
            if (string.IsNullOrEmpty(NewPassword.Value) || string.IsNullOrEmpty(ConfirmPassword))
            {
                IsEmpty = true;
                return;
            }

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
                IsMatch = !string.IsNullOrEmpty(value) && value == NewPassword.Value || string.IsNullOrEmpty(value);
            }
        }

        public ICommand AcceptCommand { get; set; }

        Action propChangedCallBack => (AcceptCommand as Command).ChangeCanExecute;

        public bool IsEmpty { get; private set; }

        public bool IsMatch { get; private set; }
    }
}
