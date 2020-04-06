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

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void SfRangeSlider_ValueChanging(System.Object sender, Syncfusion.SfRangeSlider.XForms.ValueEventArgs e)
        {
            var newStep = Math.Round(e.Value / StepValue);
            var vm = BindingContext as SettingViewModel;
            if (vm == null) return;
            var slider = (SfRangeSlider)sender;
            slider.Value = newStep * StepValue;
            if (slider.Value == 1)
                vm.TypeChange = $"{slider.Value} colón";
            else
                vm.TypeChange = $"{slider.Value} colones";
        }

        void SfRangeSlider_ValueChanging_1(System.Object sender, Syncfusion.SfRangeSlider.XForms.ValueEventArgs e)
        {
            var newStep = Math.Round(e.Value / StepValue);
            var vm = BindingContext as SettingViewModel;
            if (vm == null) return;
            var slider = (SfRangeSlider)sender;
            slider.Value = newStep * StepValue;
            if (slider.Value == 1)
                vm.Bonos = $"{slider.Value} colón";
            else
                vm.Bonos = $"{slider.Value} colones";
        }

        void SfComboBox_SelectionChanged(System.Object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
        }
    }
}
