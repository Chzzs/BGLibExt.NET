# BGLibExt

A higher abstraction level .NET library for Silicon Labs/Bluegiga BLE112 modules.

This library is based on work by [jedinja/monomyo](https://github.com/jedinja/monomyo) and [jrowberg/bglib](https://github.com/jrowberg/bglib). The BGLib was left as it was and the higher level abstraction code from [jedinja/monomyo](https://github.com/jedinja/monomyo) was refactored to be used as a library and some BLE advertisement related features have been added. So thanks goes out to them since they did most of the work.

## Installation

Install nuget package [BGLibExt](https://www.nuget.org/packages/BGLibExt/)

## Usage

### Discover devices

```c#
var ble = new BleConnector("COM1");
ble.ScanResponse += OnDiscoverDevices;
ble.StartDeviceDiscovery();
Task.Delay(5000).Wait();
ble.StopDeviceDiscovery();
ble.ScanResponse -= OnDiscoverDevices;

private void OnDiscoverDevices(object sender, BleScanResponseReceivedEventArgs args)
{
}
```

### Connect to device

```c#
var ble = new BleConnector("COM1");
var peripheralMap = ble.Connect(address, addressType);
```

### Disconnect from device

```c#
var ble = new BleConnector("COM1");
var peripheralMap = ble.Connect(address, addressType);
ble.Disconnect();
```

### Read characteristic

```c#
var ble = new BleConnector("COM1");
var peripheralMap = ble.Connect(address, addressType);
var data = ble.ReadCharacteristic(characteristicId, false);
```

### Write characteristic

```c#
var ble = new BleConnector("COM1");
var peripheralMap = ble.Connect(address, addressType);
ble.WriteCharacteristic(characteristicId, false);
```

### Characteristic notifications

```c#
var ble = new BleConnector("COM1");
var peripheralMap = ble.Connect(address, addressType);
// Validate peripheralMap
// Get uuid of characteristic for enabling notifications
ble.WriteClientCharacteristicConfiguration(uuid, BleCCCValue.NotificationsEnabled);
ble.CharacteristicValueChanged += OnValueChanged;

private void OnValueChanged(object sender, BleCharacteristicValueChangedEventArgs e)
{
}
```
