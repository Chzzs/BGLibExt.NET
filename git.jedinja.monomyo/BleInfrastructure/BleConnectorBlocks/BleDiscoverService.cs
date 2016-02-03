using System;
using git.jrowberg.bglib.Bluegiga.BLE.Events.GAP;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;

namespace git.jedinja.monomyo.BleInfrastructure.BleConnectorBlocks
{
	internal class BleDiscoverService : BleBlock
	{
		public Bytes ServiceUUID { get; private set; }

		public BleDiscoverService (BleProtocol ble, SerialPort port, Bytes service)
			: base (ble, port)
		{
			this.ServiceUUID = service;
		}

		public ScanResponseEventArgs Execute ()
		{
			// Send discover command
			byte[] scanParams = Ble.Lib.BLECommandGAPSetScanParameters (0xC8, 0xC8, 1);
			byte[] scanDiscover = Ble.Lib.BLECommandGAPDiscover (1);

			ScanResponseEventHandler handler = this.FindService;

			Ble.Lib.BLEEventGAPScanResponse += handler;

			Ble.SendCommand (this.Port, scanParams);
			Ble.SendCommand (this.Port, scanDiscover);

			this.WaitEvent (() => _scanResponse != null);

			Ble.Lib.BLEEventGAPScanResponse -= handler; // not needed anymore
			return _scanResponse;
		}

		private ScanResponseEventArgs _scanResponse = null;
		private void FindService (object sender, ScanResponseEventArgs e)
		{
			// pull all advertised service info from ad packet
			// taken from bglib example code
			List<Byte[]> ad_services = new List<Byte[]> ();
			Byte[] this_field = { };
			int bytes_left = 0;
			int field_offset = 0;
			for (int i = 0; i < e.data.Length; i++)
			{
				if (bytes_left == 0)
				{
					bytes_left = e.data[i];
					this_field = new Byte[e.data[i]];
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
							ad_services.Add (this_field.Skip (1).Take (2).Reverse ().ToArray ());
						}
						else if (this_field[0] == 0x04 || this_field[0] == 0x05)
						{
							// partial or complete list of 32-bit UUIDs
							ad_services.Add (this_field.Skip (1).Take (4).Reverse ().ToArray ());
						}
						else if (this_field[0] == 0x06 || this_field[0] == 0x07)
						{
							// partial or complete list of 128-bit UUIDs
							ad_services.Add (this_field.Skip (1).Take (16).Reverse ().ToArray ());
						}
					}
				}
			}

			if (ad_services.Any (a => a.SequenceEqual (((byte[]) ServiceUUID).Reverse ())))
			{
				_scanResponse = e;
			}
		}
	}
}

