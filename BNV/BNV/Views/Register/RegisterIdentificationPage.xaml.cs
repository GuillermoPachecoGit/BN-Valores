using System;
using System.Collections.Generic;
using BNV.Models;
using BNV.ViewModels;
using Plugin.DeviceOrientation;
using Xamarin.Forms;

namespace BNV.Views.Register
{
    public partial class RegisterIdentificationPage : ContentPage
    {
        public int MaxSize { get; private set; }
        public int MinSize { get; private set; }

        public RegisterIdentificationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);
            boxIdent.BackgroundColor = Color.White;
            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarTextColor = Color.White;
            NavigationPage.SetHasBackButton(this, true);
            identError.IsVisible = false;
        }

        void Entry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var vm = (RegisterIdentificationViewModel)BindingContext;

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

        void ComboId_SelectionChanged(System.Object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var vm = (RegisterIdentificationViewModel)BindingContext;
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

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
