using Prism;
using Prism.Ioc;
using BNV.ViewModels;
using BNV.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BNV.Views.Register;
using BNV.Views.GraphicAndDetails;

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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQ1MjAyQDMxMzgyZTMxMmUzME9sUGkxWDNsSE8rZG1qWjBlUDA4ZUI0UmZOTWd1ajgvZEcwNUViR3gyeE09");
            InitializeComponent();
          
            await NavigationService.NavigateAsync("NavigationPage/WelcomePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage, NavigationViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterViewModel>();
            containerRegistry.RegisterForNavigation<RegisterResultPage, RegisterResultViewModel>();
            containerRegistry.RegisterForNavigation<PasswordRecoveryPage, PasswordRecoveryViewModel>();
            containerRegistry.RegisterForNavigation<PasswordRecoveryResultPage, PasswordRecoveryResultViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordViewModel>();
            containerRegistry.RegisterForNavigation<DetailsPage, DetailsViewModel>();
            containerRegistry.RegisterForNavigation<InformationPage, InformationViewModel>();
            containerRegistry.RegisterForNavigation<GraphicPage, GraphicViewModel>();
            containerRegistry.RegisterForNavigation<HomeDetailPage, HomeDetailViewModel>();
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomeViewModel>();
            containerRegistry.RegisterForNavigation<RegisterIdentificationPage, RegisterIdentificationViewModel>();
            containerRegistry.RegisterForNavigation<PasswordSettingPage, PasswordSettingViewModel>();
            containerRegistry.RegisterForNavigation<PasswordSettingResultPage, PasswordSettingResultViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordResultPage, ChangePasswordResultViewModel>();
        }
    }
   
}
