using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            var tmpComp = new ViewTittleLabel("Bluetooth", Constants.Bluetooth, this);
            var tempTuple = Constants.Functions.Find(x => x.Item1.GetType() == typeof(BluetoothPageFactory));
            tmpComp.LineraGradientBck.GradientStops[0].Color = tempTuple.Item2;
            tmpComp.LineraGradientBck.GradientStops[1].Color = tempTuple.Item3;
            this.MainGrid.Children.Add(tmpComp, 0, 0);


            this.ble = CrossBluetoothLE.Current;
            this.adapter = CrossBluetoothLE.Current.Adapter;
            this.DeviceListView.ItemsSource = models;

            AddBluetoothEventHandlers();
            if (this.ble.IsOn)
            {
                DeviceListView.BeginRefresh();
                DeviceListView.IsEnabled = true;
                DeviceListView.IsPullToRefreshEnabled = true;
            }
        }
        private void AddBluetoothEventHandlers()
        {
            ble.StateChanged += (sender, args) =>
            {
                if (args.NewState == BluetoothState.On)
                {
                    DeviceListView.IsEnabled = true;
                    DeviceListView.IsPullToRefreshEnabled = true;
                }
                else if (args.OldState == BluetoothState.On)
                {
                    models.Clear();
                    DeviceListView.IsEnabled = false;
                    DeviceListView.IsPullToRefreshEnabled = false;
                }
            };
            DeviceListView.RefreshCommand = new Command(() => {
                ScanForDevicesAsync();
            });
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
                if (!models.Select(x => x.DeviceId).Contains(tempModel.DeviceId))
                    models.Add(tempModel);
                else
                {
                    var tempID = models.IndexOf(models.First<BluetoothDeviceModel>(x => x.DeviceId == tempModel.DeviceId));
                    models.RemoveAt(tempID);
                    models.Insert(tempID, tempModel);
                }
            };
            DeviceListView.ItemTapped += (object sender, ItemTappedEventArgs e) => {
                if (e.Item == null) return;
                Task.Delay(500);
                if (sender is ListView lv) lv.SelectedItem = null;
            };
        }

        private async void ScanForDevicesAsync()
        {
            if (adapter.IsScanning == false)
            {
                RefreshLabel.IsVisible = false;
                await adapter.StartScanningForDevicesAsync();
                DeviceListView.IsRefreshing = false;
                RefreshLabel.IsVisible = true;
            }
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
            _animation.Commit(this, "Bluetooth Animation", 16, 1000000, Easing.BounceIn, null, null);
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
