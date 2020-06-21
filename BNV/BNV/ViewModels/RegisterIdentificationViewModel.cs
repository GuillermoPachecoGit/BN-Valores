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

            if (Identification.Replace(CharacterDefault, string.Empty).Length < MaskWatermark?.Length)
            {
                IsErrorIdentLenght = true;
                return;
            }

            IsErrorIdentLenght = false;

            using (UserDialogs.Instance.Loading("Verificando datos..."))
            {
                await App.ApiService.GetVerifyUser(new UserVerifyParam() { IdentificationType = SelectedType.CodIdType, Identification = Identification })
                   .ContinueWith(async result =>
                   {
                       if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                       {
                           // TODO VERIFY BAD RESPONSE
                           // TODO si tiene email, y cuenta como se debe completar la etapa de registro
                           await NavigationService.NavigateAsync("RegisterPage", new NavigationParameters() { { KeyParams.VerifyParam, result.Result } });
                       }
                       else if (result.IsFaulted) { }
                       else if (result.IsCanceled) { }
                   }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        public void OnAppearing()
        {
            MaskWatermark = Placeholder;
            IsErrorEmpty = false;
            IdentificationTypes = App.IdentificationTypes;
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
                IsErrorEmpty = false;
                ErrorIdentSize = string.Format(ErrorLenght, LimitSize);
            }
        }

        public bool IsErrorEmpty{ get; private set; }
        public bool IsErrorIdentLenght { get; private set; }
    }
}
