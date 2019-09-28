using Bluegiga.BLE.Events.GAP;

namespace BGLibExt
{
    public class BleDeviceDiscovery
    {
        public event ScanResponseReceivedEventHandler ScanResponse;

        public delegate void ScanResponseReceivedEventHandler(object sender, BleScanResponseReceivedEventArgs e);

        /// <summary>
        /// Start device discovery
        /// </summary>
        public void StartDeviceDiscovery()
        {
            BleModuleConnection.Instance.BleProtocol.Lib.BLEEventGAPScanResponse += OnScanResponse;

            BleModuleConnection.Instance.BleProtocol.Lib.SendCommand(BleModuleConnection.Instance.SerialPort, BleModuleConnection.Instance.BleProtocol.Lib.BLECommandGAPSetScanParameters(0xC8, 0xC8, 1));
            BleModuleConnection.Instance.BleProtocol.Lib.SendCommand(BleModuleConnection.Instance.SerialPort, BleModuleConnection.Instance.BleProtocol.Lib.BLECommandGAPDiscover(1));
        }

        /// <summary>
        /// Stop device discovery
        /// </summary>
        public void StopDeviceDiscovery()
        {
            BleModuleConnection.Instance.BleProtocol.Lib.SendCommand(BleModuleConnection.Instance.SerialPort, BleModuleConnection.Instance.BleProtocol.Lib.BLECommandGAPEndProcedure());

            BleModuleConnection.Instance.BleProtocol.Lib.BLEEventGAPScanResponse -= OnScanResponse;
        }

        private void OnScanResponse(object sender, ScanResponseEventArgs e)
        {
            ScanResponse?.Invoke(sender, new BleScanResponseReceivedEventArgs(e.rssi, e.packet_type, e.sender, (BleAddressType)e.address_type, e.bond, e.data));
        }
    }
}
