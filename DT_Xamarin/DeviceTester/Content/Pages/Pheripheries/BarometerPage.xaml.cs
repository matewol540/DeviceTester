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
    public partial class BarometerPage : ContentPage
    {
        double LowestAllowedValue = 870;
        double MaximumAllowedValue = 1051;
        public ObservableCollection<SensorSpeed> coll = new ObservableCollection<SensorSpeed>(Enum.GetValues(typeof(SensorSpeed)) as IEnumerable<SensorSpeed>);


        public BarometerPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            var tmpComp = new ViewTittleLabel("Barometer", Constants.LoremTemp, this);
            this.MainGrid.Children.Add(tmpComp, 0, 0);
            SpeedPicker.ItemsSource = coll;
            SpeedPicker.SelectedIndex = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async Task EnableAccelerometerAsync(Boolean state)
        {
            try
            {
                if (!state)
                {
                    Barometer.ReadingChanged -= Barometer_ReadingChanged;
                    Accelerometer.Stop();
                    return;
                }
                Barometer.ReadingChanged += Barometer_ReadingChanged;
                SensorSpeed sensor = (SensorSpeed)(SpeedPicker as Picker).SelectedItem;

                Accelerometer.Start(sensor);
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Feature not supported", "Sorry, but your device does not support selected feature", "Return");
                await Navigation.PopModalAsync();
            }
        }
        void Barometer_ReadingChanged(object sender, BarometerChangedEventArgs e)
        {
            var data = e.Reading;
            // Process Pressure
            Console.WriteLine($"Reading: Pressure: {data.PressureInHectopascals} hectopascals");
        }
        void SpeedPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            if (!Barometer.IsMonitoring)
                return;
            Barometer.Stop();
            SensorSpeed sensor = (SensorSpeed)(sender as Picker).SelectedItem;
            Barometer.Start(sensor);
        }

    }
    public class BarometerPageFactory : PageFactory
    {
        public override Page getPageObject()
        {
            return new BarometerPage();
        }
    }
}
