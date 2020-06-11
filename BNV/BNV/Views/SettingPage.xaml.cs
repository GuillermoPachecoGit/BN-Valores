using System;
using System.Collections.Generic;
using BNV.ViewModels;
using Plugin.DeviceOrientation;
using Syncfusion.SfRangeSlider.XForms;
using Xamarin.Forms;

namespace BNV.Views
{
    public partial class SettingPage : ContentPage
    {
        private double StepValue;

        public SettingPage()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            Title = "Configuración";
            StepValue = 1;
        }

        void SfRangeSlider_ValueChanging(System.Object sender, Syncfusion.SfRangeSlider.XForms.ValueEventArgs e)
        {
            var newStep = Math.Round(e.Value / StepValue);
            var vm = BindingContext as SettingViewModel;
            if (vm == null) return;
            var slider = (SfRangeSlider)sender;
            slider.Value = newStep * StepValue;

            switch (slider.Value)
            {
                case 1:
                    vm.TypeChange = $"No notificar";
                    break;
                case 2:
                    vm.TypeChange = $"0.05 colones";
                    break;
                case 3:
                    vm.TypeChange = $"0.10 colones";
                    break;
                case 4:
                    vm.TypeChange = $"0.25 colones";
                    break;
                case 5:
                    vm.TypeChange = $"0.50 colones";
                    break;
                case 6:
                    vm.TypeChange = $"0.75 colones";
                    break;
                case 7:
                    vm.TypeChange = $"1.00 colon";
                    break;
                case 8:
                    vm.TypeChange = $"2.00 colones";
                    break;
                case 9:
                    vm.TypeChange = $"3.00 colones";
                    break;
                case 10:
                    vm.TypeChange = $"4.00 colones";
                    break;
                case 11:
                    vm.TypeChange = $"5.00 colones";
                    break;
                default:
                    break;
            }
        }

        void SfRangeSlider_ValueChanging_1(System.Object sender, Syncfusion.SfRangeSlider.XForms.ValueEventArgs e)
        {
            var newStep = Math.Round(e.Value / StepValue);
            var vm = BindingContext as SettingViewModel;
            if (vm == null) return;
            var slider = (SfRangeSlider)sender;
            slider.Value = newStep * StepValue;
            switch (slider.Value)
            {
                case 1:
                    vm.BonosLabel = $"No notificar";
                    break;
                case 2:
                    vm.BonosLabel = $"0.05%";
                    break;
                case 3:
                    vm.BonosLabel = $"0.10%";
                    break;
                case 4:
                    vm.BonosLabel = $"0.25%";
                    break;
                case 5:
                    vm.BonosLabel = $"0.50%";
                    break;
                case 6:
                    vm.BonosLabel = $"0.75%";
                    break;
                case 7:
                    vm.BonosLabel = $"1.00%";
                    break;
                case 8:
                    vm.BonosLabel = $"2.00%";
                    break;
                case 9:
                    vm.BonosLabel = $"3.00%";
                    break;
                case 10:
                    vm.BonosLabel = $"4.00%";
                    break;
                case 11:
                    vm.BonosLabel = $"5.00%";
                    break;
                default:
                    break;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.UnlockOrientation();
        }

    }
}
