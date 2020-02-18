using System;
using Prism.Navigation;

namespace BNV.ViewModels
{
    public class ChangeTypeViewModel : ViewModelBase
    {
        public ChangeTypeViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Login";
        }
    }
}
