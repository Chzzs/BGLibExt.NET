using Bluegiga.BLE.Events.GAP;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleDiscoverManufacturerSpecificDataCommand : BleCommand
    {
        public BleDiscoverManufacturerSpecificDataCommand()
            : base(BleModuleConnection.Instance.BleProtocol, BleModuleConnection.Instance.SerialPort)
        {
        }

        public BleDiscoverManufacturerSpecificDataCommand(BleProtocol ble, SerialPort port)
            : base(ble, port)
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
                    Ble.Lib.BLEEventGAPScanResponse += OnScanResponse;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), useSynchronizationContext: false))
                    {
                        Ble.SendCommand(Port, Ble.Lib.BLECommandGAPSetScanParameters(0xC8, 0xC8, 1));
                        Ble.SendCommand(Port, Ble.Lib.BLECommandGAPDiscover(1));

                        return await taskCompletionSource.Task.ConfigureAwait(continueOnCapturedContext: false);
                    }
                }
                finally
                {
                    Ble.SendCommand(Port, Ble.Lib.BLECommandGAPEndProcedure());

                    Ble.Lib.BLEEventGAPScanResponse -= OnScanResponse;
                }
            }
        }
    }
}
