using System.Threading.Tasks;
using System.Windows.Input;
using BNV.Events;
using BNV.Settings;
using Prism.AppModel;
using Prism.Events;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class SettingViewModel : ViewModelBase, IPageLifecycleAware
    {
        public SettingViewModel(INavigationService navigationService, IEventAggregator ea)
          : base(navigationService)
        {
            Title = "Register Page";
            TypeChange = "0.50 colones";
            BonosLabel = "0.50%";
            Events = ea;
            ChangePasswordCommand = new Command(async () => await ChangePasswordActionExecute());
            CloseSessionCommand = new Command(async () => await CloseSessionActionExecute());
        }

        public IEventAggregator Events { get; }


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

        public string BonosLabel
        {
            get { return _bonos; }
            set { _bonos = value; RaisePropertyChanged(); }

        }


        private string _selectedCoin;

        public string SelectedCoin
        {
            get { return _selectedCoin; }
            set { _selectedCoin = value; RaisePropertyChanged(); SecureStorage.SetAsync(Config.FilterCoin, value); Events.GetEvent<FilterCoinEvent>().Publish(value); }

        }

        private string _selectedSector;


        public string SelectedSector
        {
            get { return _selectedSector; }
            set { _selectedSector = value; RaisePropertyChanged(); SecureStorage.SetAsync(Config.FilterSector, value); Events.GetEvent<FilterSectorEvent>().Publish(value); }

        }

        private string _selectedHomePage;

        public string SelectedHomePage
        {
            get { return _selectedHomePage; }
            set { _selectedHomePage = value; RaisePropertyChanged(); SecureStorage.SetAsync(Config.MainPage, value); }
        }


        private async Task ChangePasswordActionExecute()
        {
            await NavigationService.NavigateAsync("ChangePasswordPage", null, false, false);
        }

        private async Task CloseSessionActionExecute()
        {
            await NavigationService.GoBackToRootAsync();
        }

        public async void OnAppearing()
        {
            Events.GetEvent<NavigationTitleEvent>().Publish("Configuración");
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
    }
}
