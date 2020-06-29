using System;
using System.Collections.Generic;
using BNV.ViewModels;
using Plugin.DeviceOrientation;
using Xamarin.Forms;

namespace BNV.Views.Register
{
    public partial class RegisterIdentificationPage : ContentPage
    {
       

        public RegisterIdentificationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);
            boxIdent.BackgroundColor = Color.White;
            identError.IsVisible = false;
        }

        private void Identification_ValueChanged(object sender, Syncfusion.XForms.MaskedEdit.ValueChangedEventArgs eventArgs)
        {
            var vm = (RegisterIdentificationViewModel)BindingContext;

            if (identification.MaskType == Syncfusion.XForms.MaskedEdit.MaskType.Text)
            {
                if (identification.Value.ToString().Replace("#", string.Empty).Length > 0 && identification.Value.ToString().Replace("#", string.Empty).Length < vm?.SelectedType?.Mask.Length)
                {
                    boxIdent.BackgroundColor = Color.FromHex("#FF5B5B");
                    identError.IsVisible = true;
                    vm.IsErrorIdentLenght = true;
                }
                else
                {
                    boxIdent.BackgroundColor = Color.White;
                    identError.IsVisible = false;
                    vm.IsErrorIdentLenght = false;
                }
            }
            else
            {
                if (identification.HasError)
                {
                    boxIdent.BackgroundColor = Color.FromHex("#FF5B5B");
                    identError.IsVisible = true;
                    vm.IsErrorIdentLenght = true;
                }
                else
                {
                    boxIdent.BackgroundColor = Color.White;
                    identError.IsVisible = false;
                    vm.IsErrorIdentLenght = false;
                }
            }
        }

        void ComboId_SelectionChanged(System.Object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            boxIdent.BackgroundColor = Color.White;
            identError.IsVisible = false;
            var vm = (RegisterIdentificationViewModel)BindingContext;
            vm.IsErrorIdentLenght = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
