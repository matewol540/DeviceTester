using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Resources;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class GPSPage : ContentPage, IPageWithNotifier
    {
        private Animation _animation;
        public IEnumerable<MapType> mapTypes { get {
                return Enum.GetValues(typeof(MapType)) as IEnumerable<MapType>;
            }
        }
        private String _Coords = "";
        public String Coords { get => _Coords; set
            {
                this._Coords = value;
                OnPropertyChanged(nameof(Coords));
            }
        }
        private Double _CompassHeading;
        public Double CompassHeading { get => _CompassHeading; set
            {
                this._CompassHeading = value;
                OnPropertyChanged(nameof(CompassHeading));
            }
        }


        public GPSPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);


            var tmpComp = new ViewTittleLabel("GPS", Constants.LoremTemp,this);

            var tempTuple = Constants.Pheriphery.Find(x => x.Item1.GetType() == typeof(GPSPageFactory));

            tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            BackButton.LinearGradientBrush.GradientStops[0].Color = tempTuple.Item2;
            BackButton.LinearGradientBrush.GradientStops[1].Color = tempTuple.Item3;

            this.MainGrid.Children.Add(tmpComp, 0, 0);
            Compass.ReadingChanged += Compass_ReadingChanged;

            if (MapTypePicker.Items.Count != 0)
                MapTypePicker.SelectedIndex = 0;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                if (!Compass.IsMonitoring)
                    Compass.Start(SensorSpeed.Default);
                _ = SetupUserLocationAsync();

            }
            catch (FeatureNotSupportedException fnsEx)
            {
            }
            catch (Exception ex)
            {
            }
        }

        protected override void OnDisappearing()
        {
            if (CrossGeolocator.Current.IsListening == true)
                CrossGeolocator.Current.StopListeningAsync();
            if (Compass.IsMonitoring)
                Compass.Stop();

            base.OnDisappearing();
        }
        void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            var data = e.Reading;
            CompassHeading = data.HeadingMagneticNorth;
        }

        public async Task SetupUserLocationAsync()
        {
            try
            {
                var Status = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
                switch (Status)
                {

                    case PermissionStatus.Denied:
                        await DisplayAlert("GPS Track cannot be used without proper permission", "Access denied", "Return");
                        await Navigation.PopModalAsync();
                        break;
                    case PermissionStatus.Unknown:
                        var result = await Permissions.RequestAsync<Permissions.LocationAlways>();
                        if (result == PermissionStatus.Denied)
                        {
                            await DisplayAlert("GPS Track cannot be used without proper permission", "Access denied", "Return");
                            await Navigation.PopModalAsync();
                        }
                        break;
                }

                var geolocationTask = await CrossGeolocator.Current.StartListeningAsync(new TimeSpan(10),15, false,new Plugin.Geolocator.Abstractions.ListenerSettings()
                {
                    ActivityType = Plugin.Geolocator.Abstractions.ActivityType.Other,
                    AllowBackgroundUpdates = true
                });
                CrossGeolocator.Current.PositionChanged += SetPositionOnMap;

               
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Feature not supported", "Selected feature is not supported by your device","Return");
                await Navigation.PopModalAsync();
            }
            catch (FeatureNotEnabledException fneEx)
            {
                await DisplayAlert("Feature not enabled", "Selected feature is not supported by your device", "Return");
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Unknown problem", "Uknown problem has occured during loading selected option", "Return");
                await Navigation.PopModalAsync();
            }
        }

        private void SetPositionOnMap(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {

            var dataResult = e.Position;
            if (dataResult != null)
            {
                MainMap.MoveToRegion(new MapSpan(GPSHelper.getPositionOnLocation(dataResult), 0.1, 0.1));
                if (MainMap.Pins.Count == 0)
                    MainMap.Pins.Add(new Pin() { Label = "Current_Posisition", });
                MainMap.Pins[0].Position = GPSHelper.getPositionOnLocation(dataResult);
                Coords = $"\nLatitude:{String.Format("{0:0.######}", dataResult.Latitude)}, Longitude:{String.Format( "{0:0.######}", dataResult.Longitude)}, Altitude:{String.Format("{0:0.######}", dataResult.Altitude)}";
            }
        }

        void MapTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainMap.MapType = (MapType)(sender as Picker).SelectedItem;
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
            Thread.Sleep(10);
        }
    }

    public static class GPSHelper {
        public static Position getPositionOnLocation(Plugin.Geolocator.Abstractions.Position location) => new Position(location.Latitude, location.Longitude);
    }

    public class GPSPageFactory : PageFactory
    {
        public override string getPageName() => "GPS";
        public override Page getPageObject()
        {
            return new GPSPage();
        }
    }
}
