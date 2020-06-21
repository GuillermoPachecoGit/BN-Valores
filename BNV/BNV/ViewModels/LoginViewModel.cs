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

            if (Identification.Replace("#", string.Empty).Length < MaskWatermark.Length)
            {
                IsErrorIdentLenght = true;
                return;
            }

            IsErrorIdentLenght = false;
            if (Password.Length < 8)
            {
                IsErrorLenght = true;
                return;
            }
            IsErrorLenght = false;
            Device.BeginInvokeOnMainThread(async () =>
            {
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
                                    Id = Identification,
                                    Password = Password
                                };
                                //TODO LOGIN ESTA EN OTRA DIRECCION URLBASE/Login, esto debe ser uniforme,como los demas endpoints urlbase/api/Login
                                //var token = await App.ApiService.PostLogin(loginParam);
                                await NavigationAction();
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    var val = ex.Message;
                }
            });
        }

        private async Task NavigationAction()
        {
            var firstLogin = await SecureStorage.GetAsync(Config.FirstLogin);
            if (string.IsNullOrEmpty(firstLogin) || firstLogin == "n")
            {
                await SecureStorage.SetAsync(Config.FirstLogin, "y");
                await NavigationService.NavigateAsync("PasswordSettingPage");
            }
            else
            {
                await NavigationService.NavigateAsync("HomePage");
            }
        }

        public void OnAppearing()
        {
            MaskWatermark = PlaceHolder;
            IsErrorIdentLenght = false;
            IdentificationTypes = App.IdentificationTypes;
            ContactInfo = $"Contáctenos {App.ContactInfo}";
        }

        public void OnDisappearing(){}

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
        public string RegEx { get; set; }

        public string ErrorIdentSize { get; set; }

        public List<IdentificationType> IdentificationTypes { get; set; }

        private IdentificationType _selectedType;

        public IdentificationType SelectedType
        {
            get => _selectedType;

            set
            {
                SetProperty(ref _selectedType, value);
                LimitSize = value.Mask.Length;
                MaskTemplate = value.Mask;
                MaskWatermark = value.Mask;
                Identification = string.Empty;
                IsErrorIdentLenght = false;
            }
        }

        public bool IsErrorIdentLenght { get; set; }

        public bool IsErrorEmpty { get;  set; }

        public bool IsErrorLenght { get;  set; }
    }
}
