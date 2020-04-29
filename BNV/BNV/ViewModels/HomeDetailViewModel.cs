using System;
using BNV.Events;
using BNV.Models;
using Prism.Events;
using Prism.Navigation;

namespace BNV.ViewModels
{
    public class HomeDetailViewModel : ViewModelBase
    {
        public HomeDetailViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            Events = ea;
        }

        public IEventAggregator Events { get; set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                var item = parameters.GetValue<ItemBase>("item");

                if (item != null)
                {
                    ColorStatus = item.ColorStatus;
                    Events.GetEvent<NavigationColorEvent>().Publish(item.ColorStatus);
                    Title = "BNVR C";
                }
            }
            catch (Exception ex)
            {

            }
        }

        public override void Destroy()
        {
            base.Destroy();
            Events.GetEvent<NavigationColorEvent>().Publish("#AFBC24");
        }
    }
}
