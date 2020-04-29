using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BNV.ViewModels;
using Xamarin.Forms;

namespace BNV.Views
{
    public partial class HomeDetailPage : TabbedPage
    {
        public HomeDetailPage()
        {
            InitializeComponent();
            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarTextColor = Color.White;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
