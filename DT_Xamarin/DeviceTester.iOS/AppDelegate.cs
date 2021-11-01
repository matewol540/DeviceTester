using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Plugin.Segmented.Control.iOS;

namespace DeviceTester.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif
            global::Xamarin.Forms.Forms.Init();
            SegmentedControlRenderer.Initialize();
            Xamarin.FormsMaps.Init();
            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();
            LoadApplication(new App());
            app.StatusBarStyle = UIStatusBarStyle.DarkContent;
            return base.FinishedLaunching(app, options);
        }
    }
}
