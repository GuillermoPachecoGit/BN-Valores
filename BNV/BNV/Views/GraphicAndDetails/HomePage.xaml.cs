using System;
using System.Linq;
using BNV.Models;
using BNV.Settings;
using BNV.ViewModels;
using Plugin.DeviceOrientation;
using Syncfusion.SfRangeSlider.XForms;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            //((NavigationPage)Application.Current.MainPage).SetBinding(NavigationPage.BarBackgroundColorProperty, new Binding("ColorStatus", BindingMode.TwoWay));
            StepValue = 1;
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.Black;
            return base.OnBackButtonPressed();
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

            var bonosChanges = await SecureStorage.GetAsync(Config.BonosChange);
            var typeChanges = await SecureStorage.GetAsync(Config.TypeChange);

            int indexBonos;
            if (int.TryParse(bonosChanges, out indexBonos))
                bonosSlider.Value = indexBonos;

            int indexTypes;
            if (int.TryParse(typeChanges, out indexTypes))
                typesSlider.Value = indexTypes;

            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#AFBC24");
            ((HomeViewModel)this.BindingContext).Title = "Estadísticas";
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);

            if (((HomeViewModel)this.BindingContext).AlreadyLoaded)
                return;

            var value = await SecureStorage.GetAsync(Config.MainPage);
            if (value != null && value == Config.MainPageTypes.Bonos) {
                TabExternal.SelectedIndex = 1;
            }
            else if (value != null && value == Config.MainPageTypes.Reportos)
            {
                TabExternal.SelectedIndex = 0;
            }
            else if (value != null && value == Config.MainPageTypes.TipoCambio)
            {
                TabExternal.SelectedIndex = 3;
            }
            else if (value != null && value == Config.MainPageTypes.Acciones)
            {
                TabExternal.SelectedIndex = 2;
            }
            else
            {
                TabExternal.SelectedIndex = 0;
            }
            ((HomeViewModel)this.BindingContext).AlreadyLoaded = true;

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.UnlockOrientation();
        }

        void CollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            ((HomeViewModel)this.BindingContext).SelectedItem = (e.CurrentSelection.FirstOrDefault() as ItemBase);
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

            SecureStorage.SetAsync(Config.TypeChange, slider.Value.ToString());
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

            SecureStorage.SetAsync(Config.BonosChange, slider.Value.ToString());
        }

        void TabExternal_SelectionChanged(System.Object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
            if (e.Index == 0)
                ((HomeViewModel)this.BindingContext).Title = "Estadísticas";
            else
                ((HomeViewModel)this.BindingContext).Title = "Configuración";
        }
    }
}
