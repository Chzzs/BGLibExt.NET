using Bluegiga;
using System.IO.Ports;

namespace BGLibExt
{
    public sealed class BleModuleConnection
    {
        private readonly BGLib _bgLib;
        private SerialPortReceiveThread _serialPortReceiveThread;

        public SerialPort SerialPort { get; private set; }

        public BleModuleConnection(BGLib bgLib)
        {
            _bgLib = bgLib;
        }

        /// <summary>
        /// Start the BLE112 serial communication
        /// </summary>
        /// <param name="portName">Serial port name</param>
        public void Start(string portName)
        {
            Start(portName, 0);
        }

        /// <summary>
        ///  Start the BLE112 serial communication
        /// </summary>
        /// <param name="portName">Serial port name</param>
        /// <param name="portThreadSleep">Sleep duration while reading serial port data</param>
        public void Start(string portName, int portThreadSleep)
        {
            SerialPort = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One)
            {
                Handshake = Handshake.RequestToSend
            };
            SerialPort.Open();

            _serialPortReceiveThread = new SerialPortReceiveThread(SerialPort, _bgLib, portThreadSleep);
            _serialPortReceiveThread.Start();
        }

        /// <summary>
        /// Stop the BLE112 serial communication
        /// </summary>
        public void Stop()
        {
            _serialPortReceiveThread.Stop();

            SerialPort.Close();
            SerialPort.Dispose();
        }
    }
}
