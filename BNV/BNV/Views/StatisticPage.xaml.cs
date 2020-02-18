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
    public partial class StatisticPage : TabbedPage
    {
        public StatisticPage()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#00579F");
        }
    }
}
