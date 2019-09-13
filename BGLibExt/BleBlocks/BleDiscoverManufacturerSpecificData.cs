using Bluegiga.BLE.Events.GAP;
using System.IO.Ports;
using System.Linq;

namespace BGLibExt.BleBlocks
{
    internal class BleDiscoverManufacturerSpecificData : BleBlock
    {
        private ScanResponseEventArgs _scanResponse = null;

        public ushort ManufacturerId { get; private set; }

        public BleDiscoverManufacturerSpecificData(BleProtocol ble, SerialPort port, ushort manufacturerId)
            : base(ble, port)
        {
            ManufacturerId = manufacturerId;
        }

        public ScanResponseEventArgs Execute()
        {
            var scanParams = Ble.Lib.BLECommandGAPSetScanParameters(0xC8, 0xC8, 1);
            var scanDiscover = Ble.Lib.BLECommandGAPDiscover(1);

            Ble.Lib.BLEEventGAPScanResponse += FindManufacturerId;

            Ble.SendCommand(Port, scanParams);
            Ble.SendCommand(Port, scanDiscover);

            WaitEvent(() => _scanResponse != null);

            Ble.SendCommand(Port, Ble.Lib.BLECommandGAPEndProcedure());

            Ble.Lib.BLEEventGAPScanResponse -= FindManufacturerId;

            return _scanResponse;
        }

        private void FindManufacturerId(object sender, ScanResponseEventArgs e)
        {
            var advertisementData = BleAdvertisingDataParser.Parse(e.data);
            var manufacturerAdvertisementData = advertisementData.SingleOrDefault(x => x.Type == BleAdvertisingDataType.ManufacturerSpecificData);

            if (manufacturerAdvertisementData?.GetManufacturerId() == ManufacturerId)
            {
                _scanResponse = e;
            }
        }
    }
}
