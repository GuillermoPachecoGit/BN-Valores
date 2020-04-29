using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BNV.Events;
using BNV.Models;
using BNV.Settings;
using Prism.Events;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class SharesOfStockViewModel : ViewModelBase
    {
        private ObservableCollection<ShareOfStock> _shares;

        public SharesOfStockViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            SetupCoin();
            SetupSector();
            NavigateToDetailsCommand = new Command<ItemBase>(NavigateToDetailsAction);
            ea.GetEvent<FilterCoinEvent>().Subscribe(FilterCoin);
            ea.GetEvent<FilterSectorEvent>().Subscribe(FilterSector);
        }

        private void FilterSector(string obj)
        {
            SetupSector();
        }

        private void FilterCoin(string obj)
        {
            SetupCoin();
        }

        private async Task SetupCoin()
        {
            var shares = new List<ShareOfStock>();
            var value = await SecureStorage.GetAsync(Config.FilterCoin);

            if (value != null && value == Config.CoinTypes.CoinColon)
            {
                for (int i = 0; i < 3; i++)
                {
                    shares.Add(new ShareOfStock() { ColorStatus = "#81B71A" });
                }
            }
            else if (value != null && value == Config.CoinTypes.CoinDolar)
            {
                for (int i = 0; i < 2; i++)
                {
                    shares.Add(new ShareOfStock() { ColorStatus = "#81B71A" });
                }
            }
            else
            {
                for (int i = 0; i < 11; i++)
                {
                    shares.Add(new ShareOfStock() { ColorStatus = "#81B71A" });
                }
            }

            Shares = new ObservableCollection<ShareOfStock>(shares);
        }

        private async Task SetupSector()
        {
            var shares = new List<ShareOfStock>();
            var value = await SecureStorage.GetAsync(Config.FilterSector);

            if (value != null && value == Config.SectorTypes.Public)
            {
                for (int i = 0; i < 1; i++)
                {
                    shares.Add(new ShareOfStock() { ColorStatus = "#81B71A" });
                }
            }
            else if (value != null && value == Config.SectorTypes.Privado)
            {
                for (int i = 0; i < 3; i++)
                {
                    shares.Add(new ShareOfStock() { ColorStatus = "#81B71A" });
                }
            }
            else if (value != null && value == Config.SectorTypes.Mixto)
            {
                for (int i = 0; i < 8; i++)
                {
                    shares.Add(new ShareOfStock() { ColorStatus = "#81B71A" });
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    shares.Add(new ShareOfStock() { ColorStatus = "#81B71A" });
                }
            }

            Shares = new ObservableCollection<ShareOfStock>(shares);
        }

        public ObservableCollection<ShareOfStock> Shares
        {
            get { return _shares; }
            set { SetProperty(ref _shares, value); }
        }

        private Command<ItemBase> NavigateToDetailsCommand { get; set; }

        private ItemBase _selectedItem;
        public ItemBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NavigateToDetailsCommand.Execute(_selectedItem);
            }
        }

        private async void NavigateToDetailsAction(ItemBase obj)
        {
            await NavigationService.NavigateAsync("HomeDetailPage", new NavigationParameters() { { "item", obj } }, false, false);
        }
    }
}
