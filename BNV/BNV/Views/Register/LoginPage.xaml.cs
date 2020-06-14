using System;
using System.Collections.Generic;
using Plugin.DeviceOrientation;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace BNV.Views
{
    public partial class LoginPage : ContentPage
    {
        private const string Mask15 = "###############";

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ComboId.SelectedIndex = 0;
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);

            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.Black;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.UnlockOrientation();
        }

        void comboBox2_SelectionChanged(System.Object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            IdentValue.Text = string.Empty;
            switch (e.Value)
            {
                case "Cédula de identidad":
                    IdentValue.Placeholder = "0#-####-####";
                    break;
                case "Cédula de residencia":
                    IdentValue.Placeholder = Mask15;// 15 characters
                    break;
                case "Pasaporte":
                    IdentValue.Placeholder = Mask15;
                    break;
                case "Carné de refugiado":
                    IdentValue.Placeholder = Mask15;
                    break;
                case "Carné de pensionado":
                    IdentValue.Placeholder = Mask15;
                    break;
                case "ID físico extranjero":
                    IdentValue.Placeholder = Mask15; // falta definicion
                    break;
                case "DIMEX":
                    IdentValue.Placeholder = "1###########"; // 12 characters
                    break;
                case "DIDI":
                    IdentValue.Placeholder = "5###########";
                    break;
                default:
                    break;
            }
        }
    }
}
