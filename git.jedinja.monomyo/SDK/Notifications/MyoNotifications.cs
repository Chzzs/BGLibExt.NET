using System;
using git.jedinja.monomyo.BleInfrastructure;
using git.jedinja.monomyo.BleInfrastructure.BleNotifications;
using System.Collections.Concurrent;
using System.Threading;
using git.jedinja.monomyo.MyoProtocol;
using System.Collections.Generic;

namespace git.jedinja.monomyo.SDK.Notifications
{
	public class MyoNotifications
	{
		private readonly ConcurrentQueue<NotificationCarrier> _queue = new ConcurrentQueue<NotificationCarrier> ();

		private readonly BleConnector _ble;
		protected readonly MyoController _controller;

		private Dictionary<Bytes, Action<NotificationCarrier>> _notificationTypes;

		private bool StopThread { get; set; }
		private Thread _notificationExecutionThread;
		private readonly int _threadSleep = 0;

		private readonly bool _auto;

		internal MyoNotifications (BleConnector ble, MyoController controller, int sleep = 0, bool auto = false)
		{
			_ble = ble;
			_controller = controller;
			_auto = auto;
			_threadSleep = sleep;
		}

		#region Events

		private event BatteryChangeEventHandler _BatteryChange;
		public  event BatteryChangeEventHandler BatteryChange {
			add {
				bool changeState = !this.BatteryChangeHasHandlers ();

				_BatteryChange += value;

				if (_auto && changeState)
				{
					_controller.ChangeBatteryNotificationMode (true);
				}
			}

			remove {
				bool hasHandlers = this.BatteryChangeHasHandlers ();

				_BatteryChange -= value;

				if (_auto && hasHandlers != this.BatteryChangeHasHandlers ())
				{
					_controller.ChangeBatteryNotificationMode (false);
				}
			}
		}

		protected bool BatteryChangeHasHandlers ()
		{
			return _BatteryChange != null;
		}

		private void OnBatteryChange (object sender, BatteryLevel level, DateTime stamp)
		{
			if (this.BatteryChangeHasHandlers ())
			{
				_BatteryChange (sender, new BatteryChangeEventArgs (stamp, level));
			}
		}

		private event EmgDataReceivedEventHandler _EmgDataReceived;
		public event EmgDataReceivedEventHandler EmgDataReceived {
			add {
				bool changeState = !this.EmgDataReceivedHasHandlers ();

				_EmgDataReceived += value;

				if (_auto && changeState)
				{
					_controller.ChangeEmgDataNotificationMode (true);
				}
			}

			remove {
				bool hasHandlers = this.EmgDataReceivedHasHandlers ();

				_EmgDataReceived -= value;

				if (_auto && hasHandlers != this.EmgDataReceivedHasHandlers ())
				{
					_controller.ChangeEmgDataNotificationMode (false);
				}
			}
		}

		private bool EmgDataReceivedHasHandlers ()
		{
			return _EmgDataReceived != null;
		}

		protected void OnEmgDataReceived (object sender, byte[] data, byte sensorNumber, DateTime stamp)
		{
			if (this.EmgDataReceivedHasHandlers ())
			{
				_EmgDataReceived (sender, new EmgDataReceivedEventArgs (stamp, data, sensorNumber));
			}
		}

		private event ImuDataReceivedEventHandler _ImuDataReceived;
		public event ImuDataReceivedEventHandler ImuDataReceived {
			add {
				bool changeState = !this.ImuDataReceiveddHasHandlers ();

				_ImuDataReceived += value;

				if (_auto && changeState)
				{
					_controller.ChangeImuDataNotificationMode (true);
				}
			}

			remove {
				bool hasHandlers = this.ImuDataReceiveddHasHandlers ();

				_ImuDataReceived -= value;

				if (_auto && hasHandlers != this.ImuDataReceiveddHasHandlers ())
				{
					_controller.ChangeImuDataNotificationMode (false);
				}
			}
		}

		private bool ImuDataReceiveddHasHandlers ()
		{
			return _ImuDataReceived != null;
		}

