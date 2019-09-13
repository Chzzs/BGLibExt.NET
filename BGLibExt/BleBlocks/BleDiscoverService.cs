using Bluegiga.BLE.Events.GAP;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace BGLibExt.BleBlocks
{
    internal class BleDiscoverService : BleBlock
    {
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
            // pull all advertised service info from ad packet
            // taken from bglib example code
            var ad_services = new List<byte[]>();
            byte[] this_field = { };
            var bytes_left = 0;
            var field_offset = 0;
            for (var i = 0; i < e.data.Length; i++)
            {
                if (bytes_left == 0)
                {
                    bytes_left = e.data[i];
                    this_field = new byte[e.data[i]];
                    field_offset = i + 1;
                }
                else
                {
                    this_field[i - field_offset] = e.data[i];
                    bytes_left--;
                    if (bytes_left == 0)
                    {
                        if (this_field[0] == 0x02 || this_field[0] == 0x03)
                        {
                            // partial or complete list of 16-bit UUIDs
                            ad_services.Add(this_field.Skip(1).Take(2).Reverse().ToArray());
                        }
                        else if (this_field[0] == 0x04 || this_field[0] == 0x05)
                        {
                            // partial or complete list of 32-bit UUIDs
                            ad_services.Add(this_field.Skip(1).Take(4).Reverse().ToArray());
                        }
                        else if (this_field[0] == 0x06 || this_field[0] == 0x07)
                        {
                            // partial or complete list of 128-bit UUIDs
                            ad_services.Add(this_field.Skip(1).Take(16).Reverse().ToArray());
                        }
                    }
                }
            }

            if (ad_services.Any(a => a.SequenceEqual(((byte[])ServiceUuid).Reverse())))
            {
                _scanResponse = e;
            }
        }
    }
}
