﻿using System.Collections.Generic;
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
    public class ChangeTypeViewModel : ViewModelBase
    {
        private ObservableCollection<ShareOfStock> _types;

        public ChangeTypeViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            NavigateToDetailsCommand = new Command<ItemBase>(NavigateToDetailsAction);
            ea.GetEvent<FilterCoinEvent>().Subscribe(FilterCoin);
            ea.GetEvent<FilterSectorEvent>().Subscribe(FilterSector);
            LoadData();
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
            var types = new List<ShareOfStock>();
            var value = await SecureStorage.GetAsync(Config.FilterCoin);

            if (value != null && value == Config.CoinTypes.CoinColon)
            {
                for (int i = 0; i < 14; i++)
                {
                    types.Add(new ShareOfStock() { ColorStatus = "#B51010" });
                }
            }
            else if (value != null && value == Config.CoinTypes.CoinDolar)
            {
                for (int i = 0; i < 1; i++)
                {
                    types.Add(new ShareOfStock() { ColorStatus = "#B51010" });
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    types.Add(new ShareOfStock() { ColorStatus = "#B51010" });
                }
            }

            Types = new ObservableCollection<ShareOfStock>(types);
        }

        private async Task SetupSector()
        {
            var types = new List<ShareOfStock>();
            var value = await SecureStorage.GetAsync(Config.FilterSector);

            if (value != null && value == Config.SectorTypes.Public)
            {
                for (int i = 0; i < 2; i++)
                {
                    types.Add(new ShareOfStock() { ColorStatus = "#B51010" });
                }
            }
            else if (value != null && value == Config.SectorTypes.Privado)
            {
                for (int i = 0; i < 11; i++)
                {
                    types.Add(new ShareOfStock() { ColorStatus = "#B51010" });
                }
            }
            else if (value != null && value == Config.SectorTypes.Mixto)
            {
                for (int i = 0; i < 6; i++)
                {
                    types.Add(new ShareOfStock() { ColorStatus = "#B51010" });
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    types.Add(new ShareOfStock() { ColorStatus = "#B51010"});
                }
            }

            Types = new ObservableCollection<ShareOfStock>(types);
        }

        public ObservableCollection<ShareOfStock> Types
        {
            get { return _types; }
            set { SetProperty(ref _types, value); }
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

        public void LoadData()
        {
            if (AlreadyLoaded)
                return;
            Task.Run(async () =>
            {
                await SetupCoin();
                await SetupSector();
                AlreadyLoaded = true;
            });
        }
    }
}
