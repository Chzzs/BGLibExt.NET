using Bluegiga;
using Bluegiga.BLE.Events.Connection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleConnectCommand : BleCommand
    {
        public BleConnectCommand(BGLib bgLib, BleModuleConnection bleModuleConnection, ILogger logger)
            : base(bgLib, bleModuleConnection, logger)
        {
        }

        public async Task<StatusEventArgs> ExecuteAsync(byte[] address, BleAddressType addressType, int timeout = DefaultTimeout)
        {
            return await ExecuteAsync(address, addressType, CancellationToken.None, timeout);
        }

        public async Task<StatusEventArgs> ExecuteAsync(byte[] address, BleAddressType addressType, CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
            Logger?.LogDebug($"Connect to device, Address={address.ToHexString()}, AddressType={addressType}");

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
                    BgLib.BLEEventConnectionStatus += OnConnectionStatus;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), false))
                    {
                        BgLib.SendCommand(BleModuleConnection.SerialPort, BgLib.BLECommandGAPConnectDirect((byte[])address, (byte)addressType, 0x20, 0x30, 0x100, 0));

                        return await taskCompletionSource.Task.ConfigureAwait(false);
                    }
                }
                finally
                {
                    BgLib.BLEEventConnectionStatus -= OnConnectionStatus;
                }
            }
        }
    }
}
