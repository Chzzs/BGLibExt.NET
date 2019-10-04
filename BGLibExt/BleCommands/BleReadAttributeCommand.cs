using Bluegiga;
using Bluegiga.BLE.Events.ATTClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleReadAttributeCommand : BleCommand
    {
        public BleReadAttributeCommand(BGLib bgLib, BleModuleConnection bleModuleConnection, ILogger logger)
            : base(bgLib, bleModuleConnection, logger)
        {
        }

        public async Task<(ProcedureCompletedEventArgs, AttributeValueEventArgs)> ExecuteAsync(byte connection, ushort attributeHandle, bool readLongValue = false, int timeout = DefaultTimeout)
        {
            return await ExecuteAsync(connection, attributeHandle, readLongValue, CancellationToken.None, timeout);
        }

        public async Task<(ProcedureCompletedEventArgs, AttributeValueEventArgs)> ExecuteAsync(byte connection, ushort attributeHandle, bool readLongValue, CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
            if (readLongValue)
            {
                return await ReadLong(connection, attributeHandle, cancellationToken, timeout);
            }
            else
            {
                return await ReadByHandle(connection, attributeHandle, cancellationToken, timeout);
            }
        }

        private async Task<(ProcedureCompletedEventArgs, AttributeValueEventArgs)> ReadByHandle(byte connection, ushort attributeHandle, CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
            Logger?.LogTrace($"Read device characteristic, Connection={connection}, AttributeHandle={attributeHandle}");

            var taskCompletionSource = new TaskCompletionSource<(ProcedureCompletedEventArgs, AttributeValueEventArgs)>();
            using (var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                cancellationTokenSource.CancelAfter(timeout);

                void OnAttributeValue(object sender, AttributeValueEventArgs e)
                {
                    if (e.connection == connection && e.atthandle == attributeHandle)
                    {
                        taskCompletionSource.SetResult((null, new AttributeValueEventArgs(e.connection, e.atthandle, e.type, e.value)));
                    }
                }

                try
                {
                    BgLib.BLEEventATTClientAttributeValue += OnAttributeValue;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), useSynchronizationContext: false))
                    {
                        BgLib.SendCommand(BleModuleConnection.SerialPort, BgLib.BLECommandATTClientReadByHandle(connection, attributeHandle));

                        return await taskCompletionSource.Task.ConfigureAwait(continueOnCapturedContext: false);
                    }
                }
                finally
                {
                    BgLib.BLEEventATTClientAttributeValue -= OnAttributeValue;
                }
            }
        }

        private async Task<(ProcedureCompletedEventArgs, AttributeValueEventArgs)> ReadLong(byte connection, ushort attributeHandle, CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
            Logger?.LogTrace($"Read long device characteristic, Connection={connection}, AttributeHandle={attributeHandle}");

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
                    if (e.connection == connection && e.chrhandle == attributeHandle)
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
                    BgLib.BLEEventATTClientAttributeValue += OnAttributeValue;
                    BgLib.BLEEventATTClientProcedureCompleted += OnProcedureCompleted;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), useSynchronizationContext: false))
                    {
                        BgLib.SendCommand(BleModuleConnection.SerialPort, BgLib.BLECommandATTClientReadLong(connection, attributeHandle));

                        return await taskCompletionSource.Task.ConfigureAwait(continueOnCapturedContext: false);
                    }
                }
                finally
                {
                    BgLib.BLEEventATTClientAttributeValue -= OnAttributeValue;
                    BgLib.BLEEventATTClientProcedureCompleted -= OnProcedureCompleted;
                }
            }
        }
    }
}
