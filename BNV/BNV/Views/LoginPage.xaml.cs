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
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}
