using System.IO.Ports;

namespace BGLibExt
{
    public sealed class BleModuleConnection
    {
        private static readonly BleModuleConnection instance = new BleModuleConnection();

        private bool _isDebugMode;
        private string _portName;

        public static BleModuleConnection Instance { get { return instance; } }

        internal BleProtocol BleProtocol { get; private set; }
        internal SerialPortReceiveThread ReceiveFromPeripheralThread { get; private set; }
        internal SerialPort SerialPort { get; private set; }

        static BleModuleConnection()
        {
        }

        private BleModuleConnection()
        {
        }

        /// <summary>
        /// Start the BLE112 serial communication
        /// </summary>
        /// <param name="portName">Serial port name</param>
        public void Start(string portName)
        {
            Start(portName, false, 0);
        }

        /// <summary>
        ///  Start the BLE112 serial communication
        /// </summary>
        /// <param name="portName">Serial port name</param>
        /// <param name="isDebugMode">Print debug information to console</param>
        /// <param name="portThreadSleep">Sleep duration while reading serial port data</param>
        public void Start(string portName, bool isDebugMode, int portThreadSleep)
        {
            _portName = portName;
            _isDebugMode = isDebugMode;

            BleProtocol = new BleProtocol(_isDebugMode);

            SerialPort = new SerialPort(_portName, 115200, Parity.None, 8, StopBits.One)
            {
                Handshake = Handshake.RequestToSend
            };
            SerialPort.Open();

            ReceiveFromPeripheralThread = new SerialPortReceiveThread(SerialPort, BleProtocol, portThreadSleep);
            ReceiveFromPeripheralThread.Start();
        }

        /// <summary>
        /// Stop the BLE112 serial communication
        /// </summary>
        public void Stop()
        {
            ReceiveFromPeripheralThread.Stop();

            SerialPort.Close();
            SerialPort = null;

            BleProtocol = null;
        }
    }
}
