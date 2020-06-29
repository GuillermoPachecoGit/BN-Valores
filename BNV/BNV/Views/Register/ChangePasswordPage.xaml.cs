using System;
using System.Collections.Generic;
using BNV.ViewModels;
using Plugin.DeviceOrientation;
using Xamarin.Forms;

namespace BNV.Views.Register
{
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);

            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#AFBC24");
            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarTextColor = Color.White;
            boxIdent.BackgroundColor = Color.White;
            identError.IsVisible = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void Identification_ValueChanged(object sender, Syncfusion.XForms.MaskedEdit.ValueChangedEventArgs eventArgs)
        {
            var vm = (ChangePasswordViewModel)BindingContext;

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
            var vm = (LoginViewModel)BindingContext;
            vm.IsErrorIdentLenght = false;
        }
    }
}
