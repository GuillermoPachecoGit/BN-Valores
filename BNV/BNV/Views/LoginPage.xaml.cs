﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BNV.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
        }
    }
}
