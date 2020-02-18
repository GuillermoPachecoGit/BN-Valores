using System;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        public ReportViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Login";
        }
}
}

