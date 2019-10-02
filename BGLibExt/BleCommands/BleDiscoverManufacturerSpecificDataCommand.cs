using Bluegiga;
using Bluegiga.BLE.Events.GAP;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleDiscoverManufacturerSpecificDataCommand : BleCommand
    {
        public BleDiscoverManufacturerSpecificDataCommand(BGLib bgLib, BleModuleConnection bleModuleConnection)
            : base(bgLib, bleModuleConnection)
        {
        }

        public async Task<ScanResponseEventArgs> ExecuteAsync(ushort manufacturerId, int timeout = DefaultTimeout)
        {
            return await ExecuteAsync(manufacturerId, CancellationToken.None, timeout);
        }

        public async Task<ScanResponseEventArgs> ExecuteAsync(ushort manufacturerId, CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
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
                    _bgLib.BLEEventGAPScanResponse += OnScanResponse;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), useSynchronizationContext: false))
                    {
                        _bgLib.SendCommand(_bleModuleConnection.SerialPort, _bgLib.BLECommandGAPSetScanParameters(0xC8, 0xC8, 1));
                        _bgLib.SendCommand(_bleModuleConnection.SerialPort, _bgLib.BLECommandGAPDiscover(1));

                        return await taskCompletionSource.Task.ConfigureAwait(false);
                    }
                }
                finally
                {
                    _bgLib.SendCommand(_bleModuleConnection.SerialPort, _bgLib.BLECommandGAPEndProcedure());

                    _bgLib.BLEEventGAPScanResponse -= OnScanResponse;
                }
            }
        }
    }
}
