using System;
using System.Collections.Generic;
using BNV.Models;
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
        }

        void ComboId_SelectionChanged(System.Object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            boxIdent.BackgroundColor = Color.White;
            identError.IsVisible = false;
            MaskTemplate.Mask = ((IdentificationType)e.Value).Mask;
            var vm = (ChangePasswordViewModel)BindingContext;
            vm.IsErrorIdentLenght = false;
        }
    }
}
