using Bluegiga;
using Bluegiga.BLE.Events.GAP;

namespace BGLibExt
{
    public class BleDeviceDiscovery
    {
        private readonly BGLib _bgLib;
        private readonly BleModuleConnection _bleModuleConnection;

        public event ScanResponseReceivedEventHandler ScanResponse;

        public delegate void ScanResponseReceivedEventHandler(object sender, BleScanResponseReceivedEventArgs e);

        public BleDeviceDiscovery(BGLib bgLib, BleModuleConnection bleModuleConnection)
        {
            _bgLib = bgLib;
            _bleModuleConnection = bleModuleConnection;
        }

        /// <summary>
        /// Start device discovery
        /// </summary>
        public void StartDeviceDiscovery()
        {
            _bgLib.BLEEventGAPScanResponse += OnScanResponse;

            _bgLib.SendCommand(_bleModuleConnection.SerialPort, _bgLib.BLECommandGAPSetScanParameters(0xC8, 0xC8, 1));
            _bgLib.SendCommand(_bleModuleConnection.SerialPort, _bgLib.BLECommandGAPDiscover(1));
        }

        /// <summary>
        /// Stop device discovery
        /// </summary>
        public void StopDeviceDiscovery()
        {
            _bgLib.SendCommand(_bleModuleConnection.SerialPort, _bgLib.BLECommandGAPEndProcedure());

            _bgLib.BLEEventGAPScanResponse -= OnScanResponse;
        }

        private void OnScanResponse(object sender, ScanResponseEventArgs e)
        {
            ScanResponse?.Invoke(sender, new BleScanResponseReceivedEventArgs(e.rssi, e.packet_type, e.sender, (BleAddressType)e.address_type, e.bond, e.data));
        }
    }
}
