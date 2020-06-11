using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BNV.ViewModels;
using Plugin.DeviceOrientation;
using Xamarin.Forms;

namespace BNV.Views
{
    public partial class ChangeTypePage : ContentPage
    {
        public ChangeTypePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            list.SelectedItem = null;
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);

            try
            {
                if (((ChangeTypeViewModel)this.BindingContext).AlreadyLoaded)
                    return;

                using (UserDialogs.Instance.Loading("Cargando datos..."))
                {
                    ((ChangeTypeViewModel)this.BindingContext).LoadData();
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
