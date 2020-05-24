using System;
using System.Collections.Generic;
using BNV.ViewModels;
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
                    vm.Bonos = $"No notificar";
                    break;
                case 2:
                    vm.Bonos = $"0.05%";
                    break;
                case 3:
                    vm.Bonos = $"0.10%";
                    break;
                case 4:
                    vm.Bonos = $"0.25%";
                    break;
                case 5:
                    vm.Bonos = $"0.50%";
                    break;
                case 6:
                    vm.Bonos = $"0.75%";
                    break;
                case 7:
                    vm.Bonos = $"1.00%";
                    break;
                case 8:
                    vm.Bonos = $"2.00%";
                    break;
                case 9:
                    vm.Bonos = $"3.00%";
                    break;
                case 10:
                    vm.Bonos = $"4.00%";
                    break;
                case 11:
                    vm.Bonos = $"5.00%";
                    break;
                default:
                    break;
            }
        }

        void SfComboBox_SelectionChanged(System.Object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
        }

    }
}
