using System.Linq;
using BNV.Models;
using BNV.ViewModels;
using Xamarin.Forms;

namespace BNV.Views
{
    public partial class SharesOfStockPage : ContentPage
    {
        public SharesOfStockPage()
        {
            InitializeComponent();
        }

        void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((SharesOfStockViewModel)this.BindingContext).SelectedItem = (e.CurrentSelection.FirstOrDefault() as ItemBase);
        }
    }
}
