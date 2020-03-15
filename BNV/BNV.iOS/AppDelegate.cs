using Foundation;
using Prism;
using Prism.Ioc;
using Syncfusion.SfRangeSlider.XForms.iOS;
using UIKit;
using Xamarin.Forms;

namespace BNV.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjE1NTIwQDMxMzcyZTM0MmUzMGdHam4wVGZ5bDRUTnNwQmJRdGhsUFluMFpjeDFtcnI0UGtVRjBsZXArYWc9");
            _ = new Syncfusion.XForms.iOS.ComboBox.SfComboBoxRenderer();
            _ = new SfRangeSliderRenderer();
            Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
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
