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
                    shares.Add(new ShareOfStock());
                }
            }
            else if (value != null && value == Config.CoinTypes.CoinDolar)
            {
                for (int i = 0; i < 2; i++)
                {
                    shares.Add(new ShareOfStock());
                }
            }
            else
            {
                for (int i = 0; i < 11; i++)
                {
                    shares.Add(new ShareOfStock());
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
                    shares.Add(new ShareOfStock());
                }
            }
            else if (value != null && value == Config.SectorTypes.Privado)
            {
                for (int i = 0; i < 3; i++)
                {
                    shares.Add(new ShareOfStock());
                }
            }
            else if (value != null && value == Config.SectorTypes.Mixto)
            {
                for (int i = 0; i < 8; i++)
                {
                    shares.Add(new ShareOfStock());
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    shares.Add(new ShareOfStock());
                }
            }

            Shares = new ObservableCollection<ShareOfStock>(shares);
        }

        public ObservableCollection<ShareOfStock> Shares
        {
            get { return _shares; }
            set { SetProperty(ref _shares, value); }
        }


    }
}
