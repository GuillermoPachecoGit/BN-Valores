﻿using System;
using System.Collections.Generic;
using Plugin.DeviceOrientation;
using Xamarin.Forms;

namespace BNV.Views
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.Black;

            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.UnlockOrientation();
        }
    }
}
