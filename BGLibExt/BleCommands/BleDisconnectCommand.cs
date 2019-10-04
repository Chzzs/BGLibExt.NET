using Bluegiga;
using Microsoft.Extensions.Logging;
using System.IO.Ports;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleDisconnectCommand : BleCommand
    {
        public BleDisconnectCommand(BGLib bgLib, BleModuleConnection bleModuleConnection, ILogger logger)
            : base(bgLib, bleModuleConnection, logger)
        {
        }

        public Task ExecuteAsync(byte connection)
        {
            Logger?.LogDebug($"Disconnect from device, Connection={connection}");

            var disconnect = BgLib.BLECommandConnectionDisconnect(connection);
            BgLib.SendCommand(BleModuleConnection.SerialPort, disconnect);

            return Task.FromResult(0);
        }
    }
}
