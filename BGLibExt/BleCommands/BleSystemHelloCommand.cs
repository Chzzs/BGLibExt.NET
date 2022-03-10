using Bluegiga;
using Bluegiga.BLE.Responses.System;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleSystemHelloCommand : BleCommand
    {
        public BleSystemHelloCommand(BGLib bgLib, BleModuleConnection bleModuleConnection, ILogger logger)
            : base(bgLib, bleModuleConnection, logger)
        {
        }

        public async Task<HelloEventArgs> ExecuteAsync(int timeout = DefaultTimeout)
        {
            return await ExecuteAsync(CancellationToken.None, timeout);
        }

        public async Task<HelloEventArgs> ExecuteAsync(CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
            Logger?.LogDebug($"System Hello");


            var taskCompletionSource = new TaskCompletionSource<HelloEventArgs>();
            using (var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                cancellationTokenSource.CancelAfter(timeout);

                void OnSystemHelloResponse(object sender, HelloEventArgs e)
                {

                    Logger?.LogDebug($"System Hello Response");

                    taskCompletionSource.SetResult(e);

                }

                try
                {
                    BgLib.BLEResponseSystemHello += OnSystemHelloResponse;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), false))
                    {
                        BgLib.SendCommand(BleModuleConnection.SerialPort, BgLib.BLECommandSystemHello());

                        return await taskCompletionSource.Task.ConfigureAwait(false);
                    }
                }
                finally
                {
                    BgLib.BLEResponseSystemHello -= OnSystemHelloResponse;
                }
            }
        }
    }
}
