using System;
using System.Collections.Generic;
using DeviceTester.Helper;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace DeviceTester.Content.Views
{
    public partial class Camera_Settings : ContentPage
    {
        public Camera_Settings(CameraSettings cs)
        {
            InitializeComponent();
            On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.FormSheet);
        }
    }
}
