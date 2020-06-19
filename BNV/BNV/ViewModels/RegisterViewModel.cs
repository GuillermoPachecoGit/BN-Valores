using System;
using System.Threading.Tasks;
using System.Windows.Input;
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
            Email = new ValidatableObject<string>(propChangedCallBack, new EmailValidator())
            {
                Value = string.Empty
            };
            PhoneNumber = new ValidatableObject<string>(propChangedCallBack, new PhoneValidator())
            {
                Value = string.Empty
            };
            Name = new ValidatableObject<string>(propChangedCallBack, new EmptyValidator())
            {
                Value = string.Empty
            };
            Surname = new ValidatableObject<string>(propChangedCallBack, new EmptyValidator())
            {
                Value = string.Empty
            };

            Gender = new ValidatableObject<string>(propChangedCallBack, new EmptyValidator())
            {
                Value = string.Empty
            };

            Nationality = new ValidatableObject<string>(propChangedCallBack, new EmptyValidator())
            {
                Value = string.Empty
            };

            valid = true;
        }

        Action propChangedCallBack => (AcceptCommand as Command).ChangeCanExecute;

        private async Task AcceptActionExecute()
        {
            Name.Value = string.IsNullOrEmpty(Name.Value) ? null : Name.Value;
            Surname.Value = string.IsNullOrEmpty(Surname.Value) ? null : Surname.Value;
            Nationality.Value = string.IsNullOrEmpty(Nationality.Value) ? null : Nationality.Value;
            PhoneNumber.Value = string.IsNullOrEmpty(PhoneNumber.Value) ? null : PhoneNumber.Value;
            Gender.Value = string.IsNullOrEmpty(Gender.Value) ? null : Gender.Value;
            Email.Value = string.IsNullOrEmpty(Email.Value) ? null : Email.Value;

            if (string.IsNullOrEmpty(Email.Value) || string.IsNullOrEmpty(Birthday) || string.IsNullOrEmpty(Name.Value) || string.IsNullOrEmpty(Surname.Value) || string.IsNullOrEmpty(PhoneNumber.Value) || string.IsNullOrEmpty(Gender.Value) || string.IsNullOrEmpty(Nationality.Value))
            {
                valid = false;
                RaisePropertyChanged(nameof(IsMissingField));
                return;
            }

            valid = true;
            await NavigationService.NavigateAsync("RegisterResultPage");
        }

        public ICommand AcceptCommand { get; set; }

        public ValidatableObject<string> Email { get; }

        public string Birthday { get; set; }

        public bool DateValid { get; set; }

        public ValidatableObject<string> Surname { get; set; }

        public ValidatableObject<string> Name { get; set; }

        public ValidatableObject<string> PhoneNumber { get; }

        public ValidatableObject<string> Gender { get; set; }

        public ValidatableObject<string> Nationality { get; set; }

        public bool valid;
        public bool IsMissingField {
            get => (valid || !((string.IsNullOrEmpty(Email.Value) || string.IsNullOrEmpty(Birthday) || string.IsNullOrEmpty(Name.Value) || string.IsNullOrEmpty(Surname.Value) || string.IsNullOrEmpty(PhoneNumber.Value) || string.IsNullOrEmpty(Gender.Value) || string.IsNullOrEmpty(Nationality.Value))));
        }
    }
}
