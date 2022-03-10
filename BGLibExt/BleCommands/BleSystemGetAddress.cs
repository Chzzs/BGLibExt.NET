using Bluegiga;
using Bluegiga.BLE.Responses.System;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleSystemGetAddressCommand : BleCommand
    {
        public BleSystemGetAddressCommand(BGLib bgLib, BleModuleConnection bleModuleConnection, ILogger logger)
            : base(bgLib, bleModuleConnection, logger)
        {
        }

        public async Task<AddressGetEventArgs> ExecuteAsync(int timeout = DefaultTimeout)
        {
            return await ExecuteAsync(CancellationToken.None, timeout);
        }

        public async Task<AddressGetEventArgs> ExecuteAsync(CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
            Logger?.LogDebug($"System Get Address");

            var taskCompletionSource = new TaskCompletionSource<AddressGetEventArgs>();
            using (var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                cancellationTokenSource.CancelAfter(timeout);

                void OnSystemAddress(object sender, AddressGetEventArgs e)
                {

                    taskCompletionSource.SetResult(e);

                }

                try
                {
                    BgLib.BLEResponseSystemAddressGet += OnSystemAddress;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), false))
                    {
                        BgLib.SendCommand(BleModuleConnection.SerialPort, BgLib.BLECommandSystemAddressGet());

                        return await taskCompletionSource.Task.ConfigureAwait(false);
                    }
                }
                finally
                {
                    BgLib.BLEResponseSystemAddressGet -= OnSystemAddress;
                }
            }
        }
    }
}
