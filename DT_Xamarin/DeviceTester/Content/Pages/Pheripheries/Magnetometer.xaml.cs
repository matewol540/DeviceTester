using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class MagnetometerPage : ContentPage, IPageWithNotifier
    {
        private Animation _animation;
        public ObservableCollection<SensorSpeed> coll = new ObservableCollection<SensorSpeed>(Enum.GetValues(typeof(SensorSpeed)) as IEnumerable<SensorSpeed>);

        public MagnetometerPage()
        {
            InitializeComponent();
            var tmpComp = new ViewTittleLabel("Magnetometer", Constants.Magnetometer, this);

            var tempTuple = Constants.Functions.Find(x => x.Item1.GetType() == typeof(MagnetometerPageFactory));

            tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            BackButton.LinearGradientBrush.GradientStops[0].Color = tempTuple.Item2;
            BackButton.LinearGradientBrush.GradientStops[1].Color = tempTuple.Item3;

            SpeedPicker.ItemsSource = coll;
            SpeedPicker.SelectedIndex = 0;
            this.MainGrid.Children.Add(tmpComp, 0, 0);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _ = EnableMegnetometerAsync(true);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (Magnetometer.IsMonitoring)
                _ = EnableMegnetometerAsync(false);
        }

        private async Task EnableMegnetometerAsync(Boolean state)
        {
            try
            {
                if (!state)
                {
                    Magnetometer.ReadingChanged -= Magnetometer_ReadingChanged;
                    Magnetometer.Stop();
                    return;
                }
                Magnetometer.ReadingChanged += Magnetometer_ReadingChanged;
                SensorSpeed sensor = (SensorSpeed)(SpeedPicker as Picker).SelectedItem;

                Magnetometer.Start(sensor);
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Feature not supported", "Sorry, but your device does not support selected feature", "Return");
                await Navigation.PopModalAsync();
            }
        }

        private void Magnetometer_ReadingChanged(object sender, MagnetometerChangedEventArgs e)
        {
            var tempText = $"X: {e.Reading.MagneticField.X.ToString("000.0000")} \nY: {e.Reading.MagneticField.Y.ToString("000.0000")} \nZ: {e.Reading.MagneticField.Z.ToString("000.0000")}";
            this.Value.Text = tempText;
        }

        public void ChangeDescriptionState(bool State)
        {
            GridLength HeightValue = new GridLength(50, GridUnitType.Absolute);
            switch (State)
            {
                case true:
                    HeightValue = new GridLength(50, GridUnitType.Absolute);
                    break;
                case false:
                    HeightValue = new GridLength(230, GridUnitType.Absolute);
                    break;
            }
            _animation = new Animation(
                        (d) => MainGrid.RowDefinitions[0] = new RowDefinition() { Height = HeightValue });
            _animation.Commit(this, "Magnetometer Animation", 16, 1000000, Easing.BounceIn, null, null);
        }

        void SpeedPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            if (!Accelerometer.IsMonitoring)
                return;
            Accelerometer.Stop();
            SensorSpeed sensor = (SensorSpeed)(sender as Picker).SelectedItem;
            Accelerometer.Start(sensor);
        }
    }
    public class MagnetometerPageFactory : PageFactory
    {
        public override string getPageName() => "Magnetometer";
        public override Page getPageObject()
        {
            return new MagnetometerPage();
        }
    }
}
