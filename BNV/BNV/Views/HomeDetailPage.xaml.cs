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
            this.CurrentPageChanged += CurrentPageHasChanged;
        }

        protected override bool OnBackButtonPressed()
        {
            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.Black;
            return base.OnBackButtonPressed();
        }

        public void CurrentPageHasChanged(object sender, EventArgs e)
        {
            //this.Title = this.CurrentPage.Title;
            //((HomeDetailViewModel)this.BindingContext).Title = this.Title;
        }
    }
}
