using System;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BNV.Models;
using BNV.ViewModels;
using Plugin.DeviceOrientation;
using Xamarin.Forms;

namespace BNV.Views
{
    public partial class BonoPage : ContentPage
    {
        public BonoPage()
        {
            InitializeComponent();
        }

        void CollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            ((BonoViewModel)this.BindingContext).SelectedItem = (e.CurrentSelection.FirstOrDefault() as ItemBase);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.UnlockOrientation();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);

            try
            {
                if (((BonoViewModel)this.BindingContext).AlreadyLoaded)
                    return;

                using (UserDialogs.Instance.Loading("Cargando datos..."))
                {
                    ((BonoViewModel)this.BindingContext).LoadData();
                }
            }
            catch (Exception ex)
            {
                var val = ex.Message;
            }
        }
    }
}
