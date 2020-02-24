using Prism;
using Prism.Ioc;
using BNV.ViewModels;
using BNV.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BNV
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("@31372e342e30Q8P22CzKWAnZgQLTongsjhinlzd3TMpQjzSa83dxaSI=");
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterViewModel>();
            containerRegistry.RegisterForNavigation<RegisterResultPage, RegisterResultViewModel>();
            containerRegistry.RegisterForNavigation<PasswordRecoveryPage, PasswordRecoveryViewModel>();
            containerRegistry.RegisterForNavigation<PasswordRecoveryResultPage, PasswordRecoveryResultViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>();
            containerRegistry.RegisterForNavigation<SettingPage, SettingViewModel>();
            containerRegistry.RegisterForNavigation<StatisticPage, StatisticViewModel>();
            containerRegistry.RegisterForNavigation<ReportPage, ReportViewModel>();
            containerRegistry.RegisterForNavigation<SharesOfStockPage, SharesOfStockViewModel>();
            containerRegistry.RegisterForNavigation<ChangeTypePage, ChangeTypeViewModel>();
            containerRegistry.RegisterForNavigation<BonoPage, BonoViewModel>();
        }
    }
   
}
