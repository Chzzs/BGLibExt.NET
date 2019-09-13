using System;
using System.IO.Ports;
using System.Threading;

namespace BGLibExt
{
    internal class ReceiveFromPeripheralThread
    {
        private readonly IReceivedDataParser _blueLib;
        private readonly SerialPort _serialPort;
        private readonly int _sleepTime;
        private Thread _receiveThread;
        private bool _stopThread;

        public ReceiveFromPeripheralThread(SerialPort port, IReceivedDataParser blueLib, int sleepTime)
        {
            _serialPort = port;
            _blueLib = blueLib;
            _sleepTime = sleepTime;
        }

        public void Start()
        {
            if (_serialPort == null || !_serialPort.IsOpen)
            {
                throw new Exception("In order to read from a peripheral an initialized and open serial port is required!");
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

            var inData = new byte[bytesToRead];

            _serialPort.Read(inData, 0, bytesToRead);

            foreach (var @byte in inData)
            {
                _blueLib.Parse(@byte);
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
