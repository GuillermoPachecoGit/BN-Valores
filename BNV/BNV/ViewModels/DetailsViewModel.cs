using BNV.Events;
using Prism.AppModel;
using Prism.Events;
using Prism.Navigation;

namespace BNV.ViewModels
{
    public class DetailsViewModel : ViewModelBase, IPageLifecycleAware
    {
        public DetailsViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            TitleNav = "BNVR C";
            Events = ea;
        }

        public string TitleNav { get; set; }
        public IEventAggregator Events { get; private set; }

        public void OnAppearing()
        {
            Events.GetEvent<NavigationTitleEvent>().Publish("BNVR C");
        }

        public void OnDisappearing()
        {
            
        }
    }
}
