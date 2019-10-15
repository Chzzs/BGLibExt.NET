using Bluegiga;
using Bluegiga.BLE.Events.ATTClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleFindCharacteristicsCommand : BleCommand
    {
        public BleFindCharacteristicsCommand(BGLib bgLib, BleModuleConnection bleModuleConnection, ILogger logger)
            : base(bgLib, bleModuleConnection, logger)
        {
        }

        public async Task<(List<BleCharacteristic>, List<BleAttribute>)> ExecuteAsync(byte connection, ushort startHandle, ushort endHandle, int timeout = DefaultTimeout)
        {
            return await ExecuteAsync(connection, startHandle, endHandle, CancellationToken.None, timeout);
        }

        public async Task<(List<BleCharacteristic>, List<BleAttribute>)> ExecuteAsync(byte connection, ushort startHandle, ushort endHandle, CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
            Logger?.LogDebug($"Find device characteristics, Connection={connection}, StartHandle={startHandle}, EndHandle={endHandle}");

            var taskCompletionSource = new TaskCompletionSource<(List<BleCharacteristic>, List<BleAttribute>)>();
            using (var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                cancellationTokenSource.CancelAfter(timeout);

                var attributes = new List<BleAttribute>();

                void OnFindInformationFound(object sender, FindInformationFoundEventArgs e)
                {
                    if (e.connection == connection)
                    {
                        attributes.Add(new BleAttribute(e.connection, e.uuid, e.chrhandle));
                    }
                }

                void OnProcedureCompleted(object sender, ProcedureCompletedEventArgs e)
                {
                    if (e.connection == connection)
                    {
                        if (attributes.Any())
                        {
                            var characteristics = ConvertAttributesToCharacteristics(attributes);
                            taskCompletionSource.SetResult((characteristics, attributes));
                        }
                        else
                        {
                            taskCompletionSource.SetException(new Exception("Couldn't find any characteristics"));
                        }
                    }
                }

                try
                {
                    BgLib.BLEEventATTClientFindInformationFound += OnFindInformationFound;
                    BgLib.BLEEventATTClientProcedureCompleted += OnProcedureCompleted;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), false))
                    {
                        BgLib.SendCommand(BleModuleConnection.SerialPort, BgLib.BLECommandATTClientFindInformation(connection, startHandle, endHandle));

                        return await taskCompletionSource.Task.ConfigureAwait(false);
                    }
                }
                finally
                {
                    BgLib.BLEEventATTClientFindInformationFound -= OnFindInformationFound;
                    BgLib.BLEEventATTClientProcedureCompleted -= OnProcedureCompleted;
                }
            }
        }

        private List<BleCharacteristic> ConvertAttributesToCharacteristics(List<BleAttribute> attributes)
        {
            // *=== Service: 42 48 12 4a 7f 2c 48 47 b9 de 04 a9 02 00 06 d5
            // *====== Att: 26 - 00 28
            // *====== Att: 27 - 03 28
            // *====== Att: 28 - 42 48 12 4a 7f 2c 48 47 b9 de 04 a9 02 04 06 d5
            // *====== Att: 29 - 02 29
            // *====== Att: 30 - 03 28
            // *====== Att: 31 - 42 48 12 4a 7f 2c 48 47 b9 de 04 a9 02 05 06 d5
            // *====== Att: 32 - 02 29

            var characteristics = new List<BleCharacteristic>();
            BleCharacteristic current = null;
            var createCharacteristic = false;

            foreach (var attr in attributes)
            {
                if (attr.Uuid.SequenceEqual(BleAttribute.ServiceUuid))
                {
                    // defines service - do nothing
                }
                else if (attr.Uuid.SequenceEqual(BleAttribute.CharacteristicUuid))
                {
                    // finish previous characteristic
                    current = null;

                    // create characteristic from next attribute
                    createCharacteristic = true;
                }
                else if (attr.Uuid.SequenceEqual(BleAttribute.CharacteristicCccUuid))
                {
                    // add ccc capabilities to characteristic
                    current.SetCccHandle(attr.Handle);
                }
                else
                {
                    // if new characteristic begins - create it else skip and do nothing
                    if (createCharacteristic)
                    {
                        current = new BleCharacteristic(BgLib, BleModuleConnection, Logger, attr.Connection, attr.Uuid, attr.Handle);
                        createCharacteristic = false;

                        characteristics.Add(current);
                    }
                }
            }

            return characteristics;
        }
    }
}
