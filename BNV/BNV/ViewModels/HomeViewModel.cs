using System;
using Prism.Navigation;

namespace BNV.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Login";
        }
    }
}
