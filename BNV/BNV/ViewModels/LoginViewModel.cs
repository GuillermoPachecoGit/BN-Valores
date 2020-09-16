using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BNV.Models;
using BNV.Settings;
using Prism.AppModel;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class LoginViewModel : ViewModelBase, IPageLifecycleAware
    {
        private const string Label = "Login";
        private const string PlaceHolder = "Identificación";


        public LoginViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            base.Title = Label;
            SignInCommand = new Command(async () => await SignInActionExecute());
            SignUpCommand = new Command(async () => await SignUpActionExecute());
            RecoveryCommand = new Command(async () => await RecoveryActionExecute());
            IsErrorEmpty = false;
            IsErrorLenght = false;
            IsErrorIdentLenght = false;
        }

        private async Task RecoveryActionExecute()
        {
            await NavigationService.NavigateAsync("ChangePasswordPage", new NavigationParameters() { { "title", "Establecimiento de contraseña" } }, false, false);
        }

        private async Task SignUpActionExecute()
        {
          await  NavigationService.NavigateAsync("RegisterIdentificationPage", null, false, false);
        }

        private async Task SignInActionExecute()
        {
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Identification))
            {
                IsErrorEmpty = true;
                return;
            }
            IsErrorEmpty = false;

            if (IsErrorIdentLenght)
            {
                return;
            }
            IsErrorIdentLenght = false;

            if (Password.Length < 8)
            {
                IsErrorLenght = true;
                return;
            }
            IsErrorLenght = false;

                try
                {
                    using (UserDialogs.Instance.Loading(MessagesAlert.InitSession))
                    {
                        try
                        {
                            using (UserDialogs.Instance.Loading(MessagesAlert.SendingData))
                            {
                                var loginParam = new LoginParam()
                                {
                                    TipId = SelectedType.CodIdType,
                                    Id = Identification.Replace("-", ""),
                                    Password = Password
                                };

                                var token = await App.ApiService.PostLogin(loginParam).ContinueWith(async result =>
                                {
                                    if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                                    {
                                        await SecureStorage.SetAsync(Config.Token, result.Result.AccessToken);
                                        await SecureStorage.SetAsync(Config.TokenExpiration, result.Result.ExpiresIn.ToString());
                                        await SecureStorage.SetAsync(Config.Password, Password);
                                        IsErrorEmpty = false;

                                        if (result.Result.IsTemp == 1)
                                        {
                                            await NavigationService.NavigateAsync("PasswordSettingPage");
                                        }
                                        else
                                        {
                                            await SentDeviceId(result.Result.AccessToken);
                                            App.WithAuthentication = true;
                                            App.Identification = Identification.Replace("-", "");
                                            await NavigationService.NavigateAsync("HomePage");
                                        }
                                        Password = string.Empty;
                                    }
                                    else if (result.IsFaulted)
                                    {
                                        IsErrorLenght = true;
                                    }
                                    else if (result.IsCanceled) { }
                                }, TaskScheduler.FromCurrentSynchronizationContext());
                            }
                        }
                        catch (Exception ex)
                        {
                            IsErrorLenght = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    var val = ex.Message;
                }
        }

        private async Task SentDeviceId(string accessToken)
        {
            var userDevice = new UserDeviceParam(App.DeviceIdFirebase);
            await App.ApiService.UserDevice($"Bearer {accessToken}", userDevice).ContinueWith(result =>
                {
                    if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                    {
                    }
                    else if (result.IsFaulted)
                    {
                    }
                    else if (result.IsCanceled)
                    { }
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void OnAppearing()
        {
            MaskWatermark = PlaceHolder;
            IsErrorIdentLenght = false;
            IdentificationTypes = App.IdentificationTypes;
            if (SelectedType != null)
                return;

            SelectedType = null;
            if (IdentificationTypes != null)
                SelectedType = IdentificationTypes[0];

            ContactInfo = $"Contáctenos {App.ContactInfo}";
        }

        public void OnDisappearing(){
            IsErrorIdentLenght = false;
            IsErrorLenght = false;
            IsErrorEmpty = false;
        }

        public ICommand SignUpCommand { get; set; }

        public ICommand SignInCommand { get; set; }

        public ICommand RecoveryCommand { get; set; }

        public string Password { get; set; }

        private string _identification;
        public string Identification
        {
            get => _identification;
            set
            {
                SetProperty(ref _identification, value);
            }
        }

        public int LimitSize { get; set; }

        private string _mask;
        public string MaskTemplate
        {
            get => _mask;
            set
            {
                SetProperty(ref _mask, value);
            }
        }

        private string _contact;
        public string ContactInfo
        {
            get => _contact;
            set
            {
                SetProperty(ref _contact, value);
            }
        }

        public string MaskWatermark { get; private set; }

        public Syncfusion.XForms.MaskedEdit.MaskType MaskType { get; set; }

        public string RegEx { get; set; }

        public string ErrorIdentSize { get; set; }

        public List<IdentificationType> IdentificationTypes { get; set; }

        private IdentificationType _selectedType;

        public IdentificationType? SelectedType
        {
            get => _selectedType;

            set
            {
                if (value != null)
                {
                    SetProperty(ref _selectedType, value);
                    Identification = string.Empty;
                    LimitSize = value.Mask.Length;
                    MaskTemplate = value.RegExpression;
                    MaskWatermark = value.Mask;
                    MaskType = value.MaskType;
                    IsErrorIdentLenght = false;
                }
            }
        }

        public bool IsErrorIdentLenght { get; set; }

        public bool IsErrorEmpty { get;  set; }

        public bool IsErrorLenght { get;  set; }
    }
}
