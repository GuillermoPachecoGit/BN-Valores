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

            if (IsErrorIdentLenght)
            {
                return;
            }

            IsErrorIdentLenght = false;

            try
            {
                using (UserDialogs.Instance.Loading("Procesando datos.."))
                {
                    var param = new RecoveryPassParam() { TipId = SelectedType.CodIdType.ToString(), Id = Identification.Replace("-", "") };
#if DEBUG
                    param.Id = "502500985";
                    param.TipId = "1";
#endif
                    await App.ApiService.PostRecoverPassword(param)
                   .ContinueWith(async result =>
                   {
                       if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                       {
                           InvalidUser = false;
                           await NavigationService.NavigateAsync("ChangePasswordResultPage", new NavigationParameters() { { "title", Title } });
                       }
                       else if (result.IsFaulted)
                       {
                           InvalidUser = true;
                       }
                       else if (result.IsCanceled) { }
                   }, TaskScheduler.FromCurrentSynchronizationContext());

                }

            }
            catch (System.Exception ex)
            {

            }
        }

        public void OnAppearing()
        {
            MaskWatermark = "Identificación";
            IsErrorLenght = false;
            InvalidUser = false;
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
                IsErrorLenght = false;
                ErrorIdentSize = string.Format(ErrorLenght, LimitSize);
            }
        }

        Action propChangedCallBack => (ValidateCommand as Command).ChangeCanExecute;

        public bool IsErrorIdentLenght { get; set; }

        public bool IsErrorEmpty { get; set; }

        public bool IsErrorLenght { get; set; }
        public bool InvalidUser { get; private set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var title = parameters.GetValue<string>("title");
            Title = !string.IsNullOrEmpty(title) ? title : "Cambio de contraseña";
        }
    }
}
