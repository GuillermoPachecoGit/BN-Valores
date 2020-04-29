using System;
using BNV.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BNV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : Xamarin.Forms.TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
            //((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#AFBC24");
            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarTextColor = Color.White;
            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).SetBinding(NavigationPage.BarBackgroundColorProperty, new Binding("ColorStatus", BindingMode.TwoWay));

            Title = "Estadísticas";
            NavigationPage.SetHasBackButton(this, false);
            this.CurrentPageChanged += CurrentPageHasChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#AFBC24");
        }

        protected override bool OnBackButtonPressed()
        {
            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.Black;
            return base.OnBackButtonPressed();
        }

        public void CurrentPageHasChanged(object sender, EventArgs e) {
            this.Title = this.CurrentPage.Title;
            ((HomeViewModel)this.BindingContext).Title = this.Title;
        }
    }
}
