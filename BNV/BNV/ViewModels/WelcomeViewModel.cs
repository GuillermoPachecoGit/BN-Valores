using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.AppModel;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class WelcomeViewModel : ViewModelBase, IPageLifecycleAware
    {
        public WelcomeViewModel(INavigationService navigationService)
          : base(navigationService)
        {

            RegisterCommand = new Command(RegisterCommandExecute);
            LoginCommand = new Command(LoginCommandExecute);
        }

        private async void LoginCommandExecute(object obj)
        {
            await NavigationService.NavigateAsync("LoginPage");
        }

        private async void RegisterCommandExecute(object obj)
        {
            await NavigationService.NavigateAsync("RegisterIdentificationPage");
        }

        public async void OnAppearing()
        {
            try
            {
                using (UserDialogs.Instance.Loading("Cargando datos..."))
                {
                    await App.ApiService.GetIdentificationTypes()
                       .ContinueWith(result =>
                       {
                           if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                           {
                               App.IdentificationTypes = result.Result.ToList();
                           }
                           else if (result.IsFaulted) { }
                           else if (result.IsCanceled) { }
                       }, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        public void OnDisappearing(){ }

        public ICommand RegisterCommand { get; set; }

        public ICommand LoginCommand { get; set; }

        
    }
}
