using System;
using System.IO.Ports;

namespace git.jedinja.monomyo.BleInfrastructure.BleConnectorBlocks
{
	internal abstract class BleBlock
	{
		private const int DEFAULT_TIMEOUT = 100;

		public BleProtocol Ble { get; private set; }
		public SerialPort Port  { get; private set; }

		protected BleBlock (BleProtocol ble, SerialPort port)
		{
			this.Ble = ble;
			this.Port = port;
		}

		protected void WaitEvent (Func<bool> predicate, int seconds = DEFAULT_TIMEOUT)
		{
			this.WaitEvent (predicate, new TimeSpan (0, 0, seconds));
		}

		protected void WaitEvent (Func<bool> predicate, TimeSpan timeout)
		{
			DateTime now = DateTime.Now;
			while (!predicate ())
			{
				if (DateTime.Now - now > timeout)
				{
					throw new TimeoutException ();
				}
				//System.Threading.Thread.Sleep (10);
			}
		}
	}
}