		protected void OnImuDataReceived (object sender, short w, short x, short y, short z, short[] accelerometer, short[] gyroscope, DateTime stamp)
		{
			if (this.ImuDataReceiveddHasHandlers ())
			{
				_ImuDataReceived (sender, new ImuDataReceivedEventArgs (stamp, new ImuDataOrientation (w, x, y, z), accelerometer, gyroscope));
			}
		}

		private event TapDetectedEventHandler _TapDetected;
		public event TapDetectedEventHandler TapDetected {
			add {
				bool changeState = !this.TapDetectedHasHandlers ();

				_TapDetected += value;

				if (_auto && changeState)
				{
					_controller.ChangeMotionEventNotificationMode (true);
				}
			}

			remove {
				bool hasHandlers = this.TapDetectedHasHandlers ();

				_TapDetected -= value;

				if (_auto && hasHandlers != this.TapDetectedHasHandlers ())
				{
					_controller.ChangeMotionEventNotificationMode (false);
				}
			}
		}

		private bool TapDetectedHasHandlers ()
		{
			return _TapDetected != null;
		}

		protected void OnTapDetected (object sender, byte tapDirection, byte tapCount, DateTime stamp)
		{
			if (this.TapDetectedHasHandlers ())
			{
				_TapDetected (sender, new TapDetectedEventArgs (stamp, tapDirection, tapCount));
			}
		}

		private bool HasClassifierHandlers ()
		{
			return this.ArmSyncedHasHandlers () || this.ArmUnSyncedHasHandlers () || this.LockedHasHandlers () ||
			this.PoseChangedHasHandlers () || this.SyncFailedHasHandlers () || this.UnLockedHasHandlers ();
		}

		private event ArmSyncedEventHandler _ArmSynced;
		public event ArmSyncedEventHandler ArmSynced {
			add {
				bool changeState = !this.HasClassifierHandlers ();

				_ArmSynced += value;

				if (_auto && changeState)
				{
					_controller.ChangeClassifierNotificationMode (true);
				}
			}

			remove {
				bool hasHandlers = this.HasClassifierHandlers ();

				_ArmSynced -= value;

				if (_auto && hasHandlers != this.HasClassifierHandlers ())
				{
					_controller.ChangeClassifierNotificationMode (false);
				}
			}
		}

		private bool ArmSyncedHasHandlers ()
		{
			return _ArmSynced != null;
		}

		protected void OnArmSynced (object sender, Arm arm, BraceletDirection direction, DateTime stamp)
		{
			if (this.ArmSyncedHasHandlers ())
			{
				_ArmSynced (sender, new ArmSyncedEventArgs (stamp, arm, direction));
			}
		}

		private event NotificationEventHandler _ArmUnSynced;
		public event NotificationEventHandler ArmUnSynced {
			add {
				bool changeState = !this.HasClassifierHandlers ();

				_ArmUnSynced += value;

				if (_auto && changeState)
				{
					_controller.ChangeClassifierNotificationMode (true);
				}
			}

			remove {
				bool hasHandlers = this.HasClassifierHandlers ();

				_ArmUnSynced -= value;

				if (_auto && hasHandlers != this.HasClassifierHandlers ())
				{
					_controller.ChangeClassifierNotificationMode (false);
				}
			}
		}

		private bool ArmUnSyncedHasHandlers ()
		{
			return _ArmUnSynced != null;
		}

		protected void OnArmUnsynced (object sender, DateTime stamp)
		{
			if (this.ArmUnSyncedHasHandlers ())
			{
				_ArmUnSynced (sender, new NotificationEventArgs (stamp));
			}
		}

		private event NotificationEventHandler _SyncFailed;
		public event NotificationEventHandler SyncFailed {
			add {
				bool changeState = !this.HasClassifierHandlers ();

				_SyncFailed += value;

				if (_auto && changeState)
				{
					_controller.ChangeClassifierNotificationMode (true);
				}
			}

			remove {
				bool hasHandlers = this.HasClassifierHandlers ();

				_SyncFailed -= value;

				if (_auto && hasHandlers != this.HasClassifierHandlers ())
				{
					_controller.ChangeClassifierNotificationMode (false);
				}
			}
		}

