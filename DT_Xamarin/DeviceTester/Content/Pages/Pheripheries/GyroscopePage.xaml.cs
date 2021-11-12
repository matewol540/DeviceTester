using System;
using System.Collections.Generic;
using DeviceTester.Content.Views;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Numerics;
using System.Collections.ObjectModel;
using DeviceTester.Interfaces;
using DeviceTester.Resources;

namespace DeviceTester.Content.Pages
{
    public partial class GyroscopePage : ContentPage, IPageWithNotifier
    {
        public Vector3 data;
        private Animation _animation;
        public ObservableCollection<SensorSpeed> coll = new ObservableCollection<SensorSpeed>(Enum.GetValues(typeof(SensorSpeed)) as IEnumerable<SensorSpeed>);
        public GyroscopePage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            var tmpComp = new ViewTittleLabel("Gyroscope", Constants.LoremTemp,this);
            var tempTuple = Constants.Pheriphery.Find(x => x.Item1.GetType() == typeof(GyroscopePageFactory));

            tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            BackButton.LinearGradientBrush.GradientStops[0].Color = tempTuple.Item2;
            BackButton.LinearGradientBrush.GradientStops[1].Color = tempTuple.Item3;

            SpeedPicker.ItemsSource = coll;
            SpeedPicker.SelectedIndex = 0;
            this.MainGrid.Children.Add(tmpComp, 0, 0);
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
            _animation.Commit(this, "GPS Animation", 16, 1000000, Easing.BounceIn, null, null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            gyroView.Initialize();
            _ = EnableGyroscopeAsync(true);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (Gyroscope.IsMonitoring)
                _ = EnableGyroscopeAsync(false);
        }

        private async Task EnableGyroscopeAsync(Boolean state)
        {
            try
            {
                if (!state)
                {
                    Gyroscope.ReadingChanged -= Gyroscope_ReadingChanged;
                    Gyroscope.Stop();
                    return;
                }
                Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
                SensorSpeed sensor = (SensorSpeed)(SpeedPicker as Picker).SelectedItem;

                Gyroscope.Start(sensor);
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Feature not supported", "Sorry, but your device does not support selected feature","Return");
                await Navigation.PopModalAsync();
            }
        }

        private void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            data = e.Reading.AngularVelocity;
            if ((data.X > -0.02 && data.X < 0.02) || Track_X.IsChecked)
                data.X = 0.0F;
            if ((data.Y > -0.02 && data.Y < 0.02) || Track_Y.IsChecked)
                data.Y = 0.0F;
            if ((data.Z > -0.02 && data.Z < 0.02) || Track_Z.IsChecked)
                data.Z = 0.0F;

            coordX.Text = String.Format("x: {0:0.00}", data.X);
            coordY.Text = String.Format("y: {0:0.00}", data.Y);
            coordZ.Text = String.Format("z: {0:0.00}", data.Z);

            if (String.IsNullOrEmpty(coordX_max.Text) || Double.Parse(coordX_max.Text.Split(':')[1]) < data.X)
                coordX_max.Text = String.Format("Max X: {0:0.00}", data.X);
            if (String.IsNullOrEmpty(coordY_max.Text) || Double.Parse(coordY_max.Text.Split(':')[1]) < data.Y)
                coordY_max.Text = String.Format("Max y: {0:0.00}", data.Y);
            if (String.IsNullOrEmpty(coordZ_max.Text) || Double.Parse(coordZ_max.Text.Split(':')[1]) < data.Z)
                coordZ_max.Text = String.Format("Max z: {0:0.00}", data.Z);

            if (String.IsNullOrEmpty(coordX_min.Text) || Double.Parse(coordX_min.Text.Split(':')[1]) > data.X)
                coordX_min.Text = String.Format("Min X: {0:0.00}", data.X);
            if (String.IsNullOrEmpty(coordY_min.Text) || Double.Parse(coordY_min.Text.Split(':')[1]) > data.Y)
                coordY_min.Text = String.Format("Min y: {0:0.00}", data.Y);
            if (String.IsNullOrEmpty(coordZ_min.Text) || Double.Parse(coordZ_min.Text.Split(':')[1]) > data.Z)
                coordZ_min.Text = String.Format("Min z: {0:0.00}", data.Z);

            gyroView.MovePointer(data.X, data.Y, data.Z);
        }

        void SpeedPicker_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (!Gyroscope.IsMonitoring)
                return;
            Gyroscope.Stop();
            SensorSpeed sensor = (SensorSpeed)(sender as Picker).SelectedItem;
            Gyroscope.Start(sensor);
        }
    }

    public class GyroscopePageFactory : PageFactory
    {
        public override string getPageName() => "Gyroscope";
        public override Page getPageObject()
        {
            return new GyroscopePage();
        }
    }
}
