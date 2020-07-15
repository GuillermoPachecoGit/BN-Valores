using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.CurrentActivity;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;

namespace BNV.Droid
{
    [Activity(Label = "BNV", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            CrossCurrentActivity.Current.Activity = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjg3MTE1QDMxMzgyZTMxMmUzMExtNE8zcXRBa1lLa1ZkeU5ha0JEWXEwdDkyKzB4TVhrZDlXd0xaVlRxc3M9");
            base.OnCreate(bundle);
            UserDialogs.Init(this);
            Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

