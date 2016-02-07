using System;
using System.IO.Ports;
using git.jrowberg.bglib.Bluegiga;
using System.Threading;

namespace git.jedinja.monomyo.BleInfrastructure
{
	internal class ReceiveFromPeripheralThread
	{
		public IReceivedDataParser BlueLib { get; private set; }

		public SerialPort Port { get; private set; }

		private bool StopThread { get; set; }

		protected int SleepTime { get; set; }

		public ReceiveFromPeripheralThread (SerialPort port, IReceivedDataParser blueLib, int sleepTime)
		{
			this.Port = port;
			this.BlueLib = blueLib;
			this.SleepTime = sleepTime;
		}

		private Thread ReceiveThread { get; set; }

		public void Start ()
		{
			if (Port == null || !Port.IsOpen)
			{
				throw new Exception ("In order to read from a peripheral an initialized and open serial port is required!");
			}

			this.StopThread = false;
			ReceiveThread = new Thread (this.Run);
			ReceiveThread.Start ();
		}

		public void Stop ()
		{
			this.StopThread = true;
			this.ReceiveThread.Join ();
		}

		private void Run ()
		{
			while (!StopThread && Port != null && Port.IsOpen)
			{
				if (Port.BytesToRead > 0)
				{
					this.ReceivedData ();
				}
				else if (this.SleepTime > 0)
				{
					Thread.Sleep (this.SleepTime);	
				}
			}
		}

		private void ReceivedData ()
		{
			int bytesToRead = Port.BytesToRead;

			Byte[] inData = new Byte[bytesToRead];

			Port.Read (inData, 0, bytesToRead);

			foreach (byte @byte in inData)
			{
				BlueLib.Parse (@byte);
			}
		}
	}
}

