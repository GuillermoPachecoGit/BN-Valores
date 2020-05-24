using System.Linq;
using BNV.Models;
using BNV.ViewModels;
using Xamarin.Forms;

namespace BNV.Views
{
    public partial class BonoPage : ContentPage
    {
        public BonoPage()
        {
            InitializeComponent();
        }

        void CollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            ((BonoViewModel)this.BindingContext).SelectedItem = (e.CurrentSelection.FirstOrDefault() as ItemBase);
        }
    }
}
