using Bluegiga;

namespace BGLibExt
{
    internal class BleProtocol : IReceivedDataParser
    {
        public BGLib Lib { get; private set; }

        public BleProtocol(bool debug)
        {
            Lib = debug ? new BGLibDebug() : new BGLib();
        }

        public void Parse(byte data)
        {
            Lib.Parse(data);
        }

        public ushort SendCommand(System.IO.Ports.SerialPort port, byte[] command)
        {
            return Lib.SendCommand(port, command);
        }
    }
}
