using System;
using Firebase.CloudMessaging;
using Foundation;
using Prism;
using Prism.Ioc;
using Syncfusion.SfRangeSlider.XForms.iOS;
using Syncfusion.XForms.iOS.MaskedEdit;
using Syncfusion.XForms.Pickers.iOS;
using UIKit;
using UserNotifications;
using Xamarin.Forms;

namespace BNV.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjk4NDA0QDMxMzgyZTMxMmUzMGp3UHJKcDc1MFFZcjNuRktVUVFqcEFodTlEaC9qYkVjdHBNUUNGRkFTeVU9");
            new SfRangeSliderRenderer();
            Syncfusion.XForms.iOS.TabView.SfTabViewRenderer.Init();
            new Syncfusion.XForms.iOS.ComboBox.SfComboBoxRenderer();
            Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            Firebase.Core.App.Configure();
            SfDatePickerRenderer.Init();
            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfChipRenderer.Init();

            UIApplication.SharedApplication.SetStatusBarHidden(true, UIStatusBarAnimation.None);
            UIApplication.SharedApplication.SetStatusBarHidden(false, UIStatusBarAnimation.None);

            LoadApplication(new App(new iOSInitializer()));

            // Register your app for remote notifications.
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // iOS 10 or later
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
                    Console.WriteLine(granted);
                });

                // For iOS 10 display notification (sent via APNS)
                //UNUserNotificationCenter.Current.Delegate = this;
                UNUserNotificationCenter.Current.Delegate = new MyNotificationCenterDelegate();

                // For iOS 10 data message (sent via FCM)
                Messaging.SharedInstance.RemoteMessageDelegate = this;

            }
            else
            {
                // iOS 9 or before
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }
            // Watch for notifications while the app is active

            App.DeviceIdFirebase = Messaging.SharedInstance.FcmToken;
            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            SfMaskedEditRenderer.Init();
            return base.FinishedLaunching(app, options);
        }

        public void DidRefreshRegistrationToken(Messaging messaging, string fcmToken)
        {
            System.Diagnostics.Debug.WriteLine($"FCM Token: {fcmToken}");
            App.DeviceIdFirebase = fcmToken;
            App.UpdateToken(fcmToken);
        }


        [Export("application:supportedInterfaceOrientationsForWindow:")]
        public UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, IntPtr forWindow)
        {
            return Plugin.DeviceOrientation.DeviceOrientationImplementation.SupportedInterfaceOrientations;
        }


        public override void OnActivated(UIApplication uiApplication)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                // If VS has updated to the latest version , you can use StatusBarManager , else use the first line code
                // UIView statusBar = new UIView(UIApplication.SharedApplication.StatusBarFrame);
                UIView statusBar = new UIView();
                statusBar.TranslatesAutoresizingMaskIntoConstraints = false;
                statusBar.BackgroundColor = UIColor.Black;
            }
            else
            {
                UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
                if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
                {
                    statusBar.BackgroundColor = UIColor.Black;
                    statusBar.TintColor = UIColor.White;
                    UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.BlackTranslucent;
                }
            }
            base.OnActivated(uiApplication);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
