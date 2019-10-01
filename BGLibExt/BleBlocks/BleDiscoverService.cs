using Bluegiga.BLE.Events.GAP;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace BGLibExt.BleBlocks
{
    internal class BleDiscoverService : BleBlock
    {
        private static readonly BleAdvertisingDataType[] ServiceUuidAdvertisements = new BleAdvertisingDataType[]
           {
            BleAdvertisingDataType.IncompleteListof16BitServiceClassUUIDs,
            BleAdvertisingDataType.CompleteListof16BitServiceClassUUIDs,
            BleAdvertisingDataType.IncompleteListof32BitServiceClassUUIDs,
            BleAdvertisingDataType.CompleteListof32BitServiceClassUUIDs,
            BleAdvertisingDataType.IncompleteListof128BitServiceClassUUIDs,
            BleAdvertisingDataType.CompleteListof128BitServiceClassUUIDs
           };

        private ScanResponseEventArgs _scanResponse = null;

        public byte[] ServiceUuid { get; private set; }

        public BleDiscoverService(BleProtocol ble, SerialPort port, byte[] service)
            : base(ble, port)
        {
            ServiceUuid = service;
        }

        public ScanResponseEventArgs Execute()
        {
            var scanParams = Ble.Lib.BLECommandGAPSetScanParameters(0xC8, 0xC8, 1);
            var scanDiscover = Ble.Lib.BLECommandGAPDiscover(1);

            Ble.Lib.BLEEventGAPScanResponse += FindService;

            Ble.SendCommand(Port, scanParams);
            Ble.SendCommand(Port, scanDiscover);

            WaitEvent(() => _scanResponse != null);

            Ble.SendCommand(Port, Ble.Lib.BLECommandGAPEndProcedure());

            Ble.Lib.BLEEventGAPScanResponse -= FindService;

            return _scanResponse;
        }

        private void FindService(object sender, ScanResponseEventArgs e)
        {
            var advertisementData = BleAdvertisingDataParser.Parse(e.data);
            if (advertisementData.Where(x => ServiceUuidAdvertisements.Contains(x.Type)).Any(x => x.Data.SequenceEqual(ServiceUuid)))
            {
                _scanResponse = e;
            }
        }
    }
}
