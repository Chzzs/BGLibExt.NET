using Bluegiga;
using Bluegiga.BLE.Events.GAP;
using Microsoft.Extensions.Logging;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleDiscoverManufacturerSpecificDataCommand : BleCommand
    {
        public BleDiscoverManufacturerSpecificDataCommand(BGLib bgLib, BleModuleConnection bleModuleConnection, ILogger logger)
            : base(bgLib, bleModuleConnection, logger)
        {
        }

        public async Task<ScanResponseEventArgs> ExecuteAsync(ushort manufacturerId, int timeout = DefaultTimeout)
        {
            return await ExecuteAsync(manufacturerId, CancellationToken.None, timeout);
        }

        public async Task<ScanResponseEventArgs> ExecuteAsync(ushort manufacturerId, CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
            Logger?.LogDebug($"Discover device by Manufacturer ID {manufacturerId}");

            var taskCompletionSource = new TaskCompletionSource<ScanResponseEventArgs>();
            using (var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                cancellationTokenSource.CancelAfter(timeout);

                void OnScanResponse(object sender, ScanResponseEventArgs e)
                {
                    var advertisementData = BleAdvertisingDataParser.Parse(e.data);
                    var manufacturerAdvertisementData = advertisementData.SingleOrDefault(x => x.Type == BleAdvertisingDataType.ManufacturerSpecificData);
                    if (manufacturerAdvertisementData?.GetManufacturerId() == manufacturerId)
                    {
                        taskCompletionSource.SetResult(e);
                    }
                }

                try
                {
                    BgLib.BLEEventGAPScanResponse += OnScanResponse;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), useSynchronizationContext: false))
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
