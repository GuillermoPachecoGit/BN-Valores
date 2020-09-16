using System;
using BNV.ViewModels;
using BNV.Views.Bases;
using Plugin.DeviceOrientation;
using Syncfusion.SfRangeSlider.XForms;
using Syncfusion.XForms.Buttons;
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
                    vm.ExchangeNotify = 0.50;
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

        void SfRangeSlider_ValueChanging(System.Object sender)
        {
            var vm = BindingContext as HomeDetailViewModel;
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
                    vm.ExchangeNotify = 0.50;
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

        void SfRangeSlider_ValueChanging_1(System.Object sender)
        {
            var vm = BindingContext as HomeDetailViewModel;
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

            ((HomeDetailViewModel)this.BindingContext).SetupSettingsEvent += SetupSettings;

            group1.SelectedItem = group1.Items[0];
            group.SelectedItem = group.Items[0];
            group3.SelectedItem = group3.Items[0];
            group4.SelectedItem = group4.Items[0];

            group.SelectionChanged += Group_SelectionChanged;
            group1.SelectionChanged += Group_SelectionChanged;
            group3.SelectionChanged += Group_SelectionChanged;
            group4.SelectionChanged += Group_SelectionChanged;
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

        void SwipeGestureRecognizer_Swiped(System.Object sender, Xamarin.Forms.SwipedEventArgs e)
        {
            if (tabs_vertical.SelectedIndex == 1 || tabs_horizontal.SelectedIndex == 1)
                return;
            var vm = BindingContext as HomeDetailViewModel;
            vm.BackCommand.Execute(null);
        }
    }
}
