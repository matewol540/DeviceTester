using System;
using System.Collections.Generic;
using DeviceTester.Helper;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace DeviceTester.Content.Views
{
    public partial class Camera_Settings : CarouselPage
    {
        public CameraSettings cs { get; set; }
        public Camera_Settings(ref CameraSettings cs)
        {
            InitializeComponent();
            On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.Automatic);
            this.cs = cs;
            foreach (var enumType in Enum.GetValues(typeof(FocusTypes)) as IEnumerable<FocusTypes>)
                Focus.Children.Add(new Plugin.Segmented.Control.SegmentedControlOption() { Text = enumType.ToString() });
            foreach (var enumType in Enum.GetValues(typeof(Exposureypes)) as IEnumerable<Exposureypes>)
                Exposure.Children.Add(new Plugin.Segmented.Control.SegmentedControlOption() { Text = enumType.ToString() });
            foreach (var enumType in Enum.GetValues(typeof(WhiteTypes)) as IEnumerable<WhiteTypes>)
                WhiteBalance.Children.Add(new Plugin.Segmented.Control.SegmentedControlOption() { Text = enumType.ToString() });


        }
    }
}
