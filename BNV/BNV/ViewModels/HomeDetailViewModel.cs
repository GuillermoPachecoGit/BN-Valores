using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public HomeDetailViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            Events = ea;
            Events.GetEvent<NavigationColorEvent>().Subscribe(SetColor);

            TitleNav = "BNVR C";

            Data = new ObservableCollection<Model>()
            {
                new Model("5 Jun", 50),
                new Model("6 Jun", 70),
                new Model("7 Jun", 65),
                new Model("8 Jun", 57)
            };

            Average = "4.36";
            Maximum = "2.08";
            Minimum = "5.7";
            VolumenMax = "N.D.";
            VolumenMin = "N.D.";
            ValueRendimiento = "3.70";
            PercentageRendimiento = "3.55%";
            ValueVolumen = "13.0%";

            PercentageVolumen = "N.D.";
            ActionCommand = new Command(async () => await ActionExecute());
            TypeChange = "0.50 colones";
            Bonos = "0.50%";

            SubTitle = "Priv $ > 31 días";
            Events = ea;
            ChangePasswordCommand = new Command(async () => await ChangePasswordActionExecute());
            CloseSessionCommand = new Command(async () => await CloseSessionActionExecute());
            SetConfigCommand = new Command(() => {
                Events.GetEvent<NavigationTitleEvent>().Publish("Configuración");
                Color = "#AFBC24";
            });
            SetDetailsCommand = new Command(() => {
                Events.GetEvent<NavigationTitleEvent>().Publish("BNVR C");
                Color = Item?.ColorStatus;
            });

            BackCommand = new Command( async () => {
                await navigationService.GoBackAsync();
            });

            SetupFilters();
        }

        public IEventAggregator Events { get; set; }
        public string TitleNav { get; private set; }

        public string SubTitle { get; set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
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
                        TitleNav = "BNVR C";
                    }

                    Item = item;
                }
              
                IsBlue = Item.IsBlue;
                IsRed = Item.IsRed;
                IsGreen = Item.IsGreen;
                Triangle = Item.Triangle;

                Task.Run(async () =>
                {
                    var value = await SecureStorage.GetAsync(Config.MainPage);
                    if (!string.IsNullOrEmpty(value))
                        SelectedHomePage = value;
                    else
                        SelectedHomePage = "Reportos";
                });
            }
            catch (Exception ex)
            {

            }
        }

        public override void Destroy()
        {
            base.Destroy();
           // Events.GetEvent<NavigationColorEvent>().Publish("#AFBC24");
        }


        private async Task ActionExecute()
        {
            Random rnd = new Random();
            ValueRendimiento = "3.70";
            PercentageRendimiento = rnd.Next(100).ToString() + "%";
            ValueVolumen = rnd.Next(100).ToString() + "%";
            PercentageVolumen = "N.D.";
            Average = rnd.Next(100).ToString();
            Maximum = "2.08";
            Minimum = rnd.Next(100).ToString();
            VolumenMax = "N.D.";
            VolumenMin = rnd.Next(100).ToString();

            Data = new ObservableCollection<Model>()
            {
                new Model("5 Jun", rnd.Next(10,100)),
                new Model("6 Jun", rnd.Next(10,100)),
                new Model("7 Jun", rnd.Next(10,100)),
                new Model("8 Jun", rnd.Next(10,100))
            };
        }

        private string _average;
        public string Average
        {
            get { return _average; }
            set { _average = value; RaisePropertyChanged(); }
        }

        private string _maximum;
        public string Maximum
        {
            get { return _maximum; }
            set { _maximum = value; RaisePropertyChanged(); }
        }

        private string _minimum;
        public string Minimum
        {
            get { return _minimum; }
            set { _minimum = value; RaisePropertyChanged(); }
        }

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

        public ICommand ActionCommand { get; set; }

        private string _valueRendimiento;
        public string ValueRendimiento
        {
            get { return _valueRendimiento; }
            set { _valueRendimiento = value; RaisePropertyChanged(); }
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

            set { SetProperty(ref _seletedItem1, value); ActionCommand.Execute(null); }
        }

        private SfChip _seletedItem2;
        public SfChip SelectedItem2
        {
            get { return _seletedItem2; }

            set { SetProperty(ref _seletedItem2, value); ActionCommand.Execute(null); }
        }

        private SfChip _seletedItem3;
        public SfChip SelectedItem3
        {
            get { return _seletedItem3; }

            set { SetProperty(ref _seletedItem3, value); ActionCommand.Execute(null); }
        }

        private SfChip _seletedItem4;
        public SfChip SelectedItem4
        {
            get { return _seletedItem4; }

            set { SetProperty(ref _seletedItem4, value); ActionCommand.Execute(null); }
        }

        public ObservableCollection<Model> Data { get; set; }

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

        private string _selectedCoin;

        public string SelectedCoin
        {
            get { return _selectedCoin; }
            set { _selectedCoin = value; RaisePropertyChanged(); SecureStorage.SetAsync(Config.FilterCoin, value); Task.Run(() => Events.GetEvent<FilterCoinEvent>().Publish(value)); }

        }

        private string _selectedSector;


        public string SelectedSector
        {
            get { return _selectedSector; }
            set { _selectedSector = value; RaisePropertyChanged(); SecureStorage.SetAsync(Config.FilterSector, value); Task.Run(() => Events.GetEvent<FilterSectorEvent>().Publish(value)); }

        }

        private string _selectedHomePage;

        public string SelectedHomePage
        {
            get { return _selectedHomePage; }
            set { _selectedHomePage = value; RaisePropertyChanged(); SecureStorage.SetAsync(Config.MainPage, value); }
        }


        private async Task ChangePasswordActionExecute()
        {
            await NavigationService.NavigateAsync("ChangePasswordPage", new NavigationParameters() { { "title", "Cambio de contraseña" } }, false, false);
        }

        private async Task CloseSessionActionExecute()
        {
            Events.GetEvent<NavigationColorEvent>().Publish("#000000");
            await NavigationService.GoBackToRootAsync();
        }

        public ICommand SetConfigCommand { get; set; }

        public ICommand SetDetailsCommand { get; set; }

        public ICommand ChangePasswordCommand { get; set; }

        public ICommand CloseSessionCommand { get; set; }

        public ICommand BackCommand { get; set; }
    }

   
}

