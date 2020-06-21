using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BNV.Events;
using BNV.Models;
using BNV.ServicesWebAPI;
using BNV.Settings;
using Prism.AppModel;
using Prism.Events;
using Prism.Navigation;
using Syncfusion.XForms.TabView;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class HomeViewModel : ViewModelBase, IPageLifecycleAware
    {
        private const string TitleLabelStatistics = "Estadísticas";
        private const string ArrowDown = "red_arrow";
        private const string ArrowStable = "blue_arrow";
        private const string ArrowUp = "green_arrow";
        private const string TriangleDown = "triangle_down";
        private const string TriangleUp = "triangle_up";
        private const string ColorBlue = "#00579F";
        private const string ColorRed = "#B51010";
        private const string ColorGreen = "#81B71A";
        private const string ColorGreenNavigation = "#AFBC24";

        public HomeViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            //Title = TitleLabelStatistics;
            //ea.GetEvent<NavigationColorEvent>().Publish(ColorGreenNavigation);
            NavigateToDetailsCommand = new Command<ItemBase>(NavigateToDetailsAction);
            ea.GetEvent<FilterCoinEvent>().Subscribe(FilterCoin);
            ea.GetEvent<FilterSectorEvent>().Subscribe(FilterSector);
            TypeChange = "0.50 colones";
            BonosLabel = "0.50%";
            Events = ea;
            ChangePasswordCommand = new Command(async () => await ChangePasswordActionExecute());
            CloseSessionCommand = new Command(async () => await CloseSessionActionExecute());
        }

        public async Task SetupFilters()
        {
            var coin = await SecureStorage.GetAsync(Config.FilterCoin);
            if (coin != null)
                SelectedCoin = coin;

            var sector = await SecureStorage.GetAsync(Config.FilterSector);
            if (sector != null)
                SelectedSector = sector;

            var home = await SecureStorage.GetAsync(Config.MainPage);
            if (sector != null)
                SelectedHomePage = home;
        }

        private string _typeChange;

        public string TypeChange
        {
            get { return _typeChange; }
            set { _typeChange = value; RaisePropertyChanged(); }

        }

        private string _bonosLabel;

        public string BonosLabel
        {
            get { return _bonosLabel; }
            set { _bonosLabel = value; RaisePropertyChanged(); }

        }

        private string _selectedCoin;

        public string SelectedCoin
        {
            get { return _selectedCoin; }
            set { _selectedCoin = value; RaisePropertyChanged(); Task.Run(() => FilterCoin(value)); SecureStorage.SetAsync(Config.FilterCoin, value);  }

        }

        private string _selectedSector;

        public string SelectedSector
        {
            get { return _selectedSector; }
            set { _selectedSector = value; RaisePropertyChanged(); Task.Run(() => FilterSector(value)); SecureStorage.SetAsync(Config.FilterSector, value);  }

        }

        private string _selectedHomePage;

        public string SelectedHomePage
        {
            get { return _selectedHomePage; }
            set { _selectedHomePage = value; RaisePropertyChanged(); SecureStorage.SetAsync(Config.MainPage, value); }
        }

        public async void OnAppearing()
        {
           var value = await SecureStorage.GetAsync(Config.MainPage);
            if (!string.IsNullOrEmpty(value))
                SelectedHomePage = value;
            else
                SelectedHomePage = "Reportos";


            using (UserDialogs.Instance.Loading("Cargando los datos..."))
            {
                await Task.Delay(5);
                var apiService = NetworkService.GetApiService();
                var getShares = apiService.GetSharesStock(/*new ItemsParamModel() { Sector = 1, Currency = 1 }*/).ContinueWith(shares => _sharesStock = shares);
                var getReportos = apiService.GetReportos(new ItemsParamModel() { Sector = 1, Currency = 1 }).ContinueWith(reportos => _reportos = reportos);
                var getExchanges = apiService.GetExchangeRates(new ItemsParamModel() { Sector = 1, Currency = 1 }).ContinueWith(exchanges => _exchanges = exchanges);
                var getBonos = apiService.GetBonos(new ItemsParamModel() { Sector = 1, Currency = 1 }).ContinueWith(bonos => _bonosItems = bonos);


                await Task.WhenAll(getShares, getReportos, getExchanges, getBonos).ContinueWith(result =>
                {
                    if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                    {
                        // Get result and update any UI here.
                        var itemsReports = _reportos?.Result;
                        if (itemsReports != null)
                            Reports = new ObservableCollection<Report>(itemsReports.Select(
                                x =>
                                {
                                    x.ColorStatus = GetColor(x.Performance);
                                    x.Triangle = GetTriangle(x.Performance);
                                    x.IsBlue = x.Performance == 0;
                                    x.IsGreen = x.Performance > 0;
                                    x.IsRed = x.Performance < 0;
                                    x.Arrow = GetArrow(x.Performance);
                                    x.VolumeDisplay = x.Volume.ToString().Length >= 9 ? $"{x.Volume / 1000000}M" : x.Volume == 0 ? "N.D." :  x.Volume.ToString();
                                    return x;
                                }).ToList());

                        var itemsShares = _sharesStock?.Result;
                        if (itemsShares != null)
                            Shares = new ObservableCollection<ShareOfStock>(itemsShares.Select(
                                x =>
                                {
                                    x.ColorStatus = GetColor(x.Performance);
                                    x.Triangle = GetTriangle(x.Performance);
                                    x.IsBlue = x.Performance == 0;
                                    x.IsGreen = x.Performance > 0;
                                    x.IsRed = x.Performance < 0;
                                    x.VolumeDisplay = x.Volume.ToString().Length >= 9 ? $"{x.Volume / 1000000}M" : x.Volume == 0 ? "N.D." : x.Volume.ToString();
                                    return x;
                                }).ToList());

                        var itemsBonos = _bonosItems?.Result;
                        if (itemsBonos != null)
                            Bonos = new ObservableCollection<Bono>(itemsBonos.Select(
                                x =>
                                {
                                    x.ColorStatus = GetColor(x.Performance);
                                    x.Triangle = GetTriangle(x.Performance);
                                    x.IsBlue = x.Performance == 0;
                                    x.IsGreen = x.Performance > 0;
                                    x.IsRed = x.Performance < 0;
                                    x.VolumeDisplay = x.Volume.ToString().Length >= 9 ? $"{x.Volume / 1000000}M" : x.Volume == 0 ? "N.D." : x.Volume.ToString();
                                    return x;
                                }).ToList());

                        var itemsExchanges = _exchanges?.Result;
                        if (itemsExchanges != null)
                            Types = new ObservableCollection<ChangeType>(itemsExchanges.Select(
                                x =>
                                {
                                    x.ColorStatus = GetColor(x.Performance);
                                    x.Triangle = GetTriangle(x.Performance);
                                    x.IsBlue = x.Performance == 0;
                                    x.IsGreen = x.Performance > 0;
                                    x.IsRed = x.Performance < 0;
                                    x.Arrow = GetArrow(x.Performance);
                                    x.VolumeDisplay = x.Volume.ToString().Length >= 9 ? $"{x.Volume / 1000000}M" : x.Volume == 0 ? "N.D." : x.Volume.ToString();
                                    return x;
                                }).ToList());

                        UserDialogs.Instance.HideLoading();
                    }
                    else if (result.IsFaulted)
                    {
                        // If any error occurred exception throws.
                        UserDialogs.Instance.HideLoading();
                    }
                    else if (result.IsCanceled)
                    {
                        // Task cancelled
                        UserDialogs.Instance.HideLoading();
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext()).ConfigureAwait(false);
            }
        }

        public void OnDisappearing()
        {

        }

        public ICommand ChangePasswordCommand { get; set; }

        public ICommand CloseSessionCommand { get; set; }

        public IEventAggregator Events { get; }

        private async Task ChangePasswordActionExecute()
        {
            await NavigationService.NavigateAsync("ChangePasswordPage", new NavigationParameters() { { "title", "Cambio de contraseña" } }, false, false);
        }

        private async Task CloseSessionActionExecute()
        {
            await NavigationService.GoBackAsync();
        }

        private TabItemCollection items;
        public event PropertyChangedEventHandler PropertyChanged;
        public TabItemCollection Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FilterSector(string sector)
        {
            SetupSector(sector);
        }

        private void FilterCoin(string coin)
        {
            SetupCoin(coin);
        }

        private async Task SetupCoin(string coin)
        {
          
        }

        private ObservableCollection<ChangeType> _types;
        public ObservableCollection<ChangeType> Types
        {
            get { return _types; }
            set { SetProperty(ref _types, value); }
        }


        public async override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }

        private string GetArrow(double performance)
        {
            if (performance > 0)
                return ArrowUp;
            if (performance < 0)
                return ArrowDown;
            return ArrowStable;
        }

        private string GetTriangle(double performance)
        {
            if (performance > 0)
                return TriangleUp;
            if (performance < 0)
                return TriangleDown;
            return null;
        }

        private string GetColor(double performance)
        {
            if (performance > 0)
                return ColorGreen;
            if (performance < 0)
                return ColorRed;
            return ColorBlue;
        }

        private async Task SetupSector(string sector)
        {
        
        }

        private ObservableCollection<ShareOfStock> _shares;

        public ObservableCollection<ShareOfStock> Shares
        {
            get { return _shares; }
            set { SetProperty(ref _shares, value); }
        }

        private ObservableCollection<Report> _reports;
        public ObservableCollection<Report> Reports
        {
            get { return _reports; }
            set { SetProperty(ref _reports, value); }
        }

        private Command<ItemBase> NavigateToDetailsCommand { get; set; }

        private ItemBase _selectedItem;
        public ItemBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                    NavigateToDetailsCommand.Execute(_selectedItem);
                SelectedItem = null;
            }
        }

        private ObservableCollection<Bono> _bonos;
        private Task<List<Report>> _reportos;
        private Task<List<ShareOfStock>> _sharesStock;
        private Task<List<ChangeType>> _exchanges;
        private Task<List<Bono>> _bonosItems;

        public ObservableCollection<Bono> Bonos
        {
            get { return _bonos; }
            set { SetProperty(ref _bonos, value); }
        }

        private async void NavigateToDetailsAction(ItemBase obj)
        {
            await NavigationService.NavigateAsync("HomeDetailPage", new NavigationParameters() { { "item", obj } }, false, false);
        }
    }
}
