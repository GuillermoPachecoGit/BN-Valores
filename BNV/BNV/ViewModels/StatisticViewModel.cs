using System;
using BNV.Settings;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using Xamarin.Essentials;

namespace BNV.ViewModels
{
    public class StatisticViewModel : ViewModelBase
    {
        public StatisticViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            Title = "Register Page";
           
        }
    }
}
