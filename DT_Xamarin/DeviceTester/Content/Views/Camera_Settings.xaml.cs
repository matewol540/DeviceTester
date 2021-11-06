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
    public partial class Camera_Settings : CarouselPage
    {

        public FocusTypes focusTypes { get => ((FocusTypes)Focus.SelectedSegment); }
        public Slider GetSlider { get => FocusValue; }
        public double FocusValueGet { get => FocusValue.Value; set => FocusValue.Value = value; }
        public Exposureypes exposureMode { get => ((Exposureypes)Exposure.SelectedSegment); }
        //public double OffsetValue { get => Offset.Value; }
        public double DurationValue { get => Duration.Value; }
        public double ISOValue { get => ISO.Value; }
        public double BiasValue { get => Bias.Value; }
        public WhiteTypes WhiteBalanceMode { get => (WhiteTypes)WhiteBalance.SelectedSegment; }
        public double TempValue { get => Temp.Value; }
        public double TintValue { get => Tint.Value; }


        public Camera_Settings()
        {
            InitializeComponent();
            On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.Automatic);
            foreach (var enumType in Enum.GetValues(typeof(FocusTypes)) as IEnumerable<FocusTypes>)
                Focus.Children.Add(new Plugin.Segmented.Control.SegmentedControlOption() { Text = enumType.ToString() });
            foreach (var enumType in Enum.GetValues(typeof(Exposureypes)) as IEnumerable<Exposureypes>)
                Exposure.Children.Add(new Plugin.Segmented.Control.SegmentedControlOption() { Text = enumType.ToString() });
            foreach (var enumType in Enum.GetValues(typeof(WhiteTypes)) as IEnumerable<WhiteTypes>)
                WhiteBalance.Children.Add(new Plugin.Segmented.Control.SegmentedControlOption() { Text = enumType.ToString() });
            

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Focus.OnSegmentSelected += FocusMode_ValueChanged;
            Exposure.OnSegmentSelected += ExposureMode_ValueChanged;

            var Parent = DependencyService.Get<ICameraService>();

            Duration.Maximum = Parent.DurationMaxValue;
            Duration.Minimum = Parent.DurationMinValue;
            ISO.Maximum = Parent.ISOMaxValue;
            ISO.Minimum = Parent.ISOMinValue;
            Bias.Maximum = Parent.BiasMaxValue;
            Bias.Minimum = Parent.BiasMinValue;

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
        void ExposureMode_ValueChanged(Object sender, SegmentSelectEventArgs e)
        {
            switch (exposureMode)
            {
                case Exposureypes.Auto:
                    Duration.IsEnabled = false;
                    ISO.IsEnabled = false;
                    break;
                case Exposureypes.Locked:
                    Duration.IsEnabled = false;
                    ISO.IsEnabled = false;
                    break;
                case Exposureypes.Custom:
                    Duration.IsEnabled = true;
                    ISO.IsEnabled = true;
                    break;
                default:
                    break;
            }
            DependencyService.Get<ICameraService>().ExposureMode_ValueChanged();
        }
        void DurationValue_ValueChanged(Object sender, ValueChangedEventArgs e) => DependencyService.Get<ICameraService>().DurationValue_ValueChanged();
        void ISOValue_ValueChanged(Object sender, ValueChangedEventArgs e) => DependencyService.Get<ICameraService>().ISOValue_ValueChanged();
        void BiasValue_ValueChanged(Object sender,ValueChangedEventArgs e) => DependencyService.Get<ICameraService>().BiasValue_ValueChanged();
        void WhiteBalanceMode_ValueChanged(Object sender, ValueChangedEventArgs e) => DependencyService.Get<ICameraService>().WhiteBalanceMode_ValueChanged();
        void TempValue_ValueChanged(Object sender, ValueChangedEventArgs e) => DependencyService.Get<ICameraService>().TempValue_ValueChanged();
        void TintValue_ValueChanged(Object sender, ValueChangedEventArgs e) => DependencyService.Get<ICameraService>().TintValue_ValueChanged();

    }
}
