using System;
using Plugin.BLE.Abstractions;
using Plugin.BluetoothLE;

namespace DeviceTester.Models
{
    public class BluetoothDeviceModel
    {
        private string _DeviceName;
        public string DeviceName { get => _DeviceName; set
            {
                if (value == null)
                    _DeviceName = "Unknown";
                else
                    _DeviceName = value;
                //OnPropertyChanged(nameof(DeviceName));
            }
        }
        public Guid DeviceId { get; set; }
        public double RSSI { get; set; }
        public DeviceState State { get; set; }
        public String Distance { get {
                if (RSSI > -63.08) return "<0.5m";
                if (RSSI < -63 && RSSI > -66.88) return "~1m";
                if (RSSI < -66.88 && RSSI > -76.23) return "~5m";
                return "> 5m";
            }
        }
    }
}

