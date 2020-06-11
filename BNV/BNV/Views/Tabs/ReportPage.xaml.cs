using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BNV.ViewModels;
using Plugin.DeviceOrientation;
using Xamarin.Forms;

namespace BNV.Views.Tabs
{
    public partial class ReportPage : ContentPage
    {
        public ReportPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);

            try
            {
                if (((ReportViewModel)this.BindingContext).AlreadyLoaded)
                    return;

                using (UserDialogs.Instance.Loading("Cargando datos..."))
                {
                  //  ((ReportViewModel)this.BindingContext).LoadData();
                }
            }
            catch (Exception ex)
            {
                var val = ex.Message;
            }
        }
    }
}
