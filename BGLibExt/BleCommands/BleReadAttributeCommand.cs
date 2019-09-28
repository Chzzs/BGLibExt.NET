using Bluegiga.BLE.Events.ATTClient;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleReadAttributeCommand : BleCommand
    {
        public BleReadAttributeCommand()
            : base(BleModuleConnection.Instance.BleProtocol, BleModuleConnection.Instance.SerialPort)
        {
        }

        public BleReadAttributeCommand(BleProtocol ble, SerialPort port)
            : base(ble, port)
        {
        }

        public async Task<(ProcedureCompletedEventArgs, AttributeValueEventArgs)> ExecuteAsync(byte connection, ushort attributeHandle, bool readLongValue = false, int timeout = DefaultTimeout)
        {
            return await ExecuteAsync(connection, attributeHandle, readLongValue, CancellationToken.None, timeout);
        }

        public async Task<(ProcedureCompletedEventArgs, AttributeValueEventArgs)> ExecuteAsync(byte connection, ushort attributeHandle, bool readLongValue, CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
            var taskCompletionSource = new TaskCompletionSource<(ProcedureCompletedEventArgs, AttributeValueEventArgs)>();
            using (var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                cancellationTokenSource.CancelAfter(timeout);

                AttributeValueEventArgs attributeValueEventArgs = null;
                var attributeValueBuffer = new List<byte>();

                void OnAttributeValue(object sender, AttributeValueEventArgs e)
                {
                    if (e.connection == connection && e.atthandle == attributeHandle)
                    {
                        attributeValueEventArgs = e;
                        attributeValueBuffer.AddRange(e.value);
                    }
                }

                void OnProcedureCompleted(object sender, ProcedureCompletedEventArgs e)
                {
                    if (e.connection == connection)
                    {
                        if (attributeValueEventArgs != null)
                        {
                            taskCompletionSource.SetResult((e, new AttributeValueEventArgs(attributeValueEventArgs.connection, attributeValueEventArgs.atthandle, attributeValueEventArgs.type, attributeValueBuffer.ToArray())));
                        }
                        else
                        {
                            taskCompletionSource.SetException(new Exception("Didn't receive any attribute values"));
                        }
                    }
                }

                try
                {
                    Ble.Lib.BLEEventATTClientAttributeValue += OnAttributeValue;
                    Ble.Lib.BLEEventATTClientProcedureCompleted += OnProcedureCompleted;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), useSynchronizationContext: false))
                    {
                        if (readLongValue)
                        {
                            Ble.SendCommand(Port, Ble.Lib.BLECommandATTClientReadByHandle(connection, attributeHandle));
                        }
                        else
                        {
                            Ble.SendCommand(Port, Ble.Lib.BLECommandATTClientReadLong(connection, attributeHandle));
                        }

                        return await taskCompletionSource.Task.ConfigureAwait(continueOnCapturedContext: false);
                    }
                }
                finally
                {
                    Ble.Lib.BLEEventATTClientAttributeValue -= OnAttributeValue;
                    Ble.Lib.BLEEventATTClientProcedureCompleted -= OnProcedureCompleted;
                }
            }
        }
    }
}
