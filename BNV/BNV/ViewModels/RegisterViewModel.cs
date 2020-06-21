using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BNV.Models;
using BNV.Settings;
using BNV.Validator;
using Prism.AppModel;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class RegisterViewModel : ViewModelBase, IPageLifecycleAware
    {
        private Task<List<Country>> _countries;
        private Task<List<Gender>> _genders;
        private long Identification;
        private long IdentificationType;

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

            valid = true;
        }

        Action propChangedCallBack => (AcceptCommand as Command).ChangeCanExecute;

        private async Task AcceptActionExecute()
        {
            try
            {
                Name.Value = string.IsNullOrEmpty(Name.Value) ? null : Name.Value;
                Surname.Value = string.IsNullOrEmpty(Surname.Value) ? null : Surname.Value;
                PhoneNumber.Value = string.IsNullOrEmpty(PhoneNumber.Value) ? null : PhoneNumber.Value;
                Email.Value = string.IsNullOrEmpty(Email.Value) ? null : Email.Value;

                if (string.IsNullOrEmpty(Email.Value) || string.IsNullOrEmpty(Birthday) || string.IsNullOrEmpty(Name.Value) || string.IsNullOrEmpty(Surname.Value) || string.IsNullOrEmpty(PhoneNumber.Value) || Gender == null || Nationality == null)
                {
                    valid = false;
                    RaisePropertyChanged(nameof(IsMissingField));
                    return;
                }

                valid = true;

                var userParam = new RegisterParam()
                {
                    Name = Name.Value,
                    Birthdate = Birthday,
                    Country = Nationality.CodIdPais,
                    Gender = Gender.CodIdGenero,
                    Email = Email.Value,
                    Phone = PhoneNumber.Value,
                    Tipid = IdentificationType,
                    Id = Identification
                };

                using (UserDialogs.Instance.Loading(MessagesAlert.SendingData))
                {
                    // TODO ADD TOKEN
                    var result = App.ApiService.PostUser(userParam);
                    // TODO que respuesta 
                    await NavigationService.NavigateAsync("RegisterResultPage");
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        public async void OnAppearing()
        {
            ContactInfo = $"Contáctenos {App.ContactInfo}";
            try
            {
                using (UserDialogs.Instance.Loading(MessagesAlert.LoadingData))
                {
                    var getCountries= await App.ApiService.GetCountries().ContinueWith(countries => _countries = countries);
                    var getGenders = await App.ApiService.GetGender().ContinueWith(genders => _genders = genders);
                    await Task.WhenAll(getGenders, getCountries).ContinueWith(result =>
                    {
                        if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                        {
                            Countries = getCountries.Result;
                            Genders = getGenders.Result;
                        }
                        else if (result.IsFaulted)
                        {
                            UserDialogs.Instance.HideLoading();
                        }
                        else if (result.IsCanceled)
                        {
                            UserDialogs.Instance.HideLoading();
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext()).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert(MessagesAlert.AlertServiceError);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var resultIdent = parameters.GetValue<UserVerificationModel>(KeyParams.VerifyParam);
            if (resultIdent != null)
            {
                // TODO que hago con estos datos de la verificacion???
            }
        }

        public void OnDisappearing() { }

        public ICommand AcceptCommand { get; set; }

        public ValidatableObject<string> Email { get; }

        public string Birthday { get; set; }

        public bool DateValid { get; set; }

        public ValidatableObject<string> Surname { get; set; }

        public ValidatableObject<string> Name { get; set; }

        public ValidatableObject<string> PhoneNumber { get; }

        public Gender? Gender { get; set; }

        public Country? Nationality { get; set; }

        public List<Country> Countries { get; set; }

        public List<Gender> Genders { get; set; }

        private string _contact;
        public string ContactInfo
        {
            get => _contact;
            set
            {
                SetProperty(ref _contact, value);
            }
        }

        public bool valid;
        public bool IsMissingField {
            get => valid || !(string.IsNullOrEmpty(Email.Value) || string.IsNullOrEmpty(Birthday) || string.IsNullOrEmpty(Name.Value) || string.IsNullOrEmpty(Surname.Value) || string.IsNullOrEmpty(PhoneNumber.Value) || Gender == null || Nationality == null);
        }
    }
}
