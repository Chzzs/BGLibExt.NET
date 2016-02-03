using System;

namespace git.jedinja.monomyo.BleInfrastructure
{
	internal class BleConnectorInitData
	{
		public string PortName { get; private set; }
		public Bytes ServiceUUID  { get; private set; }
		public bool Debug  { get; private set; }

		public BleConnectorInitData (string portName, Bytes service, bool debug)
		{
			this.PortName = portName;
			this.ServiceUUID = service;
			this.Debug = debug;
		}
	}
}

