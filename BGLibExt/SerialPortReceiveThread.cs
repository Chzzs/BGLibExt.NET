using Bluegiga;
using System;
using System.IO.Ports;
using System.Threading;

namespace BGLibExt
{
    internal class SerialPortReceiveThread
    {
        private readonly BGLib _bgLib;
        private readonly SerialPort _serialPort;
        private Thread _receiveThread;
        private bool _stopThread;

        internal SerialPortReceiveThread(SerialPort serialPort, BGLib bgLib)
        {
            _serialPort = serialPort;
            _bgLib = bgLib;
        }

        public void Start()
        {
            if (_serialPort == null || !_serialPort.IsOpen)
            {
                throw new Exception("To start receiving data an open serial port is required");
            }

            _stopThread = false;
            _receiveThread = new Thread(Run);
            _receiveThread.Start();
        }

        public void Stop()
        {
            _stopThread = true;
            _receiveThread.Join();
        }

        private void Run()
        {
            var buffer = new byte[128]; // The serial port normally only yields 1 byte at a time
            while (!_stopThread && _serialPort != null && _serialPort.IsOpen)
            {
                var readBytes = _serialPort.Read(buffer, 0, buffer.Length);
                for (var i = 0; i < readBytes; i++)
                {
                    _bgLib.Parse(buffer[i]);
                }
            }
        }
    }
}
