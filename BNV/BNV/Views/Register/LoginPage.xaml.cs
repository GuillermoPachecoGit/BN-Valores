using BNV.Interfaces;
using BNV.Models;
using BNV.ViewModels;
using Plugin.DeviceOrientation;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace BNV.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            btnLogin.Clicked += BtnLogin_Clicked;
        }

        private void BtnLogin_Clicked(object sender, System.EventArgs e)
        {
            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            boxIdent.BackgroundColor = Color.White;

            identError.IsVisible = false;
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);

            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.Black;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            password.Unfocus();
        }

        void ComboId_SelectionChanged(System.Object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            boxIdent.BackgroundColor = Color.White;
            identError.IsVisible = false;
            MaskTemplate.Mask = ((IdentificationType)e.Value).Mask;
            var vm = (LoginViewModel)BindingContext;
            vm.IsErrorIdentLenght = false;
        }
    }
}
