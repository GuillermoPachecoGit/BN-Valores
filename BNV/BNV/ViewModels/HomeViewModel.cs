using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
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
        private const string ArrowDown = "red_arrow";
        private const string ArrowStable = "blue_arrow";
        private const string ArrowUp = "green_arrow";
        private const string TriangleDown = "triangle_down";
        private const string TriangleUp = "triangle_up";
        private const string ColorBlue = "#00579F";
        private const string ColorRed = "#B51010";
        private const string ColorGreen = "#81B71A";
        private const string DefautLabelExchange = "0.50 colones";
        private const string DefaultLabelBonos = "0.50%";
        private const string LoadingMessageDialog = "Cargando los datos...";
        private const string ReportItemValueByDefault = "Reportos";
        private Task<List<Report>> _reportos;
        private Task<List<ShareOfStock>> _sharesStock;
        private Task<List<ChangeType>> _exchanges;
        private Task<List<Bono>> _bonosItems;


        public HomeViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            NavigateToDetailsCommand = new Command<ItemBase>(NavigateToDetailsAction);
            TypeChange = DefautLabelExchange;
            BonosLabel = DefaultLabelBonos;
            Events = ea;
            ChangePasswordCommand = new Command(async () => await ChangePasswordActionExecute());
            CloseSessionCommand = new Command(async () => await CloseSessionActionExecute());
            _alreadyLoaded = false;
        }

        private async Task RefreshDataAsync()
        {
            try
            {
                using (UserDialogs.Instance.Loading(LoadingMessageDialog))
                {
                    await Task.Delay(15);
                    var token = await SecureStorage.GetAsync(Config.Token);
                    var authorization = $"Bearer {token}";
                    var reportParam = new ItemsParamModel() { Sector = _selectedSector?.CodIdSector, Currency = _selectedCoin?.CodIdCurrency };
                    var apiService = NetworkService.GetApiService();
                    var getShares = App.ApiService.GetSharesStock(authorization, reportParam).ContinueWith(shares => _sharesStock = shares);
                    var getReportos = App.ApiService.GetReportos(authorization, reportParam).ContinueWith(reportos => _reportos = reportos);
                    var getExchanges = App.ApiService.GetExchangeRates(authorization, reportParam).ContinueWith(exchanges => _exchanges = exchanges);
                    var getBonos = App.ApiService.GetBonos(authorization, reportParam).ContinueWith(bonos => _bonosItems = bonos);

                    await Task.WhenAll(getShares, getReportos, getExchanges, getBonos).ContinueWith(async result =>
                    {
                        if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                        {
                            var itemsReports = _reportos?.Result;
                            if (itemsReports != null)
                                Reports = new ObservableCollection<Report>(itemsReports.Select(
                                    x =>
                                    {
                                        x.ColorStatus = GetColor(x.Variation);
                                        x.Triangle = GetTriangle(x.Variation);
                                        x.IsBlue = x.Variation == 0;
                                        x.IsGreen = x.Variation > 0;
                                        x.IsRed = x.Variation < 0;
                                        x.Arrow = GetArrow(x.Variation);
                                        x.VolumeDisplay = x.Volume.ToString().Length >= 7 ? $"{x.Volume / 1000000}M" : x.Volume == 0 ? "N.D." : x.Volume.ToString();
                                        x.VariationDisplay = x.Variation > 0 ? $"+{x.Variation.ToString("F2")}%" : $"{x.Variation.ToString("F2")}%";
                                        return x;
                                    }).ToList());

                            var itemsShares = _sharesStock?.Result;
                            if (itemsShares != null)
                                Shares = new ObservableCollection<ShareOfStock>(itemsShares.Select(
                                    x =>
                                    {
                                        x.ColorStatus = GetColor(x.Variation);
                                        x.Triangle = GetTriangle(x.Variation);
                                        x.IsBlue = x.Variation == 0;
                                        x.IsGreen = x.Variation > 0;
                                        x.IsRed = x.Variation < 0;
                                        x.VolumeDisplay = x.Volume.ToString().Length >= 7 ? $"{x.Volume / 1000000}M" : x.Volume == 0 ? "N.D." : x.Volume.ToString();
                                        x.VariationDisplay = x.Variation > 0 ? $"+{x.Variation.ToString("F2")}%" : $"{x.Variation.ToString("F2")}%";
                                        return x;
                                    }).ToList());

                            var itemsBonos = _bonosItems?.Result;
                            if (itemsBonos != null)
                                Bonos = new ObservableCollection<Bono>(itemsBonos.Select(
                                    x =>
                                    {
                                        x.ColorStatus = GetColor(x.Variation);
                                        x.Triangle = GetTriangle(x.Variation);
                                        x.IsBlue = x.Variation == 0;
                                        x.IsGreen = x.Variation > 0;
                                        x.IsRed = x.Variation < 0;
                                        x.VolumeDisplay = x.Volume.ToString().Length >= 7 ? $"{x.Volume / 1000000}M" : x.Volume == 0 ? "N.D." : x.Volume.ToString();
                                        x.VariationDisplay = x.Variation > 0 ? $"+{x.Variation.ToString("F2")}%" : $"{x.Variation.ToString("F2")}%";
                                        return x;
                                    }).ToList());

                            var itemsExchanges = _exchanges?.Result;
                            if (itemsExchanges != null)
                                Types = new ObservableCollection<ChangeType>(itemsExchanges.Select(
                                    x =>
                                    {
                                        x.ColorStatus = GetColor(x.Variation);
                                        x.Triangle = GetTriangle(x.Variation);
                                        x.IsBlue = x.Variation == 0;
                                        x.IsGreen = x.Variation > 0;
                                        x.IsRed = x.Variation < 0;
                                        x.Arrow = GetArrow(x.Variation);
                                        x.VolumeDisplay = x.Volume.ToString().Length >= 7 ? $"{x.Volume / 1000000}M" : x.Volume == 0 ? "N.D." : x.Volume.ToString();
                                        x.VariationDisplay = x.Variation > 0 ? $"+{x.Variation.ToString("F2")}%" : $"{x.Variation.ToString("F2")}%";
                                        return x;
                                    }).ToList());
                            _alreadyLoaded = true;
                        }
                        else if (result.IsFaulted)
                        {
                            UserDialogs.Instance.HideLoading();
                        }
                        else if (result.IsCanceled)
                        {
                            UserDialogs.Instance.HideLoading();
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        public async void OnAppearing()
        {
            await SetupConfig();
            RefreshData();
        }

        public void OnDisappearing(){ }

        public ICommand ChangePasswordCommand { get; set; }

        public ICommand CloseSessionCommand { get; set; }

        private bool _alreadyLoaded;

        public event PropertyChangedEventHandler PropertyChanged;

        public IEventAggregator Events { get; }

        private string _date;
        public string DataDate
        {
            get { return _date; }
            set { _date = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Currency> _currencies;
        public ObservableCollection<Currency> Currencies
        {
            get { return _currencies; }
            set { SetProperty(ref _currencies, value); }
        }

        private ObservableCollection<Sector> _sectors;
        public ObservableCollection<Sector> Sectors
        {
            get { return _sectors; }
            set { SetProperty(ref _sectors, value); }
        }

        public async Task SetupFilters()
        {
            var home = await SecureStorage.GetAsync(Config.MainPage);
            if (home != null)
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

        private Currency? _selectedCoin;
        public Currency? SelectedCoin
        {
            get { return _selectedCoin; }
            set {
                _selectedCoin = value;
                RaisePropertyChanged();
                App.SelectedCoin = value;
                SecureStorage.SetAsync(Config.FilterCoin, value.CodIdCurrency.ToString());
                if (_alreadyLoaded)
                    RefreshData();
            }
        }

        private async void RefreshData()
        {
            await RefreshDataAsync().ConfigureAwait(false);
        }

        private Sector? _selectedSector;
        public Sector? SelectedSector
        {
            get { return _selectedSector; }
            set {
                _selectedSector = value;
                RaisePropertyChanged();
                App.SelectedSector = value;
                SecureStorage.SetAsync(Config.FilterSector, value.CodIdSector.ToString());
                if (_alreadyLoaded)
                    RefreshData();
            }
        }

        private string _selectedHomePage;
        public string SelectedHomePage
        {
            get { return _selectedHomePage; }
            set { _selectedHomePage = value; RaisePropertyChanged(); SecureStorage.SetAsync(Config.MainPage, value); }
        }

        private async Task SetupConfig()
        {
            var value = await SecureStorage.GetAsync(Config.MainPage);
            if (!string.IsNullOrEmpty(value))
                SelectedHomePage = value;
            else
                SelectedHomePage = ReportItemValueByDefault;
            Currencies = App.Currencies;
            Sectors = App.Sectors;
            SelectedCoin = App.SelectedCoin;
            SelectedSector = App.SelectedSector;
            DataDate = $"Al dia: {DateTime.Today.ToShortDateString()}";
        }

        private async Task ChangePasswordActionExecute()
        {
            await NavigationService.NavigateAsync("ChangePasswordPage", new NavigationParameters() { { "title", "Cambio de contraseña" } }, false, false);
        }

        private async Task CloseSessionActionExecute()
        {
            var token = await SecureStorage.GetAsync(Config.Token);
            var response = await App.ApiService.CloseSession($"Bearer {token}");
            await SecureStorage.SetAsync(Config.Token, string.Empty);
            await SecureStorage.SetAsync(Config.TokenExpiration, string.Empty);
            await NavigationService.GoBackAsync();
        }

        private TabItemCollection items;
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

        private ObservableCollection<ChangeType> _types;
        public ObservableCollection<ChangeType> Types
        {
            get { return _types; }
            set { SetProperty(ref _types, value); }
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
        public ObservableCollection<Bono> Bonos
        {
            get { return _bonos; }
            set { SetProperty(ref _bonos, value); }
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

        private async void NavigateToDetailsAction(ItemBase obj)
        {
            await NavigationService.NavigateAsync("HomeDetailPage", new NavigationParameters() { { "item", obj } }, false, false);
        }
    }
}
