using Bluegiga;
using Bluegiga.BLE.Events.Connection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleEncryptCommand : BleCommand
    {
        public BleEncryptCommand(BGLib bgLib, BleModuleConnection bleModuleConnection, ILogger logger)
            : base(bgLib, bleModuleConnection, logger)
        {
        }

        public async Task<StatusEventArgs> ExecuteAsync(byte handle, int timeout = DefaultTimeout)
        {
            return await ExecuteAsync(handle, CancellationToken.None, timeout);
        }

        public async Task<StatusEventArgs> ExecuteAsync(byte handle, CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
            Logger?.LogDebug($"Encrypting connection with handle {handle}");

            var taskCompletionSource = new TaskCompletionSource<StatusEventArgs>();
            using (var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                cancellationTokenSource.CancelAfter(timeout);

                void OnConnectionStatus(object sender, StatusEventArgs e)
                {
                    Logger?.LogDebug($"Encrypting flags are {e.flags}");
                    if ((e.flags & 0x03) == 0x03)
                    {
                        taskCompletionSource.SetResult(e);
                    }
                    else
                    {
                        //    taskCompletionSource.SetException(new Exception("Couldn't encrypt connection"));
                    }
                }

                try
                {
                    BgLib.BLEEventConnectionStatus += OnConnectionStatus;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), false))
                    {
                        BgLib.SendCommand(BleModuleConnection.SerialPort, BgLib.BLECommandSMEncryptStart((byte)handle, (byte)0));

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
