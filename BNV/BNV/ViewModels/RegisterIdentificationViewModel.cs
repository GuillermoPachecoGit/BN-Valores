using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BNV.Models;
using BNV.Settings;
using Prism.AppModel;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class RegisterIdentificationViewModel : ViewModelBase, IPageLifecycleAware
    {
        private const string ErrorLenght = "El identificador debe estar completo.";
        private const string Placeholder = "Identificación";
        private const string CharacterDefault = "#";

        public RegisterIdentificationViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            ValidateCommand = new Command(ValidateCommandExecute);
        }

        private async void ValidateCommandExecute(object obj)
        {

            if (string.IsNullOrEmpty(Identification) || SelectedType == null)
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

            using (UserDialogs.Instance.Loading("Verificando datos..."))
            {
                try
                {
                    var param = new UserVerifyParam() { IdentificationType = SelectedType.CodIdType, Identification = Identification.Replace("-", "") };
//#if DEBUG
//                    param.Identification = "502500985";
//                    param.IdentificationType = 1;
//#endif
                    await App.ApiService.GetVerifyUser(param)
                   .ContinueWith(async result =>
                   {
                       if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                       {
                           // TODO el email en caso del que el usuario tenga un email valido???
                           if (result.Result.EsCliOAutor == 1)
                           {
                               if (result.Result.TieneCorreo == 1)
                                   await NavigationService.NavigateAsync("RegisterRegisteredResultPage", new NavigationParameters() { { KeyParams.EmailRegistered, result.Result.Correo } });
                               else
                                   await NavigationService.NavigateAsync("RegisterRegisteredResultPage", new NavigationParameters() { { KeyParams.EmailRegistered, result.Result.Correo } });
                           }
                           else
                                await NavigationService.NavigateAsync("RegisterPage", new NavigationParameters() { { KeyParams.VerifyParam, param } });
                       }
                       else if (result.IsFaulted) {

                       }
                       else if (result.IsCanceled) { }
                   }, TaskScheduler.FromCurrentSynchronizationContext());
                }
                catch (System.Exception ex)
                {

                }

            }
        }

        public void OnAppearing()
        {
            MaskWatermark = Placeholder;
            IsErrorEmpty = false;
            IdentificationTypes = App.IdentificationTypes;
            SelectedType = IdentificationTypes[0];
            ContactInfo = $"Contáctenos {App.ContactInfo}";
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

        public void OnDisappearing() { }

        public ICommand ValidateCommand { get; set; }

        private string _identification;
        public string Identification
        {
            get => _identification;
            set {
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

        public string? MaskWatermark { get; private set; }
        public Syncfusion.XForms.MaskedEdit.MaskType MaskType { get; private set; }
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
                Identification = string.Empty;
                LimitSize = value.Mask.Length;
                MaskTemplate = value.RegExpression;
                MaskWatermark = value.Mask;
                MaskType = value.MaskType;
                IsErrorEmpty = false;
                ErrorIdentSize = string.Format(ErrorLenght, LimitSize);
            }
        }

        public bool IsErrorEmpty{ get; private set; }

        public bool IsErrorIdentLenght { get; set; }
    }
}
