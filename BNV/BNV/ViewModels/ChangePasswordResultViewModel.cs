using System;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class ChangePasswordResultViewModel : ViewModelBase
    {
        public ChangePasswordResultViewModel(INavigationService navigationService)
            : base(navigationService) {

            Title = "Cambio de contraseña";
            ReturnLoginCommand = new Command(ReturnLoginCommandExecute);
           
        }

        private async void ReturnLoginCommandExecute()
        {
            await NavigationService.GoBackToRootAsync();
        }

        public ICommand ReturnLoginCommand { get; set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var title = parameters.GetValue<string>("title");
            Title = string.IsNullOrEmpty(title) ? title : "Cambio de contraseña";
        }
    }
}
