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
            TitleNav = "BNVR C";
        }

        public IEventAggregator Events { get; set; }
        public string TitleNav { get; private set; }

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
                    Events.GetEvent<NavigationTitleEvent>().Subscribe(SetTitle);

                    TitleNav = "BNVR C";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SetTitle(string obj)
        {
            TitleNav = obj;
        }

        public override void Destroy()
        {
            base.Destroy();
            Events.GetEvent<NavigationColorEvent>().Publish("#AFBC24");
        }
    }
}
