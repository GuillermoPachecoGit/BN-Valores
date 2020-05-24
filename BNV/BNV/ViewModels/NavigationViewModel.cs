using System;
using BNV.Events;
using Prism.Events;
using Prism.Navigation;

namespace BNV.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        public NavigationViewModel(INavigationService navigationService, IEventAggregator ea)
          : base(navigationService)
        {
            ea.GetEvent<NavigationColorEvent>().Subscribe(SetColor);
            ea.GetEvent<NavigationTitleEvent>().Subscribe(SetTitle);
        }

        private void SetTitle(string obj)
        {
            Title = obj;
        }

        private void SetColor(string obj)
        {
            ColorStatus = obj;
        }
    }
}
