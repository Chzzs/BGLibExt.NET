using Bluegiga;
using Bluegiga.BLE.Events.GAP;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleDiscoverServiceCommand : BleCommand
    {
        private static readonly BleAdvertisingDataType[] ServiceUuidAdvertisements = new BleAdvertisingDataType[]
        {
            BleAdvertisingDataType.IncompleteListof16BitServiceClassUUIDs,
            BleAdvertisingDataType.CompleteListof16BitServiceClassUUIDs,
            BleAdvertisingDataType.IncompleteListof32BitServiceClassUUIDs,
            BleAdvertisingDataType.CompleteListof32BitServiceClassUUIDs,
            BleAdvertisingDataType.IncompleteListof128BitServiceClassUUIDs,
            BleAdvertisingDataType.CompleteListof128BitServiceClassUUIDs
        };

        public BleDiscoverServiceCommand(BGLib bgLib, BleModuleConnection bleModuleConnection, ILogger logger)
            : base(bgLib, bleModuleConnection, logger)
        {
        }

        public async Task<ScanResponseEventArgs> ExecuteAsync(byte[] serviceUuid, int timeout = DefaultTimeout)
        {
            return await ExecuteAsync(serviceUuid, CancellationToken.None, timeout);
        }

        public async Task<ScanResponseEventArgs> ExecuteAsync(byte[] serviceUuid, CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
            Logger?.LogDebug($"Discover device by service Uuid {serviceUuid.ToHexString()}");

            var taskCompletionSource = new TaskCompletionSource<ScanResponseEventArgs>();
            using (var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                cancellationTokenSource.CancelAfter(timeout);

                void OnScanResponse(object sender, ScanResponseEventArgs e)
                {
                    var advertisementData = BleAdvertisingDataParser.Parse(e.data);
                    if (advertisementData.Where(x => ServiceUuidAdvertisements.Contains(x.Type)).Any(x => x.Data.SequenceEqual(serviceUuid)))
                    {
                        taskCompletionSource.SetResult(e);
                    }
                }

                try
                {
                    BgLib.BLEEventGAPScanResponse += OnScanResponse;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), false))
                    {
                        BgLib.SendCommand(BleModuleConnection.SerialPort, BgLib.BLECommandGAPSetScanParameters(0xC8, 0xC8, 1));
                        BgLib.SendCommand(BleModuleConnection.SerialPort, BgLib.BLECommandGAPDiscover(1));

                        return await taskCompletionSource.Task.ConfigureAwait(false);
                    }
                }
                finally
                {
                    BgLib.SendCommand(BleModuleConnection.SerialPort, BgLib.BLECommandGAPEndProcedure());

                    BgLib.BLEEventGAPScanResponse -= OnScanResponse;
                }
            }
        }
    }
}