		private bool SyncFailedHasHandlers ()
		{
			return _SyncFailed != null;
		}

		protected void OnSyncFailed (object sender, DateTime stamp)
		{
			if (this.SyncFailedHasHandlers ())
			{
				_SyncFailed (sender, new NotificationEventArgs (stamp));
			}
		}

		private event NotificationEventHandler _Locked;
		public event NotificationEventHandler Locked {
			add {
				bool changeState = !this.HasClassifierHandlers ();

				_Locked += value;

				if (_auto && changeState)
				{
					_controller.ChangeClassifierNotificationMode (true);
				}
			}

			remove {
				bool hasHandlers = this.HasClassifierHandlers ();

				_Locked -= value;

				if (_auto && hasHandlers != this.HasClassifierHandlers ())
				{
					_controller.ChangeClassifierNotificationMode (false);
				}
			}
		}

		private bool LockedHasHandlers ()
		{
			return _Locked != null;
		}

		protected void OnLocked (object sender, DateTime stamp)
		{
			if (this.LockedHasHandlers ())
			{
				_Locked (sender, new NotificationEventArgs (stamp));
			}
		}

		private event NotificationEventHandler _UnLocked;
		public  event NotificationEventHandler UnLocked {
			add {
				bool changeState = !this.HasClassifierHandlers ();

				_UnLocked += value;

				if (_auto && changeState)
				{
					_controller.ChangeClassifierNotificationMode (true);
				}
			}

			remove {
				bool hasHandlers = this.HasClassifierHandlers ();

				_UnLocked -= value;

				if (_auto && hasHandlers != this.HasClassifierHandlers ())
				{
					_controller.ChangeClassifierNotificationMode (false);
				}
			}
		}

		private bool UnLockedHasHandlers ()
		{
			return _UnLocked != null;
		}

		protected void OnUnLocked (object sender, DateTime stamp)
		{
			if (this.UnLockedHasHandlers ())
			{
				_UnLocked (sender, new NotificationEventArgs (stamp));
			}
		}

		private event PoseChangedEventHandler _PoseChanged;
		public event PoseChangedEventHandler PoseChanged {
			add {
				bool changeState = !this.HasClassifierHandlers ();

				_PoseChanged += value;

				if (_auto && changeState)
				{
					_controller.ChangeClassifierNotificationMode (true);
				}
			}

			remove {
				bool hasHandlers = this.HasClassifierHandlers ();

				_PoseChanged -= value;

				if (_auto && hasHandlers != this.HasClassifierHandlers ())
				{
					_controller.ChangeClassifierNotificationMode (false);
				}
			}
		}

		private bool PoseChangedHasHandlers ()
		{
			return _PoseChanged != null;
		}

		protected void OnPoseChanged (object sender, Pose pose, DateTime stamp)
		{
			if (this.PoseChangedHasHandlers ())
			{
				_PoseChanged (sender, new PoseChangedEventArgs (stamp, pose));
			}
		}

		#endregion Events

		#region Mechanism

		internal void StartModule ()
		{
			_notificationTypes = new Dictionary<Bytes, Action<NotificationCarrier>> () { 
				{ ProtocolServices._characteristicBatteryLevel, ProcessBatteryNotification },
				{ ProtocolServices._characteristicEmgData0, ProcessEmgDataNotification },
				{ ProtocolServices._characteristicEmgData1, ProcessEmgDataNotification },
				{ ProtocolServices._characteristicEmgData2, ProcessEmgDataNotification },
				{ ProtocolServices._characteristicEmgData3, ProcessEmgDataNotification },
				{ ProtocolServices._characteristicImuData, ProcessImuDataNotification },
				{ ProtocolServices._characteristicMotionEvent, ProcessMotionEventNotification },
				{ ProtocolServices._characteristicClasifierEvent, ProcessClassifierEventNotification }
			};

			_ble.CharacteristicValueChanged += CharacteristicValueChanged;

			this.StopThread = false;
			_notificationExecutionThread = new Thread (new ThreadStart (this.NotificationCycleRun));
			_notificationExecutionThread.Start ();
		}

