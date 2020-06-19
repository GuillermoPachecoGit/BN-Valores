using System;
using System.Collections.Generic;
using System.Windows.Input;
using BNV.Models;
using BNV.Validator;
using Prism.AppModel;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class RegisterIdentificationViewModel : ViewModelBase, IPageLifecycleAware
    {
        private const string ErrorLenght = "El identificador debe estar completo.";
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

            if (Identification.Replace("#", string.Empty).Length < MaskWatermark?.Length)
            {
                IsErrorIdentLenght = true;
                return;
            }

            IsErrorIdentLenght = false;

            await NavigationService.NavigateAsync("RegisterPage");
        }

        public void OnAppearing()
        {
            MaskWatermark = "Identificación";
            IsErrorEmpty = false;
            //call to api
            IdentificationTypes = new List<IdentificationType>()
            {
                new IdentificationType()
                {
                    Description = "Cédula de identidad",
                    MaskWatermark = "0#-####-####",
                    Mask = "0#-####-####",
                },
                new IdentificationType()
                {
                    Description = "Cédula de residencia",
                    MaskWatermark = "###############",
                    Mask = "AAAAAAAAAAAAAAA",
                },
                new IdentificationType()
                {
                    Description = "Pasaporte",
                    MaskWatermark = "###############",
                    Mask = "AAAAAAAAAAAAAAA",
                },
                new IdentificationType()
                {
                    Description = "Carné de refugiado",
                    MaskWatermark = "###############",
                    Mask = "AAAAAAAAAAAAAAA",
                },
                new IdentificationType()
                {
                    Description = "Carné de pensionado",
                    MaskWatermark = "###############",
                    Mask = "AAAAAAAAAAAAAAA",
                },
                new IdentificationType()
                {
                    Description = "DIMEX",
                    MaskWatermark = "1###########",
                    Mask = "1###########"

                },
                new IdentificationType()
                {
                    Description = "DIDI",
                    MaskWatermark = "5###########",
                    Mask = "5###########"
                }
            };
        }

        public void OnDisappearing()
        {

        }

        public ICommand ValidateCommand { get; set; }

        private string _identification;
        public string Identification
        {
            get => _identification;
            set {
                SetProperty(ref _identification, value);
                // add logic to validate RegEx
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
                MaskWatermark = value.MaskWatermark;
                //RegEx = value.RegEx;
                Identification = string.Empty;
                IsErrorEmpty = false;
                ErrorIdentSize = string.Format(ErrorLenght, LimitSize);
            }
        }

        Action propChangedCallBack => (ValidateCommand as Command).ChangeCanExecute;

        public bool IsErrorEmpty{ get; private set; }
        public bool IsErrorIdentLenght { get; private set; }
    }
}
