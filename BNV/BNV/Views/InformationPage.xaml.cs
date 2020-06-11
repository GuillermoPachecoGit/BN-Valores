using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BNV.Views
{
    public partial class InformationPage : ContentPage
    {
        public InformationPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            group.SelectedItem = group.Items[0];
        }
    }
}
