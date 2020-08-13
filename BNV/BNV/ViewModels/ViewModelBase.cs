using Acr.UserDialogs;
using BNV.ServicesWebAPI;
using BNV.Settings;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

        public bool IsBusy { get; set; }

        public IUserDialogs PageDialog = UserDialogs.Instance;

        public async Task RunSafe(Task task, bool ShowLoading = true, string loadinMessage = null)
        {
            try
            {
                if (IsBusy) return;

                IsBusy = true;

                if (ShowLoading) UserDialogs.Instance.ShowLoading(loadinMessage ?? "Cargando");

                await task;
            }
            catch (ApiException ex)
            {
                IsBusy = false;
                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(ex.ToString());
                await ShowUnauthorizedAccess();
            }
            catch (UnauthorizedAccessException e)
            {
                IsBusy = false;
                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(e.ToString());
                await ShowUnauthorizedAccess();
            }
            catch (Exception e)
            {
                IsBusy = false;
                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(e.ToString());
                await App.Current.MainPage.DisplayAlert("Error", "Revisa tu conexion a internet", "Aceptar");

            }
            finally
            {
                IsBusy = false;
                if (ShowLoading) UserDialogs.Instance.HideLoading();
            }
        }

        public async Task ShowUnauthorizedAccess()
        {
            await App.Current.MainPage.DisplayAlert("Seguridad", "El tiempo de su sesión ha caducado. Por su seguridad ingrese nuevamente, por favor.", "Aceptar");
            await CloseSessionActionExecute();
        }

        private async Task CloseSessionActionExecute()
        {
            var token = await SecureStorage.GetAsync(Config.Token);
            var response = await App.ApiService.CloseSession($"Bearer {token}");
            await SecureStorage.SetAsync(Config.Token, string.Empty);
            await SecureStorage.SetAsync(Config.TokenExpiration, string.Empty);
            await NavigationService.GoBackAsync();
        }
    }
}
