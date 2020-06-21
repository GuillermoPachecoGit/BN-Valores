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
    public class ChangePasswordViewModel : ViewModelBase, IPageLifecycleAware
    {
        private const string ErrorLenght = "El identificador debe estar completo.";

        public ChangePasswordViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            ValidateCommand = new Command(ValidateCommandExecute);
            Title = "Cambio de contraseña";
        }

        private async void ValidateCommandExecute(object obj)
        {
            if (string.IsNullOrEmpty(Identification))
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

             await SecureStorage.SetAsync(Config.FirstLogin, "n");
             await NavigationService.NavigateAsync("ChangePasswordResultPage", new NavigationParameters() { { "title", Title } });
        }

        public void OnAppearing()
        {
            MaskWatermark = "Identificación";
            IsErrorLenght = false;
            IdentificationTypes = App.IdentificationTypes;
        }

        public void OnDisappearing() { }

        public ICommand ValidateCommand { get; set; }

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
                IsErrorLenght = false;
                ErrorIdentSize = string.Format(ErrorLenght, LimitSize);
            }
        }

        Action propChangedCallBack => (ValidateCommand as Command).ChangeCanExecute;

        public bool IsErrorIdentLenght { get; set; }

        public bool IsErrorEmpty { get; set; }

        public bool IsErrorLenght { get; set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var title = parameters.GetValue<string>("title");
            Title = !string.IsNullOrEmpty(title) ? title : "Cambio de contraseña";
        }
    }
}
