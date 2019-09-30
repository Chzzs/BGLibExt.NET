using Bluegiga.BLE.Events.GAP;
using System.IO.Ports;
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

        public BleDiscoverServiceCommand()
            : base(BleModuleConnection.Instance.BleProtocol, BleModuleConnection.Instance.SerialPort)
        {
        }

        public BleDiscoverServiceCommand(BleProtocol ble, SerialPort port)
            : base(ble, port)
        {
        }

        public async Task<ScanResponseEventArgs> ExecuteAsync(byte[] serviceUuid, int timeout = DefaultTimeout)
        {
            return await ExecuteAsync(serviceUuid, CancellationToken.None, timeout);
        }

        public async Task<ScanResponseEventArgs> ExecuteAsync(byte[] serviceUuid, CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
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
