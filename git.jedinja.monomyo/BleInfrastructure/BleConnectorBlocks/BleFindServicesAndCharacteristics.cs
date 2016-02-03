using System;
using System.IO.Ports;
using git.jedinja.monomyo.BleInfrastructure.BleBackbone;
using git.jrowberg.bglib.Bluegiga.BLE.Events.ATTClient;
using System.Linq;

namespace git.jedinja.monomyo.BleInfrastructure.BleConnectorBlocks
{
	internal class BleFindServicesAndCharacteristics : BleBlock
	{
		private static readonly Bytes GATT_SERVICE_TYPE_PRIMARY = new Bytes (0x00, 0x28);
		private const ushort GATT_MIN_HANDLE = 0x0001;
		private const ushort GATT_MAX_HANDLE = 0xFFFF;

		public byte Connection  { get; private set; }
		private BlePeripheralMap PeripheralMap { get; set; }

		public BleFindServicesAndCharacteristics (BleProtocol ble, SerialPort port, byte connection)
			: base (ble, port)
		{
			this.Connection = connection;
		}

		public BlePeripheralMap Execute ()
		{
			this.PeripheralMap = new BlePeripheralMap ();

			this.FindServices ();

			foreach (BlePeripheralService service in this.PeripheralMap.Services)
			{
				this.FindAttributes (service);

				this.ConvertAttributesToCharacteristics (service);
			}

			return this.PeripheralMap;
		}

		#region Find Services

		private void FindServices ()
		{
			// (established connection, handle.min_address, handle.max_address, primary gatt service attribute identifier)
			byte[] readGroups = Ble.Lib.BLECommandATTClientReadByGroupType (this.Connection, GATT_MIN_HANDLE, GATT_MAX_HANDLE, (byte[]) GATT_SERVICE_TYPE_PRIMARY);

			Ble.Lib.BLEEventATTClientGroupFound += GroupFound;

			ProcedureCompletedEventArgs procedureResponse = null;
			ProcedureCompletedEventHandler handler = (sender, e) => {
				procedureResponse = e;
			};
			Ble.Lib.BLEEventATTClientProcedureCompleted += handler;

			Ble.SendCommand (this.Port, readGroups);

			this.WaitEvent (() => procedureResponse != null);

			Ble.Lib.BLEEventATTClientGroupFound -= GroupFound;
			Ble.Lib.BLEEventATTClientProcedureCompleted -= handler;
		}

		private void GroupFound (object sender, GroupFoundEventArgs e)
		{
			this.PeripheralMap.Services.Add (new BlePeripheralService (e.uuid, e.start, e.end));
		}

		#endregion

		#region Find Attributes

		private void FindAttributes (BlePeripheralService service)
		{
			byte[] findInfo = Ble.Lib.BLECommandATTClientFindInformation (this.Connection, service.StartHandle, service.EndHandle);

			FindInformationFoundEventHandler infoHandler = (sender, e) => this.AttributeFound (service, e);
				
			Ble.Lib.BLEEventATTClientFindInformationFound += infoHandler;

			ProcedureCompletedEventArgs procedureResponse = null;
			ProcedureCompletedEventHandler handler = (sender, e) => {
				procedureResponse = e;
			};
			Ble.Lib.BLEEventATTClientProcedureCompleted += handler;

			Ble.SendCommand (this.Port, findInfo);

			this.WaitEvent (() => procedureResponse != null);

			Ble.Lib.BLEEventATTClientFindInformationFound -= infoHandler;
			Ble.Lib.BLEEventATTClientProcedureCompleted -= handler;
		}

		private void AttributeFound (BlePeripheralService service, FindInformationFoundEventArgs e)
		{
			service.Attributes.Add (new BlePeripheralAttribute (e.uuid, e.chrhandle));
		}


		private void ConvertAttributesToCharacteristics (BlePeripheralService service)
		{
			#region Example attr table

//			*=== Service: 42 48 12 4a 7f 2c 48 47 b9 de 04 a9 02 00 06 d5 
//			*====== Att: 26 - 00 28 
//			*====== Att: 27 - 03 28 
//			*====== Att: 28 - 42 48 12 4a 7f 2c 48 47 b9 de 04 a9 02 04 06 d5 
//			*====== Att: 29 - 02 29 
//			*====== Att: 30 - 03 28 
//			*====== Att: 31 - 42 48 12 4a 7f 2c 48 47 b9 de 04 a9 02 05 06 d5 
//			*====== Att: 32 - 02 29 

			#endregion

			BlePeripheralCharacteristic current = null;
			bool createCharacteristic = false;

			foreach (BlePeripheralAttribute attr in service.Attributes)
			{
				if (attr.AttributeUUID.Equals (BlePeripheralAttribute.SERVICE_UUID))
				{
					// defines service - do nothing
				}
				else if (attr.AttributeUUID.Equals (BlePeripheralAttribute.CHARACTERISTIC_UUID))
				{
					// finish previous characteristic
					current = null;

					// crete characteristic from next attribute
					createCharacteristic = true;
				}
				else if (attr.AttributeUUID.Equals (BlePeripheralAttribute.CHARACTERISTIC_CCC_UUID))
				{
					// add ccc capabilities to characteristic
					current.SetCCCHandle (attr.Handle);
				}
				else
				{
					// if new characteristic begins - create it else skip and do nothing
					if (createCharacteristic)
					{
						current = new BlePeripheralCharacteristic (attr.AttributeUUID, attr.Handle);
						createCharacteristic = false;

						service.Characteristics.Add (current);
					}
				}
			}
		}

		#endregion
	}
}

