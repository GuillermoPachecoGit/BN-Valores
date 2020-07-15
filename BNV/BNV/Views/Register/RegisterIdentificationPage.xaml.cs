using System;
using System.Collections.Generic;
using BNV.Models;
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
        }

        void ComboId_SelectionChanged(System.Object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            boxIdent.BackgroundColor = Color.White;
            identError.IsVisible = false;
            MaskTemplate.Mask = ((IdentificationType)e.Value).Mask;
            var vm = (RegisterIdentificationViewModel)BindingContext;
            vm.IsErrorIdentLenght = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
