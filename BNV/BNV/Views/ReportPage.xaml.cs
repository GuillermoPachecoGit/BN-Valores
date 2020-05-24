using BNV.ViewModels;
using Xamarin.Forms;

namespace BNV.Views
{
    public partial class ReportPage : ContentPage
    {
        public ReportPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((ReportViewModel)this.BindingContext).LoadData();
        }
    }
}
