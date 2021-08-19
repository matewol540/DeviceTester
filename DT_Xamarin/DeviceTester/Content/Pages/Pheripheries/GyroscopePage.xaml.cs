using System;
using System.Collections.Generic;
using DeviceTester.Content.Views;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Numerics;
using System.Collections.ObjectModel;
using DeviceTester.Interfaces;

namespace DeviceTester.Content.Pages
{
    public partial class GyroscopePage : ContentPage
    {
        public Vector3 data;
        public ObservableCollection<SensorSpeed> coll = new ObservableCollection<SensorSpeed>(Enum.GetValues(typeof(SensorSpeed)) as IEnumerable<SensorSpeed>);
        public GyroscopePage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            var tmpComp = new MyLabelView("Gyroscope");
            SpeedPicker.ItemsSource = coll;
            SpeedPicker.SelectedIndex = 0;
            Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
            EnableGyroscopeAsync(false);
            this.MainGrid.Children.Add(tmpComp, 0, 0);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            gyroView.Initialize();
            EnableGyroscopeAsync(true);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            EnableGyroscopeAsync(false);
        }

        private async Task EnableGyroscopeAsync(Boolean state)
        {
            try
            {
                if (!state)
                {
                    Gyroscope.Stop();
                    return;
                }
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

            if (coordX_max.Text == String.Empty || Double.Parse(coordX_max.Text.Split(':')[1]) < data.X)
                coordX_max.Text = String.Format("Max X: {0:0.00}", data.X);
            if (coordY_max.Text == String.Empty || Double.Parse(coordY_max.Text.Split(':')[1]) < data.Y)
                coordY_max.Text = String.Format("Max y: {0:0.00}", data.Y);
            if (coordZ_max.Text == String.Empty || Double.Parse(coordZ_max.Text.Split(':')[1]) < data.Z)
                coordZ_max.Text = String.Format("Max z: {0:0.00}", data.Z);

            if (coordX_min.Text == String.Empty || Double.Parse(coordX_min.Text.Split(':')[1]) > data.X)
                coordX_min.Text = String.Format("Min X: {0:0.00}", data.X);
            if (coordY_min.Text == String.Empty || Double.Parse(coordY_min.Text.Split(':')[1]) > data.Y)
                coordY_min.Text = String.Format("Min y: {0:0.00}", data.Y);
            if (coordZ_min.Text == String.Empty || Double.Parse(coordZ_min.Text.Split(':')[1]) > data.Z)
                coordZ_min.Text = String.Format("Min z: {0:0.00}", data.Z);

            gyroView.MovePointer(data.X, data.Y, data.Z);
        }

        void SpeedPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            Gyroscope.Stop();
            SensorSpeed sensor = (SensorSpeed)(sender as Picker).SelectedItem;
            Gyroscope.Start(sensor);
        }
    }

    public class GyroscopePageFactory : PageFactory
    {
        public override Page getPageObject()
        {
            return new GyroscopePage();
        }
    }
}
