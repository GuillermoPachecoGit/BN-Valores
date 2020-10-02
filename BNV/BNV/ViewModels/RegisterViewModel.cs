using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BNV.Models;
using BNV.Settings;
using BNV.Validator;
using Newtonsoft.Json;
using Prism.AppModel;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class RegisterViewModel : ViewModelBase, IPageLifecycleAware
    {
        private Task<List<Country>> _countries;
        private Task<List<Gender>> _genders;
        private string Identification;
        private long IdentificationType;

        public RegisterViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            Title = "Register Page";

            AcceptCommand = new Command(async () => await AcceptActionExecute());
            Email = new ValidatableObject<string>(null, new EmailValidator())
            {
                Value = string.Empty
            };
            Name = new ValidatableObject<string>(null, new EmptyValidator())
            {
                Value = string.Empty
            };
            Surname = new ValidatableObject<string>(null, new EmptyValidator())
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
                if (string.IsNullOrEmpty(Email.Value) || string.IsNullOrEmpty(Birthday) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrEmpty(Name.Value) || string.IsNullOrEmpty(Surname.Value) || Gender == null || Nationality == null)
                {
                    valid = false;
                    RaisePropertyChanged(nameof(IsMissingField));
                    return;
                }

                valid = true;


                Name.Value = string.IsNullOrEmpty(Name.Value) ? null : Name.Value;
                Surname.Value = string.IsNullOrEmpty(Surname.Value) ? null : Surname.Value;
                Email.Value = string.IsNullOrEmpty(Email.Value) ? null : Email.Value;

                // check valid date
                var values = Birthday.Split('/').Select(x => int.Parse(x)).ToList();

                if (Birthday.Length != 10)
                {
                    DateInvalid = true;
                    return;
                }

                if ((values[0] != 0 && values[0] > 31) || (values[1] != 0 && values[1] > 12) || (values[2] != 0 && values[2] > DateTime.Now.Year))
                {
                    DateInvalid = true;
                    return;
                }

                DateTime parse;
                bool validDate = DateTime.TryParseExact(Birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parse);
                if (!validDate || DateTime.ParseExact(Birthday, "dd/MM/yyyy", new CultureInfo("es-MX")) > DateTime.Now)
                {
                    DateInvalid = true;
                    return;
                }

                DateInvalid = false;

                var userParam = new RegisterParam()
                {
                    Name = Name.Value,
                    LastName = Surname.Value,
                    Birthdate = Birthday,
                    Country = Nationality.CodIdPais,
                    Gender = Gender.CodIdGenero,
                    Email = Email.Value,
                    Phone = PhoneNumber,
                    Tipid = IdentificationType,
                    Id = Identification
                };

                using (UserDialogs.Instance.Loading(MessagesAlert.SendingData))
                {
                    var jsonData = JsonConvert.SerializeObject(userParam);
                    var result = App.ApiService.PostUser(userParam).ContinueWith(async result =>
                    {
                        if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                        {
                            AlreadyExist = false;
                            await NavigationService.NavigateAsync("RegisterResultPage");
                        }
                        else if (result.IsFaulted)
                        {
                            AlreadyExist = true;
                        }
                        else if (result.IsCanceled) { AlreadyExist = true; }
                    }, TaskScheduler.FromCurrentSynchronizationContext());
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
            var resultIdent = parameters.GetValue<UserVerifyParam>(KeyParams.VerifyParam);
            if (resultIdent != null)
            {
                // TODO que hago con estos datos de la verificacion???
                Identification = resultIdent.Identification;
                IdentificationType = resultIdent.IdentificationType;
            }
        }

        public void OnDisappearing() { }

        public ICommand AcceptCommand { get; set; }

        public ValidatableObject<string> Email { get; }

        public string Birthday { get; set; }

        public bool DateValid { get; set; }

        public ValidatableObject<string> Surname { get; set; }

        public ValidatableObject<string> Name { get; set; }

        private List<char> _validCharacters = new List<char> { '-', '(', ')' };
        private string _phone;
        public string PhoneNumber
        {
            get => _phone;

            set
            {
                _phone = value;
                if (value == string.Empty)
                {
                    IsInvalidPhone = false;
                    return;
                }

                if (value.ToCharArray().Any(x => !_validCharacters.Contains(x) && !char.IsDigit(x) && !char.IsWhiteSpace(x)))
                {
                    ErrorDescriptionPhone = MessagesAlert.ErrorPhoneInvalid;
                    IsInvalidPhone = true;
                    IsInvalidPhone = true;
                    return;
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    ErrorDescriptionPhone = MessagesAlert.ErrorPhoneInvalid;
                    IsInvalidPhone = false;
                    IsInvalidPhone = false;
                    return;
                }

                if (value.Length > 20)
                {
                    ErrorDescriptionPhone = MessagesAlert.ErrorPhoneLenght;
                    IsInvalidPhone = true;
                    IsInvalidPhone = true;
                    return;
                }

                IsInvalidPhone = false;
            }
        }

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

        public bool IsInvalidPhone { get; set; }

        public string ErrorDescriptionPhone { get; set; }

        public bool valid;
        public bool IsMissingField {
            get => valid || !(string.IsNullOrEmpty(Email.Value) || string.IsNullOrEmpty(Birthday) || string.IsNullOrEmpty(Name.Value) || string.IsNullOrEmpty(Surname.Value) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrWhiteSpace(PhoneNumber) || Gender == null || Nationality == null);
        }

        public bool DateInvalid { get; private set; }

        public bool AlreadyExist { get; private set; }
    }
}
