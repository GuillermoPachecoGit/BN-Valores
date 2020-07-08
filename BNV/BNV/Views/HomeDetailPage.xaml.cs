using System;
using System.Linq;
using System.Threading.Tasks;
using BNV.Settings;
using BNV.ViewModels;
using BNV.Views.Bases;
using Plugin.DeviceOrientation;
using Syncfusion.SfRangeSlider.XForms;
using Syncfusion.XForms.Buttons;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BNV.Views
{
    public partial class HomeDetailPage : ContentPageBase
    {
        private double StepValue;

        public HomeDetailPage()
        {
            InitializeComponent();
            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarTextColor = Color.White;
            SizeChanged += MainPageSizeChanged;
            StepValue = 1;
            navConfig.IsVisible = false;
            nav.IsVisible = true;
        }

        void SfRangeSlider_ValueChanging(System.Object sender, Syncfusion.SfRangeSlider.XForms.ValueEventArgs e)
        {
            var newStep = Math.Round(e.Value / StepValue);
            var vm = BindingContext as HomeDetailViewModel;
            if (vm == null) return;
            var slider = (SfRangeSlider)sender;
            slider.Value = newStep * StepValue;

            switch (slider.Value)
            {
                case 1:
                    vm.TypeChange = $"No notificar";
                    vm.ExchangeNotify = -1;
                    break;
                case 2:
                    vm.TypeChange = $"0.05 colones";
                    vm.ExchangeNotify = 0.05;
                    break;
                case 3:
                    vm.TypeChange = $"0.10 colones";
                    vm.ExchangeNotify = 0.10;
                    break;
                case 4:
                    vm.TypeChange = $"0.25 colones";
                    vm.ExchangeNotify = 0.25;
                    break;
                case 5:
                    vm.TypeChange = $"0.50 colones";
                    vm.ExchangeNotify = 0.50;
                    break;
                case 6:
                    vm.TypeChange = $"0.75 colones";
                    vm.ExchangeNotify = 0.75;
                    break;
                case 7:
                    vm.TypeChange = $"1.00 colon";
                    vm.ExchangeNotify = 1;
                    break;
                case 8:
                    vm.TypeChange = $"2.00 colones";
                    vm.ExchangeNotify = 2;
                    break;
                case 9:
                    vm.TypeChange = $"3.00 colones";
                    vm.ExchangeNotify = 3;
                    break;
                case 10:
                    vm.TypeChange = $"4.00 colones";
                    vm.ExchangeNotify = 4;
                    break;
                case 11:
                    vm.TypeChange = $"5.00 colones";
                    vm.ExchangeNotify = 5;
                    break;
                default:
                    break;
            }

            SecureStorage.SetAsync(Config.TypeChange, slider.Value.ToString());
        }

        void SfRangeSlider_ValueChanging_1(System.Object sender, Syncfusion.SfRangeSlider.XForms.ValueEventArgs e)
        {
            var newStep = Math.Round(e.Value / StepValue);
            var vm = BindingContext as HomeDetailViewModel;
            if (vm == null) return;
            var slider = (SfRangeSlider)sender;
            slider.Value = newStep * StepValue;
            switch (slider.Value)
            {
                case 1:
                    vm.Bonos = $"No notificar";
                    vm.BonosNotify = -1;
                    break;
                case 2:
                    vm.Bonos = $"0.05%";
                    vm.BonosNotify = 0.05;
                    break;
                case 3:
                    vm.Bonos = $"0.10%";
                    vm.BonosNotify = 0.1;
                    break;
                case 4:
                    vm.Bonos = $"0.25%";
                    vm.BonosNotify = 0.25;
                    break;
                case 5:
                    vm.Bonos = $"0.50%";
                    vm.BonosNotify = 0.50;
                    break;
                case 6:
                    vm.Bonos = $"0.75%";
                    vm.BonosNotify = 0.75;
                    break;
                case 7:
                    vm.Bonos = $"1.00%";
                    vm.BonosNotify = 1;
                    break;
                case 8:
                    vm.Bonos = $"2.00%";
                    vm.BonosNotify = 2;
                    break;
                case 9:
                    vm.Bonos = $"3.00%";
                    vm.BonosNotify = 3;
                    break;
                case 10:
                    vm.Bonos = $"4.00%";
                    vm.BonosNotify = 4;
                    break;
                case 11:
                    vm.Bonos = $"5.00%";
                    vm.BonosNotify = 5;
                    break;
                default:
                    break;
            }

            SecureStorage.SetAsync(Config.BonosChange, slider.Value.ToString());
        }

        void MainPageSizeChanged(object sender, EventArgs e)
        {
            bool isPortrait = this.Height > this.Width;
            if (!isPortrait)
            {
                if (TabVerticalMain.SelectedIndex != 1)
                {
                    tabHorizontal.IsVisible = true;
                    tab.IsVisible = false;
                }
            }
            else
            {
                tab.IsVisible = true;
                tabHorizontal.IsVisible = false;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.UnlockOrientation();
            group1.SelectedItem = group1.Items[0];
            group.SelectedItem = group.Items[0];
            group3.SelectedItem = group3.Items[0];
            group4.SelectedItem = group4.Items[0];

            group.SelectionChanged += Group_SelectionChanged;
            group1.SelectionChanged += Group_SelectionChanged;
            group3.SelectionChanged += Group_SelectionChanged;
            group4.SelectionChanged += Group_SelectionChanged;

            var bonosChanges = await SecureStorage.GetAsync(Config.BonosChange);
            var typeChanges = await SecureStorage.GetAsync(Config.TypeChange);

            int indexBonos;
            if (int.TryParse(bonosChanges, out indexBonos))
                bonosSlider.Value = indexBonos;

            int indexTypes;
            if (int.TryParse(typeChanges, out indexTypes))
                typeSlider.Value = indexTypes;

        }

        private void Group_SelectionChanged(object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {
            var index = ((SfChipGroup)sender).Items.IndexOf((SfChip)e.AddedItem);

            group1.SelectedItem = group1.Items[index];
            group.SelectedItem = group.Items[index];
            group3.SelectedItem = group3.Items[index];
            group4.SelectedItem = group4.Items[index];
        }

        void SfTabView_SelectionChanged(System.Object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
            var vm = BindingContext as HomeDetailViewModel;
            if (vm == null) return;

            var selectedIndex = e.Index;
            if (selectedIndex == 0)
            {
                if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.UnlockOrientation();
                vm.SetDetailsCommand.Execute(null);
                navConfig.IsVisible = false;
                nav.IsVisible = true;
                nav2.IsVisible = true;
            }
            else
            {
                if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);
                vm.SetConfigCommand.Execute(null);
                navConfig.IsVisible = true;
                nav2.IsVisible = false;
                nav.IsVisible = false;
            }
        }

        void tabs_vertical_SelectionChanged(System.Object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
            tabs_horizontal.SelectedIndex = e.Index;
        }

        void tabs_horizontal_SelectionChanged(System.Object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
            tabs_vertical.SelectedIndex = e.Index;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);
        }
    }
}
