using System;
using System.Linq;
using git.jedinja.monomyo.BleInfrastructure;
using git.jedinja.monomyo.MyoProtocol;
using git.jedinja.monomyo.BleInfrastructure.BleBackbone;
using System.Collections.Generic;
using git.jedinja.monomyo.SDK.Notifications;

namespace git.jedinja.monomyo.SDK
{
	public sealed class Myo : IDisposable
	{
		private BleConnector _ble;
		public MyoController Controller { get; private set; }
		public MyoNotifications Notifications { get; private set; }
		private readonly MyoConfig _config;

		private Myo (MyoConfig cfg)
		{
			_config = cfg ?? new MyoConfig ();
		}

		#region Connection and validation

		public static Myo ConnectFullControl (string port, MyoConfig cfg = null)
		{
			Myo myo = new Myo (cfg);
			myo.ConnectToDevice (port, NotificationSubscriptionMode.FullControl);
			return myo;
		}

		public static Myo ConnectEasyPreConfigured (string port, NotificationAutoConfigurableValues config, MyoConfig cfg = null)
		{
			Myo myo = new Myo (cfg);
			myo.ConnectToDevice (port, NotificationSubscriptionMode.EasyConfigurable, config);
			return myo;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="port">The port with the bluegiga ble adapter</param>
		/// <param name="notificationMode">Can't be changed after initialization!</param>
		/// <param name="config">Must be suplied when choosing EasyConfigurable notificatoin mode. Otherwise - ignored!</param>
		private void ConnectToDevice (string port, NotificationSubscriptionMode notificationMode, NotificationAutoConfigurableValues config = null)
		{
			// connect
			_ble = new BleConnector (new BleConnectorInitData (port, ProtocolServices.MYO_INFO_SERVICE, _config.Debug, _config.PortThreadSleep));
			_ble.Disconnected += DisconnectedHandler;
			BlePeripheralMap clientMap = _ble.Connect ();

			// init functionallity object
			this.Controller = new MyoController (_ble);

			// validate
			this.ValidateClientMap (clientMap);

			FirmwareVersion fv = this.Controller.GetFirmwareVersion ();
			if (fv.Major < ProtocolRevision.MAJOR_VERSION ||
			    (fv.Major == ProtocolRevision.MAJOR_VERSION && fv.Minor < ProtocolRevision.MINOR_VERSION))
			{
				throw new Exception ("Not supported protocol version. Please upgrade!");
			}

			// handle notification events
			this.InitializeNotificationSystem (notificationMode, config, _config.NotificationThreadSleep);
		}

		private void DisconnectedHandler (object sender, git.jrowberg.bglib.Bluegiga.BLE.Events.Connection.DisconnectedEventArgs e)
		{
			this.Dispose (true);
			this.OnDisconnected (this);
		}

		public event EventHandler Dosconnected;

		private void OnDisconnected (object sender)
		{
			if (Dosconnected != null)
			{
				Dosconnected (sender, new EventArgs ());
			}
		}

		private void ValidateClientMap (BlePeripheralMap map)
		{
			if (!ProtocolServices.ALL_SERVICES.All (service => map.Services.Any (mapService => mapService.ServiceUUID.Equals (service))))
			{
				throw new Exception ("Not all expected services found on the device!");
			}

			if (ProtocolServices.ALL_CHARACTERISTICS.Any (charac => map.FindCharacteristicByUUID (charac) == null))
			{
				throw new Exception ("Not all expected characteristics found on the device!");
			}
		}

		#endregion



		#region Notifications

		private void InitializeNotificationSystem (NotificationSubscriptionMode notificationMode, NotificationAutoConfigurableValues config, int threadSleep)
		{
			switch (notificationMode)
			{
			case NotificationSubscriptionMode.EasyConfigurable:
				{
					if (config == null)
					{
						throw new ArgumentNullException ("config");
					}

					this.Controller.IssueCommand_SetReceiveDataMode (config.Emg, config.Imu, config.Pose);

					this.Notifications = new MyoNotifications (_ble, this.Controller, threadSleep, true);
					break;
				}
			case NotificationSubscriptionMode.FullControl:
				{
					this.Notifications = new MyoNotifications (_ble, this.Controller, threadSleep);
					break;
				}
			}

			this.Notifications.StartModule ();
		}

		#endregion

		#region IDisposable implementation

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		private void Dispose (bool disposing)
		{
			if (disposing)
			{
				if (_ble != null)
				{
					this.Notifications.StopModule ();
					_ble.Disconnect ();
					_ble = null;
				}
			}
		}

		#endregion
	}
}

