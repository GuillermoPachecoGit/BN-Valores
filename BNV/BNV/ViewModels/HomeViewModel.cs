using BNV.Events;
using Prism.Events;
using Prism.Navigation;

namespace BNV.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            Title = "Estadisticas";
            ea.GetEvent<NavigationColorEvent>().Publish("#AFBC24");
        }
    }
}
