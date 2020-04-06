using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNV.Settings;
using BNV.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace BNV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : Xamarin.Forms.TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#AFBC24");
            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarTextColor = Color.White;
            Title = "Estadísticas";
            NavigationPage.SetHasBackButton(this, false);
            this.CurrentPageChanged += CurrentPageHasChanged;
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
