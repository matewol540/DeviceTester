using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Microcharts;
using SkiaSharp;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class BarometerPage : ContentPage, IPageWithNotifier
    {
        public ObservableCollection<SensorSpeed> coll = new ObservableCollection<SensorSpeed>(Enum.GetValues(typeof(SensorSpeed)) as IEnumerable<SensorSpeed>);
        private Animation _animation;

        public ObservableCollection<ChartEntry> Values { get; set; }

        private readonly ChartEntry[] entries = new[]
        {
            new ChartEntry(100)
            {
                Label="T100",
                ValueLabel="100",
                Color = SKColors.AliceBlue
            },
            new ChartEntry(150)
            {
                Label="T150",
                ValueLabel="150",
                Color = SKColors.IndianRed
            },
            new ChartEntry(200)
            {
                Label="T200",
                ValueLabel="200",
                Color = SKColors.AliceBlue
            },
            new ChartEntry(250)
            {
                Label="T250",
                ValueLabel="250",
                Color = SKColors.IndianRed
            }

        };



        private BarChart _barChart;
        public BarChart barChart { get => _barChart;
            set
            {
                _barChart = value;
                OnPropertyChanged(nameof(barChart));
            } }
        public BarometerPage()
        {
            InitializeComponent();
            Values = new ObservableCollection<ChartEntry>();

            barChart = new BarChart();
            barChart.Entries = Values;
            barChart.MaxValue = 1086;
            barChart.MinValue = 870;
            barChart.PointMode = PointMode.Circle;

            ChartDisplayer.Chart = barChart;

            NavigationPage.SetHasBackButton(this, false);
            var tmpComp = new ViewTittleLabel("Barometer", Constants.LoremTemp, this);
            this.MainGrid.Children.Add(tmpComp, 0, 0);
            SpeedPicker.ItemsSource = coll;
            SpeedPicker.SelectedIndex = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = EnableBarometerAsync(true);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (Barometer.IsMonitoring)
                _ = EnableBarometerAsync(false);
        }

        private async Task EnableBarometerAsync(Boolean state)
        {
            try
            {
                if (!state)
                {
                    Barometer.ReadingChanged -= Barometer_ReadingChanged;
                    Barometer.Stop();
                    return;
                }
                Barometer.ReadingChanged += Barometer_ReadingChanged;
                SensorSpeed sensor = (SensorSpeed)(SpeedPicker as Picker).SelectedItem;

                Barometer.Start(sensor);
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Feature not supported", "Sorry, but your device does not support selected feature", "Return");
                if (Debugger.IsAttached)
                    return;
                await Navigation.PopModalAsync();
            }
        }

        void Barometer_ReadingChanged(object sender, BarometerChangedEventArgs e)
        {
            var data = e.Reading;
            AddChartEntry((float)data.PressureInHectopascals);
        }

        private void AddChartEntry(float pressureInHectopascals)
        {
            var tempDataPoint = new ChartEntry(pressureInHectopascals);
            tempDataPoint.Color = SKColor.Parse("#8F00FF");
            tempDataPoint.Label = DateTime.Now.ToString("HH:mm");
            tempDataPoint.ValueLabel = pressureInHectopascals.ToString();
            Values.Add(tempDataPoint);
            if (Values.Count > 15)
                Values.RemoveAt(0);

            if (!barChart.IsAnimating)
                barChart.AnimateAsync(false);
        }

        void SpeedPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            if (!Barometer.IsMonitoring)
                return;
            Barometer.Stop();
            SensorSpeed sensor = (SensorSpeed)(sender as Picker).SelectedItem;
            Barometer.Start(sensor);
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
            _animation = new Xamarin.Forms.Animation(
                        (d) => MainGrid.RowDefinitions[0] = new RowDefinition() { Height = HeightValue });
            _animation.Commit(this, "Barometer Animation", 16, 1000000, Easing.BounceIn, null, null);
        }
    }
    public class BarometerPageFactory : PageFactory
    {
        public override string getPageName() => "Barometer";
        public override Page getPageObject()
        {
            return new BarometerPage();
        }
    }

}
