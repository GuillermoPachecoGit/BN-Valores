using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace BNV.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        public SettingViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            Title = "Register Page";
            TypeChange = "5 colones";
            Bonos = "5 colones";

            ChangePasswordCommand = new Command(async () => await ChangePasswordActionExecute());
            CloseSessionCommand = new Command(async () => await CloseSessionActionExecute());
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


        private async Task ChangePasswordActionExecute()
        {
            await NavigationService.NavigateAsync("ChangePasswordPage", null, false, false);
        }

        private async Task CloseSessionActionExecute()
        {
            await NavigationService.GoBackToRootAsync();
        }

        public ICommand ChangePasswordCommand { get; set; }

        public ICommand CloseSessionCommand { get; set; }
    }
}
