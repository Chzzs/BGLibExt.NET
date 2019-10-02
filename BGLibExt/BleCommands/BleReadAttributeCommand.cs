using Bluegiga;
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
        public BleReadAttributeCommand(BGLib bgLib, BleModuleConnection bleModuleConnection)
            : base(bgLib, bleModuleConnection)
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
                    _bgLib.BLEEventATTClientAttributeValue += OnAttributeValue;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), useSynchronizationContext: false))
                    {
                        _bgLib.SendCommand(_bleModuleConnection.SerialPort, _bgLib.BLECommandATTClientReadByHandle(connection, attributeHandle));

                        return await taskCompletionSource.Task.ConfigureAwait(continueOnCapturedContext: false);
                    }
                }
                finally
                {
                    _bgLib.BLEEventATTClientAttributeValue -= OnAttributeValue;
                }
            }
        }

        private async Task<(ProcedureCompletedEventArgs, AttributeValueEventArgs)> ReadLong(byte connection, ushort attributeHandle, CancellationToken cancellationToken, int timeout = DefaultTimeout)
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
                    _bgLib.BLEEventATTClientAttributeValue += OnAttributeValue;
                    _bgLib.BLEEventATTClientProcedureCompleted += OnProcedureCompleted;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), useSynchronizationContext: false))
                    {
                        _bgLib.SendCommand(_bleModuleConnection.SerialPort, _bgLib.BLECommandATTClientReadLong(connection, attributeHandle));

                        return await taskCompletionSource.Task.ConfigureAwait(continueOnCapturedContext: false);
                    }
                }
                finally
                {
                    _bgLib.BLEEventATTClientAttributeValue -= OnAttributeValue;
                    _bgLib.BLEEventATTClientProcedureCompleted -= OnProcedureCompleted;
                }
            }
        }
    }
}
