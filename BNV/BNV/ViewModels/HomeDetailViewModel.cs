using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BNV.Events;
using BNV.Models;
using BNV.Settings;
using Prism.Events;
using Prism.Navigation;
using Syncfusion.XForms.Buttons;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class HomeDetailViewModel : ViewModelBase
    {
        private const string DefaultLabelExchange = "0.50 colones";
        private const string DefaultLabelBonos = "0.50%";
        private const string ColorNavigationConfig = "#B8BE14";
        private const string TitleNavConfig = "Configuración";
        private const int Time = 7;

        public HomeDetailViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            Events = ea;
            Events.GetEvent<NavigationColorEvent>().Subscribe(SetColor);
            ActionCommand = new Command<string>(async (value) => await ActionExecute(value));
            TypeChange = DefaultLabelExchange;
            Bonos = DefaultLabelBonos;
            ChangePasswordCommand = new Command(async () => await ChangePasswordActionExecute());
            CloseSessionCommand = new Command(async () => await CloseSessionActionExecute());
            SetConfigCommand = new Command(() => {
                Events.GetEvent<NavigationTitleEvent>().Publish(TitleNavConfig);
                Color = ColorNavigationConfig;
            });
            SetDetailsCommand = new Command(() => {
                Events.GetEvent<NavigationTitleEvent>().Publish(Item.Name);
                Color = Item?.ColorStatus;
            });

            BackCommand = new Command( async () => {
                await navigationService.GoBackAsync();
            });

            _appeared = false;
            SetupHomePage();
        }

        public ICommand SetConfigCommand { get; set; }

        public ICommand SetDetailsCommand { get; set; }

        public ICommand ChangePasswordCommand { get; set; }

        public ICommand CloseSessionCommand { get; set; }

        public ICommand BackCommand { get; set; }

        public IEventAggregator Events { get; set; }

        public string TitleNav { get; private set; }

        public string SubTitle { get; set; }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            try
            {
                if (Item == null)
                {
                    var item = parameters.GetValue<ItemBase>("item");

                    if (item != null)
                    {
                        Item = item;
                        ColorStatus = item.ColorStatus;
                        Color = item.ColorStatus;
                        TitleNav = item.Title;
                        SubTitle = item.Description;
                    }
                    Item = item;
                }
              
                IsBlue = Item.IsBlue;
                IsRed = Item.IsRed;
                IsGreen = Item.IsGreen;
                Triangle = Item.Triangle;

                GetDetailsAsync(Time);

                _ = Task.Run(async () =>
                  {
                      var value = await SecureStorage.GetAsync(Config.MainPage);
                      if (!string.IsNullOrEmpty(value))
                          SelectedHomePage = value;
                      else
                          SelectedHomePage = "Reportos";
                  });

                //SelectedCoin = App.SelectedCoin;
                //SelectedSector = App.SelectedSector;
                //SelectedHomePage = App.HomePage;
                //BonosIndex = App.BonosIndexNotify;
                //ExchangesIndex = App.ExchangesIndexNotify;
            }
            catch (Exception ex)
            {
            }
        }

        public async void GetDetailsAsync(int time)
        {
            try
            {
                DataDate = Item?.DateDisplay;
                using (UserDialogs.Instance.Loading("Cargando los datos..."))
                {
                    await Task.Delay(5);
                    var token = await SecureStorage.GetAsync(Config.Token);
                    var authorization = $"Bearer {token}";
                    if (Item is Report)
                    {
                        await App.ApiService.GetReportoDetails(authorization, Item.Id, new DetailParamModel() { Time = time })
                         .ContinueWith(async result =>
                         {
                             if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                             {
                                 PopulateData(result);
                             }
                             else if (result.IsFaulted) {

                                 if (result?.Exception?.Message.Contains("401") ?? true)
                                 {
                                     await ShowUnauthorizedAccess();
                                     return;
                                 }
                             }
                             else if (result.IsCanceled) { }
                         }, TaskScheduler.FromCurrentSynchronizationContext())// execute in main/UI thread.
                         .ConfigureAwait(false);
                        _appeared = true;
                        _loading = false;
                        return;
                    }

                    if (Item is Bono)
                    {
                        await App.ApiService.GetBonoDetails(authorization, Item.Id, new DetailParamModel() { Time = time })
                         .ContinueWith(async result =>
                         {
                             if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                             {
                                 PopulateData(result);
                             }
                             else if (result.IsFaulted) {
                                 if (result?.Exception?.Message.Contains("401") ?? true)
                                 {
                                     await ShowUnauthorizedAccess();
                                     return;
                                 }
                             }
                             else if (result.IsCanceled) { }
                         }, TaskScheduler.FromCurrentSynchronizationContext())// execute in main/UI thread.
                         .ConfigureAwait(false);
                        _appeared = true;
                        _loading = false;
                        return;
                    }

                    if (Item is ChangeType)
                    {
                        await App.ApiService.GetExchangeDetails(authorization, Item.Id, new DetailParamModel() { Time = time })
                         .ContinueWith(async result =>
                         {
                             if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                             {
                                 PopulateData(result);
                             }
                             else if (result.IsFaulted) {
                                 if (result?.Exception?.Message.Contains("401") ?? true)
                                 {
                                     await ShowUnauthorizedAccess();
                                     return;
                                 }
                             }
                             else if (result.IsCanceled) { }
                         }, TaskScheduler.FromCurrentSynchronizationContext())// execute in main/UI thread.
                         .ConfigureAwait(false);
                        _appeared = true;
                        _loading = false;
                        return;
                    }

                    if (Item is ShareOfStock)
                    {
                        await App.ApiService.GetShareOfStockDetails(authorization, Item.Id, new DetailParamModel() { Time = time })
                         .ContinueWith(async result =>
                         {
                             if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                             {
                                 PopulateData(result);
                             }
                             else if (result.IsFaulted) {
                                 if (result?.Exception?.Message.Contains("401") ?? true)
                                 {
                                     await ShowUnauthorizedAccess();
                                     return;
                                 }
                             }
                             else if (result.IsCanceled) { }
                         }, TaskScheduler.FromCurrentSynchronizationContext())// execute in main/UI thread.
                         .ConfigureAwait(false);
                        _appeared = true;
                        _loading = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void PopulateData(Task<Details> result)
        {
            var value = result.Result;
            long volMax;
            long.TryParse(value.TradedVolumeMax,out volMax);
            long volMin;
            long.TryParse(value.TradedVolumeMin, out volMin);
            long avr;
            long.TryParse(value.TradedVolumeAverage, out avr);

            VolumenMax = value.TradedVolumeMax;
            VolumenMin = value.TradedVolumeMin;
            Average = value.TradedVolumeAverage;
            Maximum = double.Parse(value.ValueMax, CultureInfo.InvariantCulture);
            Minimum = double.Parse(value.ValueMin, CultureInfo.InvariantCulture);
            MaximumDisplay = value.ValueMax;
            MinimumDisplay = value.ValueMin;
            ValueRendimiento = Item.Performance;
            PercentageRendimiento = Item.Price;
            ValueVolumen = Item.Volume;
            PercentageVolumen = Item.Volume;
            
            var list = new List<Model>();
            foreach(var dataItem in value.Data.OrderBy(x => x.Date))
            {
                list.Add(new Model(dataItem.Date.ToString("d - MMM", CultureInfo.CreateSpecificCulture("es-MX")), dataItem.Price));
            }
            Data = new ObservableCollection<Model>(list);
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        private async Task ActionExecute(string time)
        {
            if (_loading)
                return;
            _loading = true;
            GetDetailsAsync(GetDays(time));
        }

        private int GetDays(string time)
        {
            switch (time)
            {
                case "1 sem":
                    return 7;
                case "1 mes":
                    return 30;
                case "3 meses":
                    return 90;
                case "6 meses":
                    return 180;
                case "1 año":
                    return 360;
                case "2 años":
                    return 720;
                default:
                    return 360;
            }
        }

        private string _average;
        public string Average
        {
            get { return _average; }
            set { _average = value; RaisePropertyChanged(); }
        }

        private double _maximum;
        public double Maximum
        {
            get { return _maximum; }
            set { _maximum = value; RaisePropertyChanged(); }
        }

        private double _minimum;
        public double Minimum
        {
            get { return _minimum; }
            set { _minimum = value; RaisePropertyChanged(); }
        }

        public string MaximumDisplay { get; private set; }
        public string MinimumDisplay { get; private set; }

        private string _volumenMax;
        public string VolumenMax
        {
            get { return _volumenMax; }
            set { _volumenMax = value; RaisePropertyChanged(); }
        }

        private string _volumenMin;
        public string VolumenMin
        {
            get { return _volumenMin; }
            set { _volumenMin = value; RaisePropertyChanged(); }
        }

        private void SetColor(string obj)
        {
            Color = obj;
        }

        public Command<string> ActionCommand { get; set; }

        private string _valueRendimiento;
        public string ValueRendimiento
        {
            get { return _valueRendimiento; }
            set { _valueRendimiento = value; RaisePropertyChanged(); }
        }

        private string _date;
        public string DataDate
        {
            get { return _date; }
            set { _date = value; RaisePropertyChanged(); }
        }
        
        private string _percentageRendimiento;
        public string PercentageRendimiento
        {
            get { return _percentageRendimiento; }
            set { _percentageRendimiento = value; RaisePropertyChanged(); }
        }

        private string _valueVolumen;
        public string ValueVolumen
        {
            get { return _valueVolumen; }
            set { _valueVolumen = value; RaisePropertyChanged(); }
        }

        private string _percentageVolumen;
        public string PercentageVolumen
        {
            get { return _percentageVolumen; }
            set { _percentageVolumen = value; RaisePropertyChanged(); }
        }

        private SfChip _seletedItem1;
        public SfChip SelectedItem1
        {
            get { return _seletedItem1; }

            set { SetProperty(ref _seletedItem1, value); ActionCommand.Execute(value.Text); }
        }

        private SfChip _seletedItem2;
        public SfChip SelectedItem2
        {
            get { return _seletedItem2; }

            set { SetProperty(ref _seletedItem2, value); ActionCommand.Execute(value.Text); }
        }

        private SfChip _seletedItem3;
        public SfChip SelectedItem3
        {
            get { return _seletedItem3; }

            set { SetProperty(ref _seletedItem3, value); ActionCommand.Execute(value.Text); }
        }

        private SfChip _seletedItem4;
        public SfChip SelectedItem4
        {
            get { return _seletedItem4; }

            set { SetProperty(ref _seletedItem4, value); ActionCommand.Execute(value.Text); }
        }

        public ObservableCollection<Model> Data { get; set; }

        public ObservableCollection<Currency> Currencies { get; set; }

        public ObservableCollection<Sector> Sectors { get; set; }

        private string _color;
        public string Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        public class Model
        {
            public string Month { get; set; }

            public double Target { get; set; }

            public Model(string xValue, double target)
            {
                Target = target;
                Month = xValue;
            }
        }

        public async Task SetupHomePage()
        {
            var home = await SecureStorage.GetAsync(Config.MainPage);
            if (home != null)
                SelectedHomePage = home;

            Currencies = App.Currencies;
            Sectors = App.Sectors;
            SelectedCoin = App.SelectedCoin;
            SelectedSector = App.SelectedSector;
        }

        private string _typeChange;
        public string TypeChange
        {
            get { return _typeChange; }
            set { _typeChange = value; RaisePropertyChanged(); }
        }

        private string _bonos;
        public string Bonos
        {
            get { return _bonos; }
            set { _bonos = value; RaisePropertyChanged(); }
        }

        private bool _isRed;
        public bool IsRed
        {
            get { return _isRed; }
            set { _isRed = value; RaisePropertyChanged(); }
        }

        private bool _isGreen;
        public bool IsGreen
        {
            get { return _isGreen; }
            set { _isGreen = value; RaisePropertyChanged(); }
        }

        public string Triangle { get; private set; }

        private bool _isBlue;

        public ItemBase Item { get; private set; }

        public bool IsBlue
        {
            get { return _isBlue; }
            set { _isBlue = value; RaisePropertyChanged(); }
        }

        private Currency _selectedCoin;
        public Currency SelectedCoin
        {
            get { return _selectedCoin; }
            set {
                _selectedCoin = value;
                RaisePropertyChanged();
                App.SelectedCoin = value;
                SecureStorage.SetAsync(Config.FilterCoin, value.CodIdCurrency.ToString());
            }
        }

        private Sector _selectedSector;
        public Sector SelectedSector
        {
            get { return _selectedSector; }
            set {
                _selectedSector = value;
                RaisePropertyChanged();
                App.SelectedSector = value;
                SecureStorage.SetAsync(Config.FilterSector, value.CodIdSector.ToString());
            }
        }

        private string _selectedHomePage;
        private bool _appeared;
        private bool _loading;

        public string SelectedHomePage
        {
            get { return _selectedHomePage; }
            set { _selectedHomePage = value; RaisePropertyChanged(); SecureStorage.SetAsync(Config.MainPage, value); }
        }

        public int BonosIndex { get; set; }

        public int ExchangesIndex { get; set; }
        public double ExchangeNotify { get; set; }
        public double BonosNotify { get; set; }

        private async Task ChangePasswordActionExecute()
        {
            await NavigationService.NavigateAsync("ChangePasswordPage", new NavigationParameters() { { "title", "Cambio de contraseña" } }, false, false);
        }

        private async Task CloseSessionActionExecute()
        {
            Events.GetEvent<NavigationColorEvent>().Publish("#000000");
            var token = await SecureStorage.GetAsync(Config.Token);
            var response = await App.ApiService.CloseSession($"Bearer {token}");
            await SecureStorage.SetAsync(Config.Token, string.Empty);
            await SecureStorage.SetAsync(Config.TokenExpiration, string.Empty);
            await NavigationService.GoBackToRootAsync();
        }
    }
}