		internal void StopModule ()
		{
			this.StopThread = true;

			_notificationExecutionThread.Join ();

			_ble.CharacteristicValueChanged -= CharacteristicValueChanged;
		}

		/// <summary>
		/// Invoked on another thread!!!
		/// </summary>
		private void CharacteristicValueChanged (object sender, CharacteristicValueChangedEventArgs e)
		{
			_queue.Enqueue (new NotificationCarrier (e.CharacteristicUUID, DateTime.UtcNow, e.Value));
		}

		private void NotificationCycleRun ()
		{
			while (!this.StopThread)
			{
				NotificationCarrier not;
				Action<NotificationCarrier> act;

				if (_queue.TryDequeue (out not) &&
				    _notificationTypes.TryGetValue (not.CharacteristicUUID, out act))
				{
					act (not);
				}

				if (_queue.Count == 0 && _threadSleep > 0)
				{
					Thread.Sleep (_threadSleep);
				}
			}
		}

		#endregion

		#region Parsers

		private void ProcessBatteryNotification (NotificationCarrier notification)
		{
			BatteryLevel level = new BatteryLevel (notification.CharacteristicValue);
			OnBatteryChange (this, level, notification.Timestamp);
		}

		private void ProcessEmgDataNotification (NotificationCarrier notification)
		{
			byte sensorNumber = (byte) (notification.CharacteristicUUID.ToArray ()[13] - 1);

			ProtocolEmgDataType emgData = new ProtocolEmgDataType ();
			emgData.DeSerialize (notification.CharacteristicValue);

			OnEmgDataReceived (this, emgData.EmgData.ToArray (), sensorNumber, notification.Timestamp);
		}

		private void ProcessImuDataNotification (NotificationCarrier notification)
		{
			ProtocolImuDataType imuData = new ProtocolImuDataType ();
			imuData.DeSerialize (notification.CharacteristicValue);

			OnImuDataReceived (this, imuData.OrientationW, imuData.OrientationX, imuData.OrientationY, imuData.OrientationZ,
				imuData.Accelerometer, imuData.Gyroscope, notification.Timestamp);
		}

		private void ProcessMotionEventNotification (NotificationCarrier notification)
		{
			ProtocolMotionEventType motionEvent = new ProtocolMotionEventType ();
			motionEvent.DeSerialize (notification.CharacteristicValue);

			switch (motionEvent.Event)
			{
			case ProtocolMotionEvent.Tap:
				{
					OnTapDetected (this, motionEvent.TapDirection, motionEvent.TapCount, notification.Timestamp);
					break;
				}
			}
		}

		private void ProcessClassifierEventNotification (NotificationCarrier notification)
		{
			ProtocolClassifierEventType classifierEvent = new ProtocolClassifierEventType ();
			classifierEvent.DeSerialize (notification.CharacteristicValue);

			switch (classifierEvent.Event)
			{
			case ProtocolClassifierEvent.ArmSynced:
				{
					OnArmSynced (this, (Arm) classifierEvent.Arm, (BraceletDirection) classifierEvent.Direction, notification.Timestamp);
					break;
				}
			case ProtocolClassifierEvent.ArmUnsynced:
				{
					OnArmUnsynced (this, notification.Timestamp);
					break;
				}
			case ProtocolClassifierEvent.SyncFailed:
				{
					OnSyncFailed (this, notification.Timestamp);
					break;
				}
			case ProtocolClassifierEvent.Locked:
				{
					OnLocked (this, notification.Timestamp);
					break;
				}
			case ProtocolClassifierEvent.Unlocked:
				{
					OnUnLocked (this, notification.Timestamp);
					break;
				}
			case ProtocolClassifierEvent.Pose:
				{
					OnPoseChanged (this, (Pose) classifierEvent.Pose, notification.Timestamp);
					break;
				}
			}
		}

		#endregion
	}
}

