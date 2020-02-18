using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BNV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#AFBC24");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            Title = "Estadisticas";
            this.CurrentPageChanged += CurrentPageHasChanged;
        }

        

        protected void CurrentPageHasChanged(object sender, EventArgs e) { this.Title = this.CurrentPage.Title; }
    }
}
