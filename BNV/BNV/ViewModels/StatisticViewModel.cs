using System;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;

namespace BNV.ViewModels
{
    public class StatisticViewModel : ViewModelBase
    {
        public StatisticViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            Title = "Register Page";
            Init();
        }

        private async void Init()
        {
            await NavigationService.SelectTabAsync("ReportPage");
        }
    }
}
