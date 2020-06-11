﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BNV.Models;
using BNV.ViewModels;
using Plugin.DeviceOrientation;
using Xamarin.Forms;

namespace BNV.Views
{
    public partial class SharesOfStockPage : ContentPage
    {
        public SharesOfStockPage()
        {
            InitializeComponent();
        }

        void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((SharesOfStockViewModel)this.BindingContext).SelectedItem = (e.CurrentSelection.FirstOrDefault() as ItemBase);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);

            try
            {
                if (((SharesOfStockViewModel)this.BindingContext).AlreadyLoaded)
                    return;

                using (UserDialogs.Instance.Loading("Cargando datos..."))
                {
                    ((SharesOfStockViewModel)this.BindingContext).LoadData();
                }
            }
            catch (Exception ex)
            {
                var val = ex.Message;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.UnlockOrientation();
        }
    }
}
