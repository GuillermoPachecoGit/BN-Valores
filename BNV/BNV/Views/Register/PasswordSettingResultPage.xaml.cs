using System;
using System.Collections.Generic;
using Plugin.DeviceOrientation;
using Xamarin.Forms;

namespace BNV.Views.Register
{
    public partial class PasswordSettingResultPage : ContentPage
    {
        public PasswordSettingResultPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);

            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.Black;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.UnlockOrientation();
        }
    }
}
