using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.Connection
{
	public delegate void FeatureIndEventHandler (object sender, FeatureIndEventArgs e);

	public class FeatureIndEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly Byte[] features;

		public FeatureIndEventArgs (Byte connection, Byte[] features)
		{
			this.connection = connection;
			this.features = features;
		}
	}
}
