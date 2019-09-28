using Bluegiga.BLE.Events.ATTClient;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleWriteAttributeCommand : BleCommand
    {
        public BleWriteAttributeCommand()
            : base(BleModuleConnection.Instance.BleProtocol, BleModuleConnection.Instance.SerialPort)
        {
        }

        public BleWriteAttributeCommand(BleProtocol ble, SerialPort port)
            : base(ble, port)
        {
        }

        public async Task<ProcedureCompletedEventArgs> ExecuteAsync(byte connection, ushort attributeHandle, byte[] value, int timeout = DefaultTimeout)
        {
            return await ExecuteAsync(connection, attributeHandle, value, CancellationToken.None, timeout);
        }

        public async Task<ProcedureCompletedEventArgs> ExecuteAsync(byte connection, ushort attributeHandle, byte[] value, CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
            var taskCompletionSource = new TaskCompletionSource<ProcedureCompletedEventArgs>();
            using (var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                cancellationTokenSource.CancelAfter(timeout);

                void OnProcedureCompleted(object sender, ProcedureCompletedEventArgs e)
                {
                    if (e.connection == connection)
                    {
                        taskCompletionSource.SetResult(e);
                    }
                }

                try
                {
                    Ble.Lib.BLEEventATTClientProcedureCompleted += OnProcedureCompleted;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), useSynchronizationContext: false))
                    {
                        Ble.SendCommand(Port, Ble.Lib.BLECommandATTClientAttributeWrite(connection, attributeHandle, value));

                        return await taskCompletionSource.Task.ConfigureAwait(continueOnCapturedContext: false);
                    }
                }
                finally
                {
                    Ble.Lib.BLEEventATTClientProcedureCompleted -= OnProcedureCompleted;
                }
            }
        }
    }
}
