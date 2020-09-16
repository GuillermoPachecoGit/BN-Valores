using System.Collections.Generic;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Util;
using Firebase;
using Firebase.Iid;
using Plugin.CurrentActivity;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;

namespace BNV.Droid
{
    [Activity(Label = "BNV", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;

        protected override void OnCreate(Bundle bundle)
        {
            CrossCurrentActivity.Current.Activity = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjk4NDA0QDMxMzgyZTMxMmUzMGp3UHJKcDc1MFFZcjNuRktVUVFqcEFodTlEaC9qYkVjdHBNUUNGRkFTeVU9");
            base.OnCreate(bundle);
            UserDialogs.Init(this);
            Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, bundle);
            InitFirebase();
            CreateNotificationChannel();
            LoadApplication(new App(new AndroidInitializer()));
            IsPlayServicesAvailable();
        }

        private async void InitFirebase()
        {
            // To Update
            FirebaseOptions options = new FirebaseOptions.Builder()
                         .SetApplicationId("1:361843519978:android:8b772f588decc346038293")
                         .SetApiKey("AAAAVD-N8eo:APA91bEY3fjpUIhJYzR-Fl9w66klT-HNltLNfvVgTcxn-DMw9UKpNXylRcz-f6mIEjGQfbwH5lViaKSFSid6IytKmo_nu2rSTXyiuDXuTJa9sovgsYliTydtgQ8ILe1Zu-mTtUCkTWx1")
                         .SetGcmSenderId("361843519978")
                         .Build();

            bool hasBeenInitialized = false;
            IList<FirebaseApp> firebaseApps = FirebaseApp.GetApps(this);
            foreach (FirebaseApp app in firebaseApps)
            {
                if (app.Name.Equals(FirebaseApp.DefaultAppName))
                {
                    hasBeenInitialized = true;
                    FirebaseApp firebaseApp = app;
                }
            }

            if (!hasBeenInitialized)
            {
                FirebaseApp firebaseApp = FirebaseApp.InitializeApp(this, options);
            }

            Log.Debug(nameof(MainActivity), "Refreshed token: " + FirebaseInstanceId.Instance.Token);
            App.DeviceIdFirebase = FirebaseInstanceId.Instance.Token;
        }

        public void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID,
                                                  "FCM Notifications",
                                                  NotificationImportance.Default)
            {

                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {

                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    //msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }

                else
                {
                    // msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                // do whatever if play service is not available
                //msgText.Text = "Google Play Services is available.";
                return true;
            }
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

