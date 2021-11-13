using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DeviceTester.Content.Views;
using DeviceTester.Interfaces;
using DeviceTester.Models;
using DeviceTester.Resources;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace DeviceTester.Content.Pages.Pheripheries
{
    public partial class BluetoothPage : ContentPage,IPageWithNotifier
    {
        private Animation _animation;
        ObservableCollection<BluetoothDeviceModel> models = new ObservableCollection<BluetoothDeviceModel>();

        IBluetoothLE ble;
        IAdapter adapter;


        public BluetoothPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);
            var tmpComp = new ViewTittleLabel("Bluetooth", Constants.LoremTemp, this);
            var tempTuple = Constants.Pheriphery.Find(x => x.Item1.GetType() == typeof(BarometerPageFactory));
            tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            this.MainGrid.Children.Add(tmpComp, 0, 0);


            this.ble = CrossBluetoothLE.Current;
            this.adapter = CrossBluetoothLE.Current.Adapter;
            //this.DeviceListView.ItemsSource = models;

            AddBluetoothEventHandlers();
        }

        private void AddBluetoothEventHandlers()
        {
            ble.StateChanged += (sender, args) =>
            {
                if (args.NewState == BluetoothState.On)
                {
                    DeviceListView.IsEnabled = true;
                    DeviceListView.IsPullToRefreshEnabled = true;
                    DeviceListView.RefreshCommand = new Command(() => {
                        DeviceListView.IsRefreshing = true;
                        ScanForDevicesAsync();
                        DeviceListView.IsRefreshing = false;
                        });
                }
                else if (args.OldState == BluetoothState.On)
                {
                    models.Clear();
                    DeviceListView.IsEnabled = false;
                    DeviceListView.IsPullToRefreshEnabled = false;
                    DeviceListView.RefreshCommand = null;
                }
            };
            adapter.DeviceDiscovered += (sender, args) =>
            {
                var tempDevice = args.Device;
                var tempModel = new BluetoothDeviceModel
                {
                    DeviceName = tempDevice.Name,
                    DeviceId = tempDevice.Id,
                    RSSI = tempDevice.Rssi,
                    State = tempDevice.State
                };
                models.Add(tempModel);
            };
        }

        private async void ScanForDevicesAsync()
        {
            if (adapter.IsScanning == false)
                await adapter.StartScanningForDevicesAsync();

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
            _animation.Commit(this, "GPS Animation", 16, 1000000, Easing.BounceIn, null, null);
        }

        void DeviceListView_Refreshing(System.Object sender, System.EventArgs e)
        {
            
        }
    }




    public class BluetoothPageFactory : PageFactory
    {
        public override string getPageName() => "Bluetooth";

        public override Page getPageObject()
        {
            return new BluetoothPage();
        }
    }
}
