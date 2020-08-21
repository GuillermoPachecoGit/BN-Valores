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
        private readonly int minSize = 4;
        private int MinSize;
        private int MaxSize;

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
            password.Text = string.Empty;
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
            var vm = (LoginViewModel)BindingContext;

            boxIdent.BackgroundColor = Color.White;
            identError.IsVisible = false;
            MaskTemplate.Mask = ((IdentificationType)e.Value).Mask;
            MaskTemplate.MaxSize = ((IdentificationType)e.Value).Mask.Length;
            MaxSize = ((IdentificationType)e.Value).Mask.Length;
            MinSize = ((IdentificationType)e.Value).Mask.Length;
            var minMax = vm.SelectedType.RegExpression.Split('{', '}');
            if (minMax != null && minMax.Length > 1)
            {
                var values = minMax[1].Split(",");
                if (values != null && values.Length > 1)
                {
                    MinSize = int.Parse(values[0]);
                    MaxSize = int.Parse(values[1]);
                }
            }

            if (!((IdentificationType)e.Value).MaskExpression.Contains('A'))
            {
                identification.Keyboard = Keyboard.Numeric;
                MaskTemplate.IsAlphanumeric = false;

            }
            else
            {
                identification.Keyboard = Keyboard.Plain;
                MaskTemplate.IsAlphanumeric = true;
            }


            vm.IsErrorIdentLenght = false;
        }

        void Entry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var vm = (LoginViewModel)BindingContext;

            if (identification.Text == null || identification.Text.ToString().Length == 0)
            {
                boxIdent.BackgroundColor = Color.White;
                identError.IsVisible = false;
                return;
            }

            if (identification.Text.ToString().Length < MinSize && identification.Text.ToString().Length < vm?.SelectedType?.Mask.Length)
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
}
