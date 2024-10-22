using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace DeviceTester.Content.Views
{
    public partial class ResponseModal : ContentPage
    {
        public ResponseModal(Plugin.Fingerprint.Abstractions.FingerprintAuthenticationResult result)
        {
            InitializeComponent();
            On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.Automatic);
            switch (result.Status)
            {
                case Plugin.Fingerprint.Abstractions.FingerprintAuthenticationResultStatus.Succeeded:
                    this.Status.Source = ImageSource.FromResource("DeviceTester.Resources.Images.ApproveIcon.png");
                    break;
                default:
                    this.Status.Source = ImageSource.FromResource("DeviceTester.Resources.Images.DeniedIcon.png");
                    break;
            }
        }
    }
}
