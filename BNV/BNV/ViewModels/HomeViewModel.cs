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
using static BNV.Settings.Config;

namespace BNV.ViewModels
{
    public class HomeViewModel : ViewModelBase, IPageLifecycleAware
    {
        private const string ArrowDown = "red_arrow";
        private const string ArrowStable = "blue_arrow";
        private const string ArrowUp = "green_arrow";
        private const string TriangleDown = "triangle_down";
        private const string TriangleUp = "triangle_up";
        private const string ColorBlue = "#0059A0";
        private const string ColorRed = "#B22F2F";
        private const string ColorGreen = "#348832";
        private const string DefautLabelExchange = "0.50 colones";
        private const string DefaultLabelBonos = "0.50%";
        private const string LoadingMessageDialog = "Cargando los datos...";
        private const string ReportItemValueByDefault = "Reportos";
        private Task<List<Report>> _reportos;
        private Task<List<ShareOfStock>> _sharesStock;
        private Task<List<ChangeType>> _exchanges;
        private Task<List<Bono>> _bonosItems;
        private Task<SettingResponse> _settings;
        public Task<List<SystemDate>> _dates { get; set; }


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
                    var getSettings = App.ApiService.GetSettings(authorization).ContinueWith(settings => _settings = settings);
                    var getDates = App.ApiService.GetDates(authorization).ContinueWith(dates => _dates = dates);
                    

