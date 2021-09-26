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


namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class GPSPage : ContentPage, IPageWithNotifier
    {
        private Animation _animation;
        public IEnumerable<MapType> mapTypes { get {
                return Enum.GetValues(typeof(MapType)) as IEnumerable<MapType>;
            }
        }
        public GPSPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);


            var tmpComp = new ViewTittleLabel("GPS", Constants.LoremTemp,this);
            this.MainGrid.Children.Add(tmpComp, 0, 0);
            _ = SetupUserLocationAsync();

            if (MapTypePicker.Items.Count != 0)
                MapTypePicker.SelectedIndex = 0;
        }

        public async Task SetupUserLocationAsync()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    MainMap.MoveToRegion(new MapSpan(GPSHelper.getPositionOnLocation(location), 0.1,0.1));
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Feature not supported", "Selected feature is not supported by your device","Return");
                await Navigation.PopModalAsync();
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                await DisplayAlert("Unknown problem", "Uknown problem has occured during loading selected option", "Return");
                await Navigation.PopModalAsync();
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
        public static Position getPositionOnLocation(Location location) => new Position(location.Latitude, location.Longitude);
    }

    public class GPSPageFactory : PageFactory
    {
        public override Page getPageObject()
        {
            return new GPSPage();
        }
    }
}
