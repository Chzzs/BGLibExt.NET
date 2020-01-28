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
        private readonly int _sleepTime;
        private Thread _receiveThread;
        private bool _stopThread;

        internal SerialPortReceiveThread(SerialPort serialPort, BGLib bgLib, int sleepTime)
        {
            _serialPort = serialPort;
            _bgLib = bgLib;
            _sleepTime = sleepTime;
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

        private void ReceivedData()
        {
            var bytesToRead = _serialPort.BytesToRead;

            var readBytes = new byte[bytesToRead];

            _serialPort.Read(readBytes, 0, bytesToRead);

            foreach (var readByte in readBytes)
            {
                _bgLib.Parse(readByte);
            }
        }

        private void Run()
        {
            while (!_stopThread && _serialPort != null && _serialPort.IsOpen)
            {
                if (_serialPort.BytesToRead > 0)
                {
                    ReceivedData();
                }
                else if (_sleepTime > 0)
                {
                    Thread.Sleep(_sleepTime);
                }
            }
        }
    }
}
