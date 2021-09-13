using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class GPSPage : ContentPage
    {

        public IEnumerable<MapType> mapTypes { get {
                return Enum.GetValues(typeof(MapType)) as IEnumerable<MapType>;
            }
        }

        public GPSPage()
        {
            InitializeComponent();
            var tmpComp = new MyLabelView("GPS");
            MainGrid.Children.Add(tmpComp, 0, 0);
            _ = SetupUserLocationAsync();
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
