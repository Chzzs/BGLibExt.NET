using Bluegiga;
using System;

namespace BGLibExt
{
    internal class BleProtocol : IReceivedDataParser
    {
        private readonly bool _debug;

        public BGLib Lib { get; private set; }

        public BleProtocol(bool debug)
        {
            _debug = debug;
            Lib = debug ? new BGLibDebug() : new BGLib();
        }

        public void Parse(byte data)
        {
            Lib.Parse(data);
        }

        public ushort SendCommand(System.IO.Ports.SerialPort port, byte[] command)
        {
            if (_debug)
            {
                Console.WriteLine($"Send to serial port, Data={command.ToHexString()}");
            }

            return Lib.SendCommand(port, command);
        }
    }
}
