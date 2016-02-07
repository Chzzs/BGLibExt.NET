using System;
using System.IO.Ports;
using git.jrowberg.bglib.Bluegiga.BLE.Events.GAP;
using git.jedinja.monomyo.BleInfrastructure.BleConnectorBlocks;
using git.jrowberg.bglib.Bluegiga.BLE.Events.Connection;
using git.jedinja.monomyo.BleInfrastructure.BleBackbone;
using git.jedinja.monomyo.BleInfrastructure.BleNotifications;
using git.jrowberg.bglib.Bluegiga.BLE.Events.ATTClient;
using System.Threading;

namespace git.jedinja.monomyo.BleInfrastructure
{
	internal class BleConnector
	{
		public BleConnectorInitData Config { get; private set; }

		private BleProtocol Ble { get; set; }

		private SerialPort Port { get; set; }

		private ReceiveFromPeripheralThread ReceiveThread { get; set; }

		public byte ConnectionHandle  { get; private set; }

		public BlePeripheralMap PeripheralMap { get; private set; }

		public BleConnector (BleConnectorInitData config)
		{
			this.Config = config;
		}

		public BlePeripheralMap Connect ()
		{
			// Init BgLib, open port, and start receiving thread
			this.InitTools ();

			BleDiscoverService discoverBlock = new BleDiscoverService (Ble, Port, Config.ServiceUUID);
			ScanResponseEventArgs found = discoverBlock.Execute ();

			BleConnectToService connectBlock = new BleConnectToService (Ble, Port, found.sender, found.address_type);
			this.ConnectionHandle = connectBlock.Execute ().connection;

			BleFindServicesAndCharacteristics infoBlock = new BleFindServicesAndCharacteristics (Ble, Port, this.ConnectionHandle);
			this.PeripheralMap = infoBlock.Execute ();

			this.InitializeCharacteristicNotifications ();

			this.Ble.Lib.BLEEventConnectionDisconnected += DisconnectedHandler;

			return this.PeripheralMap;
		}

		private void DisconnectedHandler (object sender, DisconnectedEventArgs e)
		{
			if (e.connection == this.ConnectionHandle)
			{
				this.Ble.Lib.BLEEventConnectionDisconnected -= DisconnectedHandler;
				Thread disconnectThread = new Thread (new ParameterizedThreadStart (DisconnectedHandlerAction));
				disconnectThread.Start (new object[] { this, e });
			}
		}

		private void DisconnectedHandlerAction (object parameters)
		{
			var pars = (object[]) parameters;
			this.OnDisconnected (pars[0], (DisconnectedEventArgs) pars[1]);
		}

		public event DisconnectedEventHandler Disconnected;

		protected void OnDisconnected (object sender, DisconnectedEventArgs e)
		{
			if (Disconnected != null)
			{
				Disconnected (sender, e);
			}
		}

		public void Disconnect ()
		{
			BleDisconnectFromService disconnectBlock = new BleDisconnectFromService (Ble, Port, this.ConnectionHandle);
			disconnectBlock.Execute ();

			this.DestructTools ();
		}

		#region Read characteristics

		public Bytes ReadCharacteristic (Bytes uuid)
		{
			ushort attrHandle = this.PeripheralMap.FindCharacteristicByUUID (uuid).Handle;

			return this.ReadAttribute (attrHandle);
		}

		public BleCCCValue ReadClientCharacteristicConfiguration (Bytes uuid)
		{
			BleCCCValue ccc;

			BlePeripheralCharacteristic characteristic = this.PeripheralMap.FindCharacteristicByUUID (uuid);

			if (!characteristic.HasCCC)
			{
				throw new ArgumentException ("Client characteristic {uuid} doesn't have a configuration attribute!");
			}

			ushort attrHandle = characteristic.HandleCCC;
			Bytes rawValue = this.ReadAttribute (attrHandle);

			using (ByteDeserializer bd = new ByteDeserializer (rawValue))
			{
				ccc = bd.DeSerializeBleCCValue ();
			}

			return ccc;
		}

		private Bytes ReadAttribute (ushort handle)
		{
			BleReadAttribute readAttribute = new BleReadAttribute (Ble, Port, this.ConnectionHandle, handle);

			return readAttribute.Execute ().value;
		}

		#endregion

		#region Write characteristics

		public void WriteCharacteristic (Bytes uuid, Bytes value)
		{
			ushort attrHandle = this.PeripheralMap.FindCharacteristicByUUID (uuid).Handle;

			this.WriteAttribute (attrHandle, value);
		}

		public void WriteClientCharacteristicConfiguration (Bytes uuid, BleCCCValue value)
		{
			BlePeripheralCharacteristic characteristic = this.PeripheralMap.FindCharacteristicByUUID (uuid);

			if (!characteristic.HasCCC)
			{
				throw new ArgumentException ($"Client characteristic {uuid} doesn't have a configuration attribute!");
			}

			ByteSerializer bs = new ByteSerializer ();
			bs.Serialize (value);

			ushort attrHandle = characteristic.HandleCCC;

			this.WriteAttribute (attrHandle, bs.GetBuffer ());
		}

		private void WriteAttribute (ushort handle, Bytes value)
		{
			BleWriteAttribute writeAttribute = new BleWriteAttribute (Ble, Port, this.ConnectionHandle, handle, value);

			writeAttribute.Execute ();
		}

		#endregion

		#region Characteristic notifications

		public event CharacteristicValueChangedEventHandler CharacteristicValueChanged;

		protected virtual void OnCharacteristicValueChanged (object sender, Bytes uuid, Bytes value)
		{
			if (this.CharacteristicValueChanged != null)
			{
				CharacteristicValueChangedEventArgs e = new CharacteristicValueChangedEventArgs (uuid, value);
				this.CharacteristicValueChanged (sender, e);
			}
		}
			
		private void InitializeCharacteristicNotifications ()
		{
			AttributeValueEventHandler handler = this.HandleAttributeValue;
			Ble.Lib.BLEEventATTClientAttributeValue += handler;
		}

		private void HandleAttributeValue (object sender, AttributeValueEventArgs e)
		{
			BlePeripheralCharacteristic characteristic;
			if (this.PeripheralMap.FindCharacteristicByHandle (e.atthandle, out characteristic))
			{
				OnCharacteristicValueChanged (this, characteristic.AttributeUUID, e.value);
			}
		}

		#endregion

		private void InitTools ()
		{
			Ble = new BleProtocol (this.Config.Debug);

			Port = new SerialPort (this.Config.PortName, 115200, Parity.None, 8, StopBits.One);
			Port.Handshake = Handshake.RequestToSend;
			Port.Open ();

			ReceiveThread = new ReceiveFromPeripheralThread (Port, Ble, this.Config.PortThreadSleep);
			ReceiveThread.Start ();
		}

		private void DestructTools ()
		{
			this.ReceiveThread.Stop ();

			Port.Close ();
			Port = null;

			Ble = null;
		}
	}
}