using System;
using Prism.Navigation;

namespace BNV.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        public SettingViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            Title = "Register Page";
        }
    }
}
