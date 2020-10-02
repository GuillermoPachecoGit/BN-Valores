using Prism;
using Prism.Ioc;
using BNV.ViewModels;
using BNV.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BNV.Views.Register;
using BNV.Views.GraphicAndDetails;
using BNV.ServicesWebAPI.Interfaces;
using System.Collections.ObjectModel;
using BNV.Models;
using BNV.ServicesWebAPI;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using BNV.Settings;
using System.Linq;
using Plugin.DeviceOrientation;
using Syncfusion.XForms.MaskedEdit;

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
        private Task<List<Currency>> _currenciesItems;
        private Task<List<Sector>> _sectorsitems;
        private Task<string> _contact;
        private Task<List<IdentificationType>> _identTypes;

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        public static ObservableCollection<Currency> Currencies { get; set; }
        public static ObservableCollection<Sector> Sectors { get; set; }
        public static Sector SelectedSector { get; set; }
        public static Currency SelectedCoin { get; set; }
        public static IAPIService ApiService { get; set; }
        public static List<IdentificationType> IdentificationTypes { get; set; }
        public static string ContactInfo { get; set; }
        public static bool Refresh { get; internal set; }
        public static string HomePage { get; internal set; }
        public static int BonosIndexNotify { get; internal set; }
        public static int ExchangesIndexNotify { get; internal set; }
        public static string DeviceIdFirebase { get; set; }
        public static bool WithAuthentication { get; internal set; }
        public static string Identification { get; internal set; }

        public static async void UpdateToken(string refreshedToken)
        {
            var userDevice = new UserDeviceParam(App.DeviceIdFirebase);
            var token = await SecureStorage.GetAsync(Config.Token);
            var authorization = $"Bearer {token}";

            if (token != null && token.Length > 0)
            {
                await App.ApiService.UserDevice(authorization, userDevice).ContinueWith(result =>
                {
                    if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                    {
                    }
                    else if (result.IsFaulted)
                    {
                    }
                    else if (result.IsCanceled)
                    { }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjk4NDA0QDMxMzgyZTMxMmUzMGp3UHJKcDc1MFFZcjNuRktVUVFqcEFodTlEaC9qYkVjdHBNUUNGRkFTeVU9");
            InitializeComponent();
            ApiService = NetworkService.GetApiService();
            await NavigationService.NavigateAsync("NavigationPage/WelcomePage");
        }

        protected async override void OnStart()
        {
            base.OnStart();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);
 
            try
            {
                var getContact = ApiService.GetContactInfo().ContinueWith(contactInfo => _contact = contactInfo);
                var getSectors = ApiService.GetSectors().ContinueWith(sectors => _sectorsitems = sectors);
                var getCurrencies = ApiService.GetCurrency().ContinueWith(currencies => _currenciesItems = currencies);
                await Task.WhenAll(getSectors, getCurrencies, getContact).ContinueWith(async result =>
                {
                    if (result.IsCompleted && result.Status == TaskStatus.RanToCompletion)
                    {
                        if (_contact.Result != null)
                            ContactInfo = _contact.Result.Replace("\"", string.Empty);

                        if (_currenciesItems.Result.Count > 0)
                        {
                            Currencies = new ObservableCollection<Currency>(_currenciesItems.Result);
                        }

                        if (_sectorsitems.Result.Count > 0)
                        {
                            Sectors = new ObservableCollection<Sector>(_sectorsitems.Result);
                        }
                    }
                    else if (result.IsFaulted){}
                    else if (result.IsCanceled) {}
                }, TaskScheduler.FromCurrentSynchronizationContext()).ConfigureAwait(false);
            }
            catch (Exception ex){}
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage, NavigationViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterViewModel>();
            containerRegistry.RegisterForNavigation<RegisterResultPage, RegisterResultViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordViewModel>();
            containerRegistry.RegisterForNavigation<HomeDetailPage, HomeDetailViewModel>();
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomeViewModel>();
            containerRegistry.RegisterForNavigation<RegisterIdentificationPage, RegisterIdentificationViewModel>();
            containerRegistry.RegisterForNavigation<PasswordSettingPage, PasswordSettingViewModel>();
            containerRegistry.RegisterForNavigation<PasswordSettingResultPage, PasswordSettingResultViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordResultPage, ChangePasswordResultViewModel>();
            containerRegistry.RegisterForNavigation<RegisterRegisteredResultPage, RegisterRegisteredResultViewModel>();
        }
    }
   
}
