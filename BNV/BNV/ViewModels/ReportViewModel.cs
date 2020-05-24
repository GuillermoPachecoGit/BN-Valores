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
    public class ReportViewModel : ViewModelBase
    {
        private ObservableCollection<Report> _reports;

        public ReportViewModel(INavigationService navigationService, IEventAggregator ea)
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
            var reportos = new List<Report>();
            var value = await SecureStorage.GetAsync(Config.FilterCoin);

            if (value != null && value == Config.CoinTypes.CoinColon)
            {
                for (int i = 0; i < 1; i++)
                {
                    reportos.Add(new Report() { ColorStatus = "#B51010" });
                }
            }
            else if (value != null && value == Config.CoinTypes.CoinDolar)
            {
                for (int i = 0; i < 3; i++)
                {
                    reportos.Add(new Report() { ColorStatus = "#B51010" });
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    reportos.Add(new Report() { ColorStatus = "#B51010" });
                }
            }

            Reports = new ObservableCollection<Report>(reportos);
        }

        private async Task SetupSector()
        {
            var reportos = new List<Report>();
            var value = await SecureStorage.GetAsync(Config.FilterSector);

            if (value != null && value == Config.SectorTypes.Public)
            {
                for (int i = 0; i < 8; i++)
                {
                    reportos.Add(new Report() { ColorStatus = "#B51010" });
                }
            }
            else if (value != null && value == Config.SectorTypes.Privado)
            {
                for (int i = 0; i < 3; i++)
                {
                    reportos.Add(new Report() { ColorStatus = "#B51010" });
                }
            }
            else if (value != null && value == Config.SectorTypes.Mixto)
            {
                for (int i = 0; i < 4; i++)
                {
                    reportos.Add(new Report() { ColorStatus = "#B51010" });
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    reportos.Add(new Report() { ColorStatus = "#B51010" });
                }
            }

            Reports = new ObservableCollection<Report>(reportos);
        }

        public ObservableCollection<Report> Reports
        {
            get { return _reports; }
            set { SetProperty( ref _reports, value); }
        }

        private Command<ItemBase> NavigateToDetailsCommand { get; set; }

        private ItemBase _selectedItem;
        public ItemBase SelectedItem
        {
            get { return _selectedItem;  }
            set
            {   _selectedItem = value;
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

