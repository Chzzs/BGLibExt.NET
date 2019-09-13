using Bluegiga.BLE.Events.ATTClient;
using System.IO.Ports;
using System.Linq;

namespace BGLibExt.BleBlocks
{
    internal class BleFindServicesAndCharacteristics : BleBlock
    {
        private const ushort GattMaxHandle = 0xFFFF;
        private const ushort GattMinHandle = 0x0001;
        private static readonly byte[] GattServiceTypePrimary = new byte[] { 0x00, 0x28 };

        public byte Connection { get; private set; }
        private BlePeripheralMap PeripheralMap { get; set; }

        public BleFindServicesAndCharacteristics(BleProtocol ble, SerialPort port, byte connection)
            : base(ble, port)
        {
            Connection = connection;
        }

        public BlePeripheralMap Execute()
        {
            PeripheralMap = new BlePeripheralMap();

            FindServices();

            foreach (var service in PeripheralMap.Services)
            {
                FindAttributes(service);

                ConvertAttributesToCharacteristics(service);
            }

            return PeripheralMap;
        }

        private static void AttributeFound(BlePeripheralService service, FindInformationFoundEventArgs e)
        {
            service.Attributes.Add(new BlePeripheralAttribute(e.uuid, e.chrhandle));
        }

        private static void ConvertAttributesToCharacteristics(BlePeripheralService service)
        {
            // *=== Service: 42 48 12 4a 7f 2c 48 47 b9 de 04 a9 02 00 06 d5
            // *====== Att: 26 - 00 28
            // *====== Att: 27 - 03 28
            // *====== Att: 28 - 42 48 12 4a 7f 2c 48 47 b9 de 04 a9 02 04 06 d5
            // *====== Att: 29 - 02 29
            // *====== Att: 30 - 03 28
            // *====== Att: 31 - 42 48 12 4a 7f 2c 48 47 b9 de 04 a9 02 05 06 d5
            // *====== Att: 32 - 02 29

            BlePeripheralCharacteristic current = null;
            var createCharacteristic = false;

            foreach (var attr in service.Attributes)
            {
                if (attr.AttributeUuid.SequenceEqual(BlePeripheralAttribute.ServiceUuid))
                {
                    // defines service - do nothing
                }
                else if (attr.AttributeUuid.SequenceEqual(BlePeripheralAttribute.CharacteristicUuid))
                {
                    // finish previous characteristic
                    current = null;

                    // crete characteristic from next attribute
                    createCharacteristic = true;
                }
                else if (attr.AttributeUuid.SequenceEqual(BlePeripheralAttribute.CharacteristicCccUuid))
                {
                    // add ccc capabilities to characteristic
                    current.SetCCCHandle(attr.Handle);
                }
                else
                {
                    // if new characteristic begins - create it else skip and do nothing
                    if (createCharacteristic)
                    {
                        current = new BlePeripheralCharacteristic(attr.AttributeUuid, attr.Handle);
                        createCharacteristic = false;

                        service.Characteristics.Add(current);
                    }
                }
            }
        }

        private void FindAttributes(BlePeripheralService service)
        {
            var findInfo = Ble.Lib.BLECommandATTClientFindInformation(Connection, service.StartHandle, service.EndHandle);

            FindInformationFoundEventHandler infoHandler = (sender, e) => AttributeFound(service, e);

            Ble.Lib.BLEEventATTClientFindInformationFound += infoHandler;

            ProcedureCompletedEventArgs procedureResponse = null;
            ProcedureCompletedEventHandler handler = (sender, e) =>
            {
                procedureResponse = e;
            };
            Ble.Lib.BLEEventATTClientProcedureCompleted += handler;

            Ble.SendCommand(Port, findInfo);

            WaitEvent(() => procedureResponse != null);

            Ble.Lib.BLEEventATTClientFindInformationFound -= infoHandler;
            Ble.Lib.BLEEventATTClientProcedureCompleted -= handler;
        }

        private void FindServices()
        {
            // (established connection, handle.min_address, handle.max_address, primary gatt service attribute identifier)
            var readGroups = Ble.Lib.BLECommandATTClientReadByGroupType(Connection, GattMinHandle, GattMaxHandle, (byte[])GattServiceTypePrimary);

            Ble.Lib.BLEEventATTClientGroupFound += GroupFound;

            ProcedureCompletedEventArgs procedureResponse = null;
            ProcedureCompletedEventHandler handler = (sender, e) =>
            {
                procedureResponse = e;
            };
            Ble.Lib.BLEEventATTClientProcedureCompleted += handler;

            Ble.SendCommand(Port, readGroups);

            WaitEvent(() => procedureResponse != null);

            Ble.Lib.BLEEventATTClientGroupFound -= GroupFound;
            Ble.Lib.BLEEventATTClientProcedureCompleted -= handler;
        }

        private void GroupFound(object sender, GroupFoundEventArgs e)
        {
            PeripheralMap.Services.Add(new BlePeripheralService(e.uuid, e.start, e.end));
        }
    }
}
