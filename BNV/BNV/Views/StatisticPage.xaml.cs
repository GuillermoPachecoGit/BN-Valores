using System.Threading.Tasks;
using BNV.Settings;
using Plugin.DeviceOrientation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BNV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticPage : TabbedPage
    {
        public StatisticPage()
        {
           InitializeComponent();

            Task.Run(async () =>
           {
               var value = await SecureStorage.GetAsync(Config.MainPage);

               if (value != null && value == Config.MainPageTypes.Bonos)
               {
                   CurrentPage = Children[1];
               }
               else if (value != null && value == Config.MainPageTypes.Reportos)
               {
                   CurrentPage = Children[0];
               }
               else if (value != null && value == Config.MainPageTypes.TipoCambio)
               {
                   CurrentPage = Children[3];
               }
               else if (value != null && value == Config.MainPageTypes.Acciones)
               {
                   CurrentPage = Children[2];
               }
               else
                   CurrentPage = Children[0];
           });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (CrossDeviceOrientation.IsSupported) CrossDeviceOrientation.Current.UnlockOrientation();
        }
    }
}
