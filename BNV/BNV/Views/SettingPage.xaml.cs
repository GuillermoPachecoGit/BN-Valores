using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BNV.Views
{
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            Title = "Configuración";
        }

        protected override void OnAppearing()
        {
            
            base.OnAppearing();
        }
    }
}
