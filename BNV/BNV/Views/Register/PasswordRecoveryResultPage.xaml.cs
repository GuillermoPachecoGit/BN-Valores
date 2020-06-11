﻿using System;
using System.Collections.Generic;
using Plugin.DeviceOrientation;
using Xamarin.Forms;

namespace BNV.Views
{
    public partial class PasswordRecoveryResultPage : ContentPage
    {
        public PasswordRecoveryResultPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.UnlockOrientation();
        }
    }
}
