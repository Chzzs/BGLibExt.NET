using System;
using System.IO.Ports;

namespace BGLibExt.BleCommands
{
    internal abstract class BleCommand
    {
        protected const int DefaultTimeout = 10000;

        protected BleProtocol Ble { get; private set; }
        protected SerialPort Port { get; private set; }

        protected BleCommand(BleProtocol ble, SerialPort port)
        {
            Ble = ble;
            Port = port;
        }
    }
}
