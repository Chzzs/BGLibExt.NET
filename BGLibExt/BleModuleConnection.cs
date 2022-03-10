using Bluegiga;
using Microsoft.Extensions.Logging;
using System;
using System.IO.Ports;

namespace BGLibExt
{
    public sealed class BleModuleConnection
    {
        private readonly BGLib _bgLib;
        private readonly ILogger<BleModuleConnection> _logger;
        private SerialPortReceiveThread _serialPortReceiveThread;

        public SerialPort SerialPort { get; private set; }

        public BleModuleConnection(BGLib bgLib, ILogger<BleModuleConnection> logger = null)
        {
            _bgLib = bgLib;
            _logger = logger;
        }

        /// <summary>
        /// Start the BLE112 serial communication
        /// </summary>
        /// <param name="portName">Serial port name</param>
        /// <param name="portThreadSleep">Sleep duration while reading serial port data</param>
        [Obsolete("The port thread sleep parameter has no effect anymore and this constructor will be removed in future releases")]
        public void Start(string portName, int portThreadSleep)
        {
            Start(portName);
        }

        /// <summary>
        ///  Start the BLE112 serial communication
        /// </summary>
        /// <param name="portName">Serial port name</param>
        /// <param name="portThreadSleep">Sleep duration while reading serial port data</param>
        public void Start(string portName)
        {
            _logger?.LogDebug($"Start BLE112 module connection");

            SerialPort = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One)
            {
                Handshake = Handshake.RequestToSend
            };
            _logger?.LogDebug($"Open serial port, Port={SerialPort.PortName}, BaudRate={SerialPort.BaudRate}, Parity={SerialPort.Parity}, DataBits={SerialPort.DataBits}, StopBits={SerialPort.StopBits}");
            SerialPort.Open();

            _serialPortReceiveThread = new SerialPortReceiveThread(SerialPort, _bgLib);
            _logger?.LogDebug($"Start serial port receive thread");
            _serialPortReceiveThread.Start();
        }

        /// <summary>
        /// Stop the BLE112 serial communication
        /// </summary>
        public void Stop()
        {
            _logger?.LogDebug($"Stop BLE112 module connection");

            _logger?.LogDebug($"Stop serial port receive thread");
            _serialPortReceiveThread.StopThread();

            _logger?.LogDebug($"Close and dispose serial port, Port={SerialPort.PortName}");
            SerialPort.Close();
            SerialPort.Dispose();

            _logger?.LogDebug($"Wait for serial port receive thread to stop");
            _serialPortReceiveThread.WaitForThreadToStop();
        }
    }
}
