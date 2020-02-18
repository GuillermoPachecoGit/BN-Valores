using System;
using Prism.Navigation;

namespace BNV.ViewModels
{
    public class BonoViewModel : ViewModelBase
    {
        public BonoViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Login";
        }
}
}
