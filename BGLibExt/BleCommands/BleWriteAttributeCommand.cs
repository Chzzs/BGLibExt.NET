using Bluegiga;
using Bluegiga.BLE.Events.ATTClient;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleWriteAttributeCommand : BleCommand
    {
        public BleWriteAttributeCommand(BGLib bgLib, BleModuleConnection bleModuleConnection, ILogger logger)
            : base(bgLib, bleModuleConnection, logger)
        {
        }

        public async Task<ProcedureCompletedEventArgs> ExecuteAsync(byte connection, ushort attributeHandle, byte[] value, int timeout = DefaultTimeout)
        {
            return await ExecuteAsync(connection, attributeHandle, value, CancellationToken.None, timeout);
        }

        public async Task<ProcedureCompletedEventArgs> ExecuteAsync(byte connection, ushort attributeHandle, byte[] value, CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
            Logger?.LogDebug($"Write device characteristic, Connection={connection}, AttributeHandle={attributeHandle}, Value={value.ToHexString()}");

            var taskCompletionSource = new TaskCompletionSource<ProcedureCompletedEventArgs>();
            using (var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                cancellationTokenSource.CancelAfter(timeout);

                void OnProcedureCompleted(object sender, ProcedureCompletedEventArgs e)
                {
                    if (e.connection == connection && e.chrhandle == attributeHandle)
                    {
                        taskCompletionSource.SetResult(e);
                    }
                }

                try
                {
                    BgLib.BLEEventATTClientProcedureCompleted += OnProcedureCompleted;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), false))
                    {
                        BgLib.SendCommand(BleModuleConnection.SerialPort, BgLib.BLECommandATTClientAttributeWrite(connection, attributeHandle, value));

                        return await taskCompletionSource.Task.ConfigureAwait(false);
                    }
                }
                finally
                {
                    BgLib.BLEEventATTClientProcedureCompleted -= OnProcedureCompleted;
                }
            }
        }
    }
}
