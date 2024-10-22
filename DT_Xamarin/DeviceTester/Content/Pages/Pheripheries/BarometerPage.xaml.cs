using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Syncfusion.SfChart.XForms;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class BarometerPage : ContentPage, IPageWithNotifier
    {
        public ObservableCollection<SensorSpeed> coll = new ObservableCollection<SensorSpeed>(Enum.GetValues(typeof(SensorSpeed)) as IEnumerable<SensorSpeed>);
        private Animation _animation;

        public ObservableCollection<DataItem> Values { get; set; }

        private decimal _MinValue;
        private decimal _MaxValue;
        private decimal _CurrentValue;
        public decimal MaxValue { get =>_MaxValue; set {
                _MaxValue = value;
                OnPropertyChanged(nameof(MaxValue));
            }}
        public decimal MinValue
        {
            get => _MinValue; set
            {
                _MinValue = value;
                OnPropertyChanged(nameof(MinValue));
            }
        }
        public decimal CurrentValue
        {
            get => _CurrentValue; set
            {
                _CurrentValue = value;
                OnPropertyChanged(nameof(CurrentValue));
            }
        }


        public BarometerPage()
        {
            InitializeComponent();
            Values = new ObservableCollection<DataItem>();
            LineSeries.ItemsSource = Values;

            MaxValue = 0;
            MinValue = 0;
            CurrentValue = 0;

            NavigationPage.SetHasBackButton(this, false);
            var tmpComp = new ViewTittleLabel("Barometer", Constants.Barometer, this);

            var tempTuple = Constants.Functions.Find(x => x.Item1.GetType() == typeof(BarometerPageFactory));

            tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            BackButton.LinearGradientBrush.GradientStops[0].Color = tempTuple.Item2;
            BackButton.LinearGradientBrush.GradientStops[1].Color = tempTuple.Item3;

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
            AddChartEntry((decimal)data.PressureInHectopascals);
        }

        private void AddChartEntry(decimal pressureInHectopascals)
        {
            Values.Add(new DataItem()
            {
                DateTime = DateTime.Now.ToString("HH:mm"),
                Value = pressureInHectopascals
            });
            if (Values.Count > 25)
                Values.RemoveAt(0);
            if (Values.Count == 1)
            {
                MaxValue = pressureInHectopascals;
                MinValue = pressureInHectopascals;
            }
            MinValue = MinValue < pressureInHectopascals ? MinValue : pressureInHectopascals;
            MaxValue = MaxValue > pressureInHectopascals ? MaxValue : pressureInHectopascals;
            CurrentValue = pressureInHectopascals;
        }

        void SpeedPicker_SelectedIndexChanged(Object sender, EventArgs e)
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

    public class DataItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private String _DateTime;
        private decimal _Value;

        public String DateTime
        {
            get => _DateTime;
            set
            {
                this._DateTime = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateTime"));
            }
        }
        public decimal Value
        {
            get => _Value;
            set
            {
                this._Value = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }
    }
}