                    await Task.WhenAll(getShares, getReportos, getExchanges, getBonos, getSettings, getDates).ContinueWith(async result =>
                    {
                        if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                        {
                            //var settings = _settings?.Result;
                            //if (settings != null)
                            //{
                            //    BonosIndex = GetIndex(settings.BonosNotify);
                            //    ExchangesIndex = GetIndex(settings.BonosNotify);
                            //    SelectedHomePage = GetTab(settings.HomeScreen);
                            //    SelectedCoin = App.Currencies.FirstOrDefault(x => x.CodIdCurrency == settings.Currency);
                            //    SelectedSector = App.Sectors.FirstOrDefault(x => x.CodIdSector == settings.Sector);
                            //    App.SelectedCoin = SelectedCoin;
                            //    App.SelectedSector = SelectedSector;
                            //    App.HomePage = SelectedHomePage;
                            //    App.BonosIndexNotify = BonosIndex;
                            //    App.ExchangesIndexNotify = ExchangesIndex;
                            //}

                            var dates = _dates?.Result;
                            if (dates != null)
                            {
                                DateBonoShares = $"Al dia: {dates.First(x => x.TipRubro == ItemType.BonoAndShades).FecReferencia.ToString("dd/MM/yyyy")}";
                                DateReportos = $"Al dia: {dates.First(x => x.TipRubro == ItemType.Reportos).FecReferencia.ToString("dd/MM/yyyy")}";
                                DateExchange = $"Al dia: {dates.First(x => x.TipRubro == ItemType.Exchanges).FecReferencia.ToString("dd/MM/yyyy")}";
                            }

                            var itemsReports = _reportos?.Result;
                            if (itemsReports != null)
                                Reports = new ObservableCollection<Report>(itemsReports.Select(
                                    x =>
                                    {
                                        x.ColorStatus = GetColor(double.Parse(x.Variation));
                                        x.Triangle = GetTriangle(double.Parse(x.Variation));
                                        x.Title = x.Name;
                                        x.IsBlue = double.Parse(x.Variation) == 0;
                                        x.IsGreen = double.Parse(x.Variation) > 0;
                                        x.IsRed = double.Parse(x.Variation) < 0;
                                        x.Arrow = GetArrow(double.Parse(x.Variation));
                                        x.DateDisplay = DateReportos;
                                        return x;
                                    }).ToList());

                            var itemsShares = _sharesStock?.Result;
                            if (itemsShares != null)
                                Shares = new ObservableCollection<ShareOfStock>(itemsShares.Select(
                                    x =>
                                    {
                                        x.Sender = x.Name.Split(" ")[0] ?? string.Empty;
                                        x.Title = x.Name.ToString();
                                        x.Name = x.Name.Split(" ")[1] ?? string.Empty;
                                        x.ColorStatus = GetColor(double.Parse(x.Variation));
                                        x.Triangle = GetTriangle(double.Parse(x.Variation));
                                        x.IsBlue = double.Parse(x.Variation) == 0;
                                        x.IsGreen = double.Parse(x.Variation) > 0;
                                        x.IsRed = double.Parse(x.Variation) < 0;
                                        x.Arrow = GetArrow(double.Parse(x.Variation));
                                        x.DateDisplay = DateBonoShares;
                                        return x;
                                    }).ToList());

                            var itemsBonos = _bonosItems?.Result;
                            if (itemsBonos != null)
                                Bonos = new ObservableCollection<Bono>(itemsBonos.Select(
                                    x =>
                                    {
                                        x.Sender = x.Name.Split(" ")[0] ?? string.Empty;
                                        x.Title = x.Name.ToString();
                                        x.Name = x.Name.Split(" ")[1] ?? string.Empty;
                                        x.ColorStatus = GetColor(double.Parse(x.Variation));
                                        x.Triangle = GetTriangle(double.Parse(x.Variation));
                                        x.IsBlue = double.Parse(x.Variation) == 0;
                                        x.IsGreen = double.Parse(x.Variation) > 0;
                                        x.IsRed = double.Parse(x.Variation) < 0;
                                        x.Arrow = GetArrow(double.Parse(x.Variation));
                                        x.DateDisplay = DateBonoShares;
                                        return x;
                                    }).ToList());

                            var itemsExchanges = _exchanges?.Result;
                            if (itemsExchanges != null)
                                Types = new ObservableCollection<ChangeType>(itemsExchanges.Select(
                                    x =>
                                    {
                                        x.Title = x.Name;
                                        x.ColorStatus = GetColor(double.Parse(x.Variation));
                                        x.Triangle = GetTriangle(double.Parse(x.Variation));
                                        x.IsBlue = double.Parse(x.Variation) == 0;
                                        x.IsGreen = double.Parse(x.Variation) > 0;
                                        x.IsRed = double.Parse(x.Variation) < 0;
                                        x.Arrow = GetArrow(double.Parse(x.Variation));
                                        x.DateDisplay = DateExchange;
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

        private string GetTab(long homeScreen)
        {
            switch (homeScreen)
            {
                case 0:
                    return MainPageTypes.Reportos;
                case 1:
                    return MainPageTypes.Bonos;
                case 2:
                    return MainPageTypes.Acciones;
                case 3:
                    return MainPageTypes.TipoCambio;
                default:
                    return MainPageTypes.Reportos;
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
            // Config local
            var value = await SecureStorage.GetAsync(Config.MainPage);
            if (!string.IsNullOrEmpty(value))
                SelectedHomePage = value;
            else
                SelectedHomePage = ReportItemValueByDefault;
            SelectedCoin = App.SelectedCoin;
            SelectedSector = App.SelectedSector;

            Currencies = App.Currencies;
            Sectors = App.Sectors;
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

        public double BonosNotify { get; set; }

        public double ExchangeNotify { get; set; }

        public int BonosIndex { get;  set; }

        public int ExchangesIndex { get;  set; }

        private string _dateBonoShares;
        public string DateBonoShares
        {
            get { return _dateBonoShares; }
            set { SetProperty(ref _dateBonoShares, value); }
        }

        private string _dateReportos;
        public string DateReportos
        {
            get { return _dateReportos; }
            set { SetProperty(ref _dateReportos, value); }
        }

        private string _dateExchange;
        public string DateExchange
        {
            get { return _dateExchange; }
            set { SetProperty(ref _dateExchange, value); }
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

        private int GetIndex(double value)
        {
            switch (value)
            {
                case -1:
                    return 1;
                case 0.05:
                    return 2;
                case 0.10:
                    return 3;
                case 0.25:
                    return 4;
                case 0.50:
                    return 5;
                case 0.75:
                    return 6;
                case 1.00:
                    return 7;
                case 2.00:
                    return 8;
                case 3.00:
                    return 9;
                case 4.00:
                    return 10;
                case 5.00:
                    return 11;
                default:
                    return 5;
            }
        }

        private async void SaveSetting()
        {
            var email = await SecureStorage.GetAsync(Config.Email);
            var setting = new SettingsModel()
            {
                Sector = SelectedSector.CodIdSector,
                Currency = SelectedCoin.CodIdCurrency,
                BonosNotify = (long)BonosNotify,
                ExchangeRateNotify = (long)ExchangeNotify,
                //AccionesNotify = 1,
                HomeScreen = 1,
                Email = email
            };

            var token = await SecureStorage.GetAsync(Config.Token);
            using (UserDialogs.Instance.Loading(MessagesAlert.SavingSettings))
            {
                await App.ApiService.UpdateSettings($"Bearer {token}", setting).ContinueWith(result =>
                 {
                     if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                     {
                         UserDialogs.Instance.HideLoading();
                         UserDialogs.Instance.Alert(MessagesAlert.SuccessSaving);
                     }
                     else if (result.IsFaulted)
                     {
                         UserDialogs.Instance.HideLoading();
                         UserDialogs.Instance.Alert(MessagesAlert.ErrorSaving);
                     }
                     else if (result.IsCanceled) { }
                 }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}
