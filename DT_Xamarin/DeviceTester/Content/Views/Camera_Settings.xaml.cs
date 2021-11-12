using System;
using System.Collections.Generic;
using DeviceTester.Helper;
using DeviceTester.Interfaces;
using Plugin.Segmented.Event;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Slider = Xamarin.Forms.Slider;

namespace DeviceTester.Content.Views
{
    public partial class Camera_Settings : ContentPage
    {

        public FocusTypes focusTypes { get => ((FocusTypes)Focus.SelectedSegment); }
        public Slider GetSlider { get => FocusValue; }
        public double FocusValueGet { get => FocusValue.Value; set => FocusValue.Value = value; }


        public Camera_Settings()
        {
            InitializeComponent();
            On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.Automatic);
            foreach (var enumType in Enum.GetValues(typeof(FocusTypes)) as IEnumerable<FocusTypes>)
                Focus.Children.Add(new Plugin.Segmented.Control.SegmentedControlOption() { Text = enumType.ToString() });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Focus.OnSegmentSelected += FocusMode_ValueChanged;
        }

        void FocusMode_ValueChanged(Object sender, SegmentSelectEventArgs e){
            switch (focusTypes)
            {
                case FocusTypes.Auto:
                    FocusValue.IsEnabled = false;
                    break;
                case FocusTypes.Locked:
                    FocusValue.IsEnabled = true;
                    break;
                default:
                    Focus.IsEnabled = true;
                    break;
            }
            DependencyService.Get<ICameraService>().FocusMode_ValueChanged();
        }
        void FocusValue_ValueChanged(Object sender, SegmentSelectEventArgs e) => DependencyService.Get<ICameraService>().FocusValue_ValueChanged();
    }
}
