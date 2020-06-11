using System;
using System.Collections.Generic;
using BNV.Views.Bases;
using Plugin.DeviceOrientation;
using Xamarin.Forms;

namespace BNV.Views
{
    public partial class GraphicPage : ContentPage
    {
        public GraphicPage()
        {
            InitializeComponent();
            SizeChanged += MainPageSizeChanged;
        }

        void MainPageSizeChanged(object sender, EventArgs e)
        {
            bool isPortrait = this.Height > this.Width;
            if (!isPortrait)
            {
                graph.HeightRequest = 180;
            }
            else
            {
                graph.HeightRequest = 250;
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
