using BNV.Settings;
using Prism.Mvvm;
using Prism.Navigation;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BNV.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible, INotifyPropertyChanged
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public bool AlreadyLoaded { get; set; }

        protected string _colorStatus;
        private bool AlreadyShown;

        public string ColorStatus
        {
            get { return _colorStatus; }
            set { SetProperty(ref _colorStatus, value); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        { }

        public async Task ShowUnauthorizedAccess()
        {
            if (!AlreadyShown)
            {
                AlreadyShown = true;
                await App.Current.MainPage.DisplayAlert("Seguridad", "El tiempo de su sesión ha caducado por su seguridad. Por favor ingrese nuevamente.", "ACEPTAR");
                await CloseSessionActionExecute();
            }

        }

        public async Task CloseSessionActionExecute()
        {
            Config.FromCloseSession = true;
            await SecureStorage.SetAsync(Config.Token, string.Empty);
            await SecureStorage.SetAsync(Config.TokenExpiration, string.Empty);
            AlreadyShown = false;
            await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
        }
    }
}
