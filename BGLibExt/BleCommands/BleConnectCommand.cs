using Bluegiga.BLE.Events.Connection;
using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleConnectCommand : BleCommand
    {
        public BleConnectCommand()
            : base(BleModuleConnection.Instance.BleProtocol, BleModuleConnection.Instance.SerialPort)
        {
        }

        public BleConnectCommand(BleProtocol ble, SerialPort port)
            : base(ble, port)
        {
        }

        public async Task<StatusEventArgs> ExecuteAsync(byte[] address, BleAddressType addressType, int timeout = DefaultTimeout)
        {
            return await ExecuteAsync(address, addressType, CancellationToken.None, timeout);
        }

        public async Task<StatusEventArgs> ExecuteAsync(byte[] address, BleAddressType addressType, CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
            var taskCompletionSource = new TaskCompletionSource<StatusEventArgs>();
            using (var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                cancellationTokenSource.CancelAfter(timeout);

                void OnConnectionStatus(object sender, StatusEventArgs e)
                {
                    if ((e.flags & 0x05) == 0x05)
                    {
                        taskCompletionSource.SetResult(e);
                    }
                    else
                    {
                        taskCompletionSource.SetException(new Exception("Couldn't connect to device"));
                    }
                }

                try
                {
                    Ble.Lib.BLEEventConnectionStatus += OnConnectionStatus;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), useSynchronizationContext: false))
                    {
                        Ble.SendCommand(Port, Ble.Lib.BLECommandGAPConnectDirect((byte[])address, (byte)addressType, 0x20, 0x30, 0x100, 0));

                        return await taskCompletionSource.Task.ConfigureAwait(continueOnCapturedContext: false);
                    }
                }
                finally
                {
                    Ble.Lib.BLEEventConnectionStatus -= OnConnectionStatus;
                }
            }
        }
    }
}
