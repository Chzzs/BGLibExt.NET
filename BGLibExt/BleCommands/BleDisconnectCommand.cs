using Bluegiga;
using System.IO.Ports;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleDisconnectCommand : BleCommand
    {
        public BleDisconnectCommand(BGLib bgLib, BleModuleConnection bleModuleConnection)
            : base(bgLib, bleModuleConnection)
        {
        }

        public Task ExecuteAsync(byte connection)
        {
            var disconnect = _bgLib.BLECommandConnectionDisconnect(connection);
            _bgLib.SendCommand(_bleModuleConnection.SerialPort, disconnect);

            return Task.FromResult(0);
        }
    }
}
