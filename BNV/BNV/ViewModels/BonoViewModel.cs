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
    public class BonoViewModel : ViewModelBase
    {
        private ObservableCollection<Bono> _bonos;

        public BonoViewModel(INavigationService navigationService, IEventAggregator ea)
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
            var bonos = new List<Bono>();
            var value = await SecureStorage.GetAsync(Config.FilterCoin);

            if (value != null && value == Config.CoinTypes.CoinColon)
            {
                for (int i = 0; i < 4; i++)
                {
                    bonos.Add(new Bono());
                }
            }
            else if (value != null && value == Config.CoinTypes.CoinDolar)
            {
                for (int i = 0; i < 11; i++)
                {
                    bonos.Add(new Bono());
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    bonos.Add(new Bono());
                }
            }

            Bonos = new ObservableCollection<Bono>(bonos);
        }

        private async Task SetupSector()
        {
            var bonos = new List<Bono>();
            var value = await SecureStorage.GetAsync(Config.FilterSector);

            if (value != null && value == Config.SectorTypes.Public)
            {
                for (int i = 0; i < 8; i++)
                {
                    bonos.Add(new Bono());
                }
            }
            else if (value != null && value == Config.SectorTypes.Privado)
            {
                for (int i = 0; i < 3; i++)
                {
                    bonos.Add(new Bono());
                }
            }
            else if (value != null && value == Config.SectorTypes.Mixto)
            {
                for (int i = 0; i < 4; i++)
                {
                    bonos.Add(new Bono());
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    bonos.Add(new Bono());
                }
            }

            Bonos = new ObservableCollection<Bono>(bonos);
        }

        public ObservableCollection<Bono> Bonos
        {
            get { return _bonos; }
            set { SetProperty(ref _bonos, value); }
        }
    }
}
