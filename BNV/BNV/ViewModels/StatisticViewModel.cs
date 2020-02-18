using System;
using Prism.Navigation;

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
