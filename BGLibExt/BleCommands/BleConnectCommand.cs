using Bluegiga;
using Bluegiga.BLE.Events.Connection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleConnectCommand : BleCommand
    {
        public BleConnectCommand(BGLib bgLib, BleModuleConnection bleModuleConnection)
            : base(bgLib, bleModuleConnection)
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
                    _bgLib.BLEEventConnectionStatus += OnConnectionStatus;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), useSynchronizationContext: false))
                    {
                        _bgLib.SendCommand(_bleModuleConnection.SerialPort, _bgLib.BLECommandGAPConnectDirect((byte[])address, (byte)addressType, 0x20, 0x30, 0x100, 0));

                        return await taskCompletionSource.Task.ConfigureAwait(continueOnCapturedContext: false);
                    }
                }
                finally
                {
                    _bgLib.BLEEventConnectionStatus -= OnConnectionStatus;
                }
            }
        }
    }
}
