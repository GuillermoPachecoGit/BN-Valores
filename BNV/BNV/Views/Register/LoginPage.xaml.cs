using BNV.Interfaces;
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

        private void Identification_ValueChanged(object sender, Syncfusion.XForms.MaskedEdit.ValueChangedEventArgs eventArgs)
        {
            var vm = (LoginViewModel)BindingContext;

            if (identification.MaskType == Syncfusion.XForms.MaskedEdit.MaskType.Text) {
                if (identification.Value.ToString().Replace("#", string.Empty).Length  > 0 && identification.Value.ToString().Replace("#", string.Empty).Length < vm?.SelectedType?.Mask.Length)
                {
                    boxIdent.BackgroundColor = Color.FromHex("#FF5B5B");
                    identError.IsVisible = true;
                    vm.IsErrorIdentLenght = true;
                }
                else
                {
                    boxIdent.BackgroundColor = Color.White;
                    identError.IsVisible = false;
                    vm.IsErrorIdentLenght = false;
                }
            }
            else
            {
                if (identification.HasError)
                {
                    boxIdent.BackgroundColor = Color.FromHex("#FF5B5B");
                    identError.IsVisible = true;
                    vm.IsErrorIdentLenght = true;
                }
                else
                {
                    boxIdent.BackgroundColor = Color.White;
                    identError.IsVisible = false;
                    vm.IsErrorIdentLenght = false;
                }
            }
        }

        private void BtnLogin_Clicked(object sender, System.EventArgs e)
        {
            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            ComboId.SelectedIndex = 0;
            boxIdent.BackgroundColor = Color.White;
            identError.IsVisible = false;
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);

            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.Black;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            identification.Unfocus();
            password.Unfocus();
        }

        void ComboId_SelectionChanged(System.Object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            boxIdent.BackgroundColor = Color.White;
            identError.IsVisible = false;
            var vm = (LoginViewModel)BindingContext;
            vm.IsErrorIdentLenght = false;
        }
    }
}
