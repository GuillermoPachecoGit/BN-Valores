using System;
using Prism.Navigation;

namespace BNV.ViewModels
{
    public class SharesOfStockViewModel : ViewModelBase
    {
        public SharesOfStockViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Login";
        }
    }
}
