using System.IO.Ports;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleDisconnectCommand : BleCommand
    {
        public BleDisconnectCommand()
            : base(BleModuleConnection.Instance.BleProtocol, BleModuleConnection.Instance.SerialPort)
        {
        }

        public BleDisconnectCommand(BleProtocol ble, SerialPort port)
            : base(ble, port)
        {
        }

        public Task ExecuteAsync(byte connection)
        {
            var disconnect = Ble.Lib.BLECommandConnectionDisconnect(connection);
            Ble.SendCommand(Port, disconnect);

            return Task.FromResult(0);
        }
    }
}
