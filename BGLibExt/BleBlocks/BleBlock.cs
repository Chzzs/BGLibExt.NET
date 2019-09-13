using System;
using System.IO.Ports;

namespace BGLibExt.BleBlocks
{
    internal abstract class BleBlock
    {
        private const int DefaultTimeout = 100;

        public BleProtocol Ble { get; private set; }
        public SerialPort Port { get; private set; }

        protected BleBlock(BleProtocol ble, SerialPort port)
        {
            Ble = ble;
            Port = port;
        }

        protected void WaitEvent(Func<bool> predicate, int seconds = DefaultTimeout)
        {
            WaitEvent(predicate, new TimeSpan(0, 0, seconds));
        }

        protected void WaitEvent(Func<bool> predicate, TimeSpan timeout)
        {
            var now = DateTime.Now;
            while (!predicate())
            {
                if (DateTime.Now - now > timeout)
                {
                    throw new TimeoutException();
                }
                //System.Threading.Thread.Sleep (10);
            }
        }
    }
}
