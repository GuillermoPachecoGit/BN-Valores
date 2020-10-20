using System;
using System.Linq;
using BNV.Models;
using BNV.Settings;
using BNV.ViewModels;
using Plugin.DeviceOrientation;
using Syncfusion.SfRangeSlider.XForms;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;
using NavigationPage = Xamarin.Forms.NavigationPage;

namespace BNV.Views.GraphicAndDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private double StepValue;
        public HomePage()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            StepValue = 1;
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public void CurrentPageHasChanged(object sender, EventArgs e){
            ((HomeViewModel)this.BindingContext).Title = this.Title;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            list1.SelectedItem = null;
            list2.SelectedItem = null;
            list3.SelectedItem = null;
            list4.SelectedItem = null;

            ((HomeViewModel)this.BindingContext).SetupSettingsEvent += SetupSettings;
            ((HomeViewModel)this.BindingContext).SetupAllSettingsEvent += SetupAllSettings;

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#B8BE14");
            ((HomeViewModel)BindingContext).Title = "Estadísticas";
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);

            if (((HomeViewModel)this.BindingContext).AlreadyLoaded)
                return;

            ((HomeViewModel)this.BindingContext).AlreadyLoaded = true;

        }

        private void SetupSettings()
        {
            bonosSlider.Value = App.BonosIndexNotify;
            typesSlider.Value = App.ExchangesIndexNotify;

            if (Device.RuntimePlatform == Device.iOS)
            {
                SfRangeSlider_ValueChanging_1(bonosSlider);
                SfRangeSlider_ValueChanging(typesSlider);
            }
        }

        private void SetupAllSettings()
        {
            SetupSettings();
            if (App.HomePage != null && App.HomePage == Config.MainPageTypes.Bonos)
            {
                TabExternal.SelectedIndex = 1;
            }
            else if (App.HomePage != null && App.HomePage == Config.MainPageTypes.Reportos)
            {
                TabExternal.SelectedIndex = 0;
            }
            else if (App.HomePage != null && App.HomePage == Config.MainPageTypes.TipoCambio)
            {
                TabExternal.SelectedIndex = 3;
            }
            else if (App.HomePage != null && App.HomePage == Config.MainPageTypes.Acciones)
            {
                TabExternal.SelectedIndex = 2;
            }
            else
            {
                TabExternal.SelectedIndex = 0;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((HomeViewModel)BindingContext).SelectedItem = (e.CurrentSelection.FirstOrDefault() as ItemBase);
        }

        void SfRangeSlider_ValueChanging(object sender)
        {
            HomeViewModel vm = BindingContext as HomeViewModel;
            if (vm == null) return;
            var slider = (SfRangeSlider)sender;

            switch (slider.Value)
            {
                case 1:
                    vm.TypeChange = $"No notificar";
                    vm.ExchangeNotify = -1;
                    App.ExchangesIndexNotify = 1;
                    break;
                case 2:
                    vm.TypeChange = $"0.05 colones";
                    vm.ExchangeNotify = 0.05;
                    App.ExchangesIndexNotify = 2;
                    break;
                case 3:
                    vm.TypeChange = $"0.10 colones";
                    vm.ExchangeNotify = 0.10;
                    App.ExchangesIndexNotify = 3;
                    break;
                case 4:
                    vm.TypeChange = $"0.25 colones";
                    vm.ExchangeNotify = 0.25;
                    App.ExchangesIndexNotify = 4;
                    break;
                case 5:
                    vm.TypeChange = $"0.50 colones";
                    vm.ExchangeNotify = 0.5;
                    App.ExchangesIndexNotify = 5;
                    break;
                case 6:
                    vm.TypeChange = $"0.75 colones";
                    vm.ExchangeNotify = 0.75;
                    App.ExchangesIndexNotify = 6;
                    break;
                case 7:
                    vm.TypeChange = $"1.00 colón";
                    vm.ExchangeNotify = 1;
                    App.ExchangesIndexNotify = 7;
                    break;
                case 8:
                    vm.TypeChange = $"2.00 colones";
                    vm.ExchangeNotify = 2;
                    App.ExchangesIndexNotify = 8;
                    break;
                case 9:
                    vm.TypeChange = $"3.00 colones";
                    vm.ExchangeNotify = 3;
                    App.ExchangesIndexNotify = 9;
                    break;
                case 10:
                    vm.TypeChange = $"4.00 colones";
                    vm.ExchangeNotify = 4;
                    App.ExchangesIndexNotify = 10;
                    break;
                case 11:
                    vm.TypeChange = $"5.00 colones";
                    vm.ExchangeNotify = 5;
                    App.ExchangesIndexNotify = 11;
                    break;
                default:
                    break;
            }
        }


        void SfRangeSlider_ValueChanging(object sender, ValueEventArgs e)
        {
            var newStep = Math.Round(e.Value / StepValue);
            HomeViewModel vm = BindingContext as HomeViewModel;
            if (vm == null) return;
            var slider = (SfRangeSlider)sender;

            slider.Value = newStep * StepValue;

            switch (slider.Value)
            {
                case 1:
                    vm.TypeChange = $"No notificar";
                    vm.ExchangeNotify = -1;
                    App.ExchangesIndexNotify = 1;
                    break;
                case 2:
                    vm.TypeChange = $"0.05 colones";
                    vm.ExchangeNotify = 0.05;
                    App.ExchangesIndexNotify = 2;
                    break;
                case 3:
                    vm.TypeChange = $"0.10 colones";
                    vm.ExchangeNotify = 0.10;
                    App.ExchangesIndexNotify = 3;
                    break;
                case 4:
                    vm.TypeChange = $"0.25 colones";
                    vm.ExchangeNotify = 0.25;
                    App.ExchangesIndexNotify = 4;
                    break;
                case 5:
                    vm.TypeChange = $"0.50 colones";
                    vm.ExchangeNotify = 0.5;
                    App.ExchangesIndexNotify = 5;
                    break;
                case 6:
                    vm.TypeChange = $"0.75 colones";
                    vm.ExchangeNotify = 0.75;
                    App.ExchangesIndexNotify = 6;
                    break;
                case 7:
                    vm.TypeChange = $"1.00 colón";
                    vm.ExchangeNotify = 1;
                    App.ExchangesIndexNotify = 7;
                    break;
                case 8:
                    vm.TypeChange = $"2.00 colones";
                    vm.ExchangeNotify = 2;
                    App.ExchangesIndexNotify = 8;
                    break;
                case 9:
                    vm.TypeChange = $"3.00 colones";
                    vm.ExchangeNotify = 3;
                    App.ExchangesIndexNotify = 9;
                    break;
                case 10:
                    vm.TypeChange = $"4.00 colones";
                    vm.ExchangeNotify = 4;
                    App.ExchangesIndexNotify = 10;
                    break;
                case 11:
                    vm.TypeChange = $"5.00 colones";
                    vm.ExchangeNotify = 5;
                    App.ExchangesIndexNotify = 11;
                    break;
                default:
                    break;
            }
        }

        void SfRangeSlider_ValueChanging_1(object sender, ValueEventArgs e)
        {
            var newStep = Math.Round(e.Value / StepValue);
            HomeViewModel vm = BindingContext as HomeViewModel;
            if (vm == null) return;
            var slider = (SfRangeSlider)sender;
            slider.Value = newStep * StepValue;
            switch (slider.Value)
            {
                case 1:
                    vm.BonosLabel = $"No notificar";
                    vm.BonosNotify = -1;
                    App.BonosIndexNotify = 1;
                    break;
                case 2:
                    vm.BonosLabel = $"0.05%";
                    vm.BonosNotify = 0.05;
                    App.BonosIndexNotify = 2;
                    break;
                case 3:
                    vm.BonosLabel = $"0.10%";
                    vm.BonosNotify = 0.1;
                    App.BonosIndexNotify = 3;
                    break;
                case 4:
                    vm.BonosLabel = $"0.25%";
                    vm.BonosNotify = 0.25;
                    App.BonosIndexNotify = 4;
                    break;
                case 5:
                    vm.BonosLabel = $"0.50%";
                    vm.BonosNotify = 0.50;
                    App.BonosIndexNotify = 5;
                    break;
                case 6:
                    vm.BonosLabel = $"0.75%";
                    vm.BonosNotify = 0.75;
                    App.BonosIndexNotify = 6;
                    break;
                case 7:
                    vm.BonosLabel = $"1.00%";
                    vm.BonosNotify = 1;
                    App.BonosIndexNotify = 7;
                    break;
                case 8:
                    vm.BonosLabel = $"2.00%";
                    vm.BonosNotify = 2;
                    App.BonosIndexNotify = 8;
                    break;
                case 9:
                    vm.BonosLabel = $"3.00%";
                    vm.BonosNotify = 3;
                    App.BonosIndexNotify = 9;
                    break;
                case 10:
                    vm.BonosLabel = $"4.00%";
                    vm.BonosNotify = 4;
                    App.BonosIndexNotify = 10;
                    break;
                case 11:
                    vm.BonosLabel = $"5.00%";
                    vm.BonosNotify = 5;
                    App.BonosIndexNotify = 11;
                    break;
                default:
                    break;
            }
        }

        void SfRangeSlider_ValueChanging_1(object sender)
        {
            HomeViewModel vm = BindingContext as HomeViewModel;
            if (vm == null) return;
            var slider = (SfRangeSlider)sender;
            switch (slider.Value)
            {
                case 1:
                    vm.BonosLabel = $"No notificar";
                    vm.BonosNotify = -1;
                    App.BonosIndexNotify = 1;
                    break;
                case 2:
                    vm.BonosLabel = $"0.05%";
                    vm.BonosNotify = 0.05;
                    App.BonosIndexNotify = 2;
                    break;
                case 3:
                    vm.BonosLabel = $"0.10%";
                    vm.BonosNotify = 0.1;
                    App.BonosIndexNotify = 3;
                    break;
                case 4:
                    vm.BonosLabel = $"0.25%";
                    vm.BonosNotify = 0.25;
                    App.BonosIndexNotify = 4;
                    break;
                case 5:
                    vm.BonosLabel = $"0.50%";
                    vm.BonosNotify = 0.50;
                    App.BonosIndexNotify = 5;
                    break;
                case 6:
                    vm.BonosLabel = $"0.75%";
                    vm.BonosNotify = 0.75;
                    App.BonosIndexNotify = 6;
                    break;
                case 7:
                    vm.BonosLabel = $"1.00%";
                    vm.BonosNotify = 1;
                    App.BonosIndexNotify = 7;
                    break;
                case 8:
                    vm.BonosLabel = $"2.00%";
                    vm.BonosNotify = 2;
                    App.BonosIndexNotify = 8;
                    break;
                case 9:
                    vm.BonosLabel = $"3.00%";
                    vm.BonosNotify = 3;
                    App.BonosIndexNotify = 9;
                    break;
                case 10:
                    vm.BonosLabel = $"4.00%";
                    vm.BonosNotify = 4;
                    App.BonosIndexNotify = 10;
                    break;
                case 11:
                    vm.BonosLabel = $"5.00%";
                    vm.BonosNotify = 5;
                    App.BonosIndexNotify = 11;
                    break;
                default:
                    break;
            }
        }


        void TabExternal_SelectionChanged(object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
            if (e.Index == 0)
                ((HomeViewModel)BindingContext).Title = "Estadísticas";
            else
                ((HomeViewModel)BindingContext).Title = "Configuración";
        }
    }
}
