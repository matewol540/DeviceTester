using System;
using Plugin.BLE.Abstractions;
using Plugin.BluetoothLE;

namespace DeviceTester.Models
{
    public class BluetoothDeviceModel
    {
        public string DeviceName { get; set; }
        public Guid DeviceId { get; set; }
        public int RSSI { get; set; }
        public DeviceState State { get; set; }
    }
}
