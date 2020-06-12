using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BNV.Events;
using BNV.Models;
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
        }

        public void OnDisappearing()
        {

        }

        public ICommand ChangePasswordCommand { get; set; }

        public ICommand CloseSessionCommand { get; set; }

        public IEventAggregator Events { get; }

        private async Task ChangePasswordActionExecute()
        {
            await NavigationService.NavigateAsync("ChangePasswordPage", null, false, false);
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
            var shares = new List<ShareOfStock>();
            var bonos = new List<Bono>();
            var reportos = new List<Report>();
            var types = new List<ShareOfStock>();
            var value = coin;

            if (value != null && value == Config.CoinTypes.CoinColon)
            {
                for (int i = 0; i < 1; i++)
                {
                    shares.Add(new ShareOfStock() { ColorStatus = ColorBlue, IsBlue = true });
                    types.Add(new ShareOfStock() { ColorStatus = ColorRed, IsRed = true, Triangle = TriangleDown });
                    types.Add(new ShareOfStock() { ColorStatus = ColorRed, IsRed = true, Triangle = TriangleDown });
                    reportos.Add(new Report() { ColorStatus = ColorRed, Arrow = ArrowDown, Triangle = TriangleDown, IsRed = true });
                    reportos.Add(new Report() { ColorStatus = ColorBlue, Arrow = ArrowStable, IsBlue = true });
                    reportos.Add(new Report() { ColorStatus = ColorGreen, Arrow = ArrowUp, Triangle = TriangleUp, IsGreen = true });
                    bonos.Add(new Bono() { ColorStatus = ColorRed, IsRed = true, Triangle = TriangleDown });
                }
            }
            else if (value != null && value == Config.CoinTypes.CoinDolar)
            {
                for (int i = 0; i < 3; i++)
                {
                    shares.Add(new ShareOfStock() { ColorStatus = ColorBlue, IsBlue = true });
                    types.Add(new ShareOfStock() { ColorStatus = ColorRed, IsRed = true, Triangle = TriangleDown });
                    bonos.Add(new Bono() { ColorStatus = ColorRed, IsRed = true, Triangle = TriangleDown });
                    reportos.Add(new Report() { ColorStatus = ColorRed, Arrow = ArrowDown, Triangle = TriangleDown, IsRed = true });
                    reportos.Add(new Report() { ColorStatus = ColorBlue, Arrow = ArrowStable, IsBlue = true });
                    reportos.Add(new Report() { ColorStatus = ColorGreen, Arrow = ArrowUp, Triangle = TriangleUp, IsGreen = true });
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    shares.Add(new ShareOfStock() { ColorStatus = ColorBlue, IsBlue = true });
                    types.Add(new ShareOfStock() { ColorStatus = ColorRed, IsRed = true, Triangle = TriangleDown });

                    bonos.Add(new Bono() { ColorStatus = ColorRed, IsRed = true, Triangle = TriangleDown });
                    reportos.Add(new Report() { ColorStatus = ColorRed, Arrow = ArrowDown, Triangle = TriangleDown, IsRed = true });
                    reportos.Add(new Report() { ColorStatus = ColorBlue, Arrow = ArrowStable, IsBlue = true });
                    reportos.Add(new Report() { ColorStatus = ColorGreen, Arrow = ArrowUp, Triangle = TriangleUp, IsGreen = true });
                }
            }

            Shares = new ObservableCollection<ShareOfStock>(shares);
            Types = new ObservableCollection<ShareOfStock>(types);
            Bonos = new ObservableCollection<Bono>(bonos);
            Reports = new ObservableCollection<Report>(reportos);
        }

        private ObservableCollection<ShareOfStock> _types;
        public ObservableCollection<ShareOfStock> Types
        {
            get { return _types; }
            set { SetProperty(ref _types, value); }
        }


        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            Task.Run(async () =>
            {
                await SetupCoin(null);
            });
        }

        private async Task SetupSector(string sector)
        {
            var shares = new List<ShareOfStock>();
            var types = new List<ShareOfStock>();
            var bonos = new List<Bono>();
            var reportos = new List<Report>();
            var value = sector;

            if (value != null && value == Config.SectorTypes.Public)
            {
                for (int i = 0; i < 4; i++)
                {
                    types.Add(new ShareOfStock() { ColorStatus = ColorRed, IsRed = true, Triangle = TriangleDown });
                    bonos.Add(new Bono() { ColorStatus = ColorRed, IsRed = true, Triangle = TriangleDown });
                    shares.Add(new ShareOfStock() { ColorStatus = ColorBlue, IsBlue = true });
                    reportos.Add(new Report() { ColorStatus = ColorRed, Arrow = ArrowDown, Triangle = TriangleDown, IsRed = true });
                    reportos.Add(new Report() { ColorStatus = ColorBlue, Arrow = ArrowStable, IsBlue = true });
                    reportos.Add(new Report() { ColorStatus = ColorGreen, Arrow = ArrowUp, Triangle = TriangleUp, IsGreen = true });
                }
            }
            else if (value != null && value == Config.SectorTypes.Privado)
            {
                for (int i = 0; i < 3; i++)
                {
                    types.Add(new ShareOfStock() { ColorStatus = ColorRed, IsRed = true, Triangle = TriangleDown });
                    bonos.Add(new Bono() { ColorStatus = ColorRed, IsRed = true, Triangle = TriangleDown });
                    shares.Add(new ShareOfStock() { ColorStatus = ColorBlue, IsBlue = true });
                    reportos.Add(new Report() { ColorStatus = ColorRed, Arrow = ArrowDown, Triangle = TriangleDown, IsRed = true });
                    reportos.Add(new Report() { ColorStatus = ColorBlue, Arrow = ArrowStable, IsBlue = true });
                    reportos.Add(new Report() { ColorStatus = ColorGreen, Arrow = ArrowUp, Triangle = TriangleUp, IsGreen = true });
                }
            }
            else if (value != null && value == Config.SectorTypes.Mixto)
            {
                for (int i = 0; i < 4; i++)
                {
                    shares.Add(new ShareOfStock() { ColorStatus = ColorBlue, IsBlue = true });
                    types.Add(new ShareOfStock() { ColorStatus = ColorRed, IsRed = true, Triangle = TriangleDown });
                    bonos.Add(new Bono() { ColorStatus = ColorRed, IsRed = true, Triangle = TriangleDown });
                    shares.Add(new ShareOfStock() { ColorStatus = ColorBlue, IsBlue = true });
                    reportos.Add(new Report() { ColorStatus = ColorRed, Arrow = ArrowDown, Triangle = TriangleDown, IsRed = true });
                    reportos.Add(new Report() { ColorStatus = ColorGreen, Arrow = ArrowUp, Triangle = TriangleUp, IsGreen = true });
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    bonos.Add(new Bono() { ColorStatus = ColorRed, IsRed = true, Triangle = TriangleDown });
                    shares.Add(new ShareOfStock() { ColorStatus = ColorBlue, IsBlue = true });
                    types.Add(new ShareOfStock() { ColorStatus = ColorRed, IsRed = true, Triangle = TriangleDown });
                    reportos.Add(new Report() { ColorStatus = ColorRed, Arrow = ArrowDown, Triangle = TriangleDown, IsRed = true });
                    reportos.Add(new Report() { ColorStatus = ColorGreen, Arrow = ArrowUp, Triangle = TriangleUp, IsGreen = true });
                }
            }

            Shares = new ObservableCollection<ShareOfStock>(shares);
            Types = new ObservableCollection<ShareOfStock>(types);
            Bonos = new ObservableCollection<Bono>(bonos);
            Reports = new ObservableCollection<Report>(reportos);
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
                await SetupCoin(null);
                AlreadyLoaded = true;
            });
        }
    }
}
