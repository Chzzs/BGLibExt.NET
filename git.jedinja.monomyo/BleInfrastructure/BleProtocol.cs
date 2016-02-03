using System;
using git.jrowberg.bglib.Bluegiga;
using git.jrowberg.bglib.Bluegiga.BLE;

namespace git.jedinja.monomyo.BleInfrastructure
{
	internal interface IReceivedDataParser
	{
		void Parse (byte data);
	}

	internal class BleProtocol : IReceivedDataParser
	{
		public BGLib Lib { get; private set; }

		public BleProtocol (bool debug)
		{
			Lib = debug ? new BGLibDebug () : new BGLib ();
		}

		public ushort SendCommand (System.IO.Ports.SerialPort port, byte[] command)
		{
			return Lib.SendCommand (port, command);
		}

		#region IReceivedDataParser implementation

		public void Parse (byte data)
		{
			this.Lib.Parse (data);
		}

		#endregion
	}
}

