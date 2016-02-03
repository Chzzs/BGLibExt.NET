using System;
using git.jedinja.monomyo.MyoProtocol;
using git.jedinja.monomyo.BleInfrastructure;
using git.jedinja.monomyo.BleInfrastructure.BleBackbone;
using System.Collections.Generic;

namespace git.jedinja.monomyo.SDK
{
	public class MyoController
	{
		private BleConnector _ble;

		internal MyoController (BleConnector ble)
		{
			_ble = ble;
		}

		// FIRMWARE VERSION
		private ProtocolVersionType _firmwareVersion;
		public FirmwareVersion GetFirmwareVersion ()
		{
			if (_firmwareVersion == null)
			{
				Bytes rawValue = _ble.ReadCharacteristic (ProtocolServices._characteristicFirmwareVersion);

				_firmwareVersion = new ProtocolVersionType ();
				_firmwareVersion.DeSerialize (rawValue);
			}

			return new FirmwareVersion (
				_firmwareVersion.Major, 
				_firmwareVersion.Minor, 
				_firmwareVersion.Patch,
				(HardwareRevision) _firmwareVersion.HardwareRevision);
		}


		// DEVICE NAME
		private string _deviceName;
		public string GetDeviceName ()
		{
			if (_deviceName == null)
			{
				Bytes rawValue = _ble.ReadCharacteristic (ProtocolServices._characteristicDeviceName);
				_deviceName = rawValue.ToText ();
			}

			return _deviceName;
		}

		public void ChangeDeviceName (string name)
		{
			if (string.IsNullOrEmpty (name))
			{
				throw new ArgumentNullException ("name");
			}

			if (name != _deviceName)
			{
				byte[] b = new byte[name.Length];
				for (int i = 0; i < b.Length; i++)
				{
					b[i] = (byte) name[i];
				}

				_ble.WriteCharacteristic (ProtocolServices._characteristicDeviceName, b);
				_deviceName = null; // refresh name
			}
		}


		// BATTERY LEVEL
		public BatteryLevel GetBatteryLevel ()
		{
			Bytes rawValue = _ble.ReadCharacteristic (ProtocolServices._characteristicBatteryLevel);
			return new BatteryLevel (rawValue);
		}

		public bool AreBatteryNotificationsEnabled ()
		{
			BleCCCValue value = _ble.ReadClientCharacteristicConfiguration (ProtocolServices._characteristicBatteryLevel);
			return ((value & BleCCCValue.NotificationsEnabled) == BleCCCValue.NotificationsEnabled);
		}

		public void ChangeBatteryNotificationMode (bool enabled)
		{
			_ble.WriteClientCharacteristicConfiguration (ProtocolServices._characteristicBatteryLevel, 
				enabled ? BleCCCValue.NotificationsEnabled : BleCCCValue.None);
		}


		// MYO INFO
		private ProtocolInfoType _deviceInfo;
		public DeviceInformation GetDeviceInformation ()
		{
			if (_deviceInfo == null)
			{
				Bytes rawValue = _ble.ReadCharacteristic (ProtocolServices._characteristicMyoInfo);

				_deviceInfo = new ProtocolInfoType ();
				_deviceInfo.DeSerialize (rawValue);
			}

			return new DeviceInformation (
				_deviceInfo.SerialNumber.ToString (), 
				(MyoPose) _deviceInfo.UnlockPose,
				(ClassifierType) _deviceInfo.ActiveClassifierType,
				_deviceInfo.ActiveClassifierIndex,
				_deviceInfo.HasCustomClassifier,
				_deviceInfo.StreamIndicating,
				(DeviceType) _deviceInfo.Sku
			);
		}


		// EMG Data
		public void ChangeEmgDataNotificationMode (bool enabled)
		{
			BleCCCValue val = enabled ? BleCCCValue.NotificationsEnabled : BleCCCValue.None;

			_ble.WriteClientCharacteristicConfiguration (ProtocolServices._characteristicEmgData0, val);

			_ble.WriteClientCharacteristicConfiguration (ProtocolServices._characteristicEmgData1, val);

			_ble.WriteClientCharacteristicConfiguration (ProtocolServices._characteristicEmgData2, val);

			_ble.WriteClientCharacteristicConfiguration (ProtocolServices._characteristicEmgData3, val);
		}


		// IMU Data
		public void ChangeImuDataNotificationMode (bool enabled)
		{
			_ble.WriteClientCharacteristicConfiguration (ProtocolServices._characteristicImuData,
				enabled ? BleCCCValue.NotificationsEnabled : BleCCCValue.None);
		}
        
		public void ChangeMotionEventNotificationMode (bool enabled)
		{
			_ble.WriteClientCharacteristicConfiguration (ProtocolServices._characteristicMotionEvent,
				enabled ? BleCCCValue.IndicationsEnabled : BleCCCValue.None);
		}


		// CLASSIFIER DATA
		public void ChangeClassifierNotificationMode (bool enabled)
		{
			_ble.WriteClientCharacteristicConfiguration (ProtocolServices._characteristicClasifierEvent,
				enabled ? BleCCCValue.IndicationsEnabled : BleCCCValue.None);
		}


		// COMMANDS
		private void IssueCommand (ProtocolCommandType command)
		{
			_ble.WriteCharacteristic (ProtocolServices._characteristicCommand, command.Serialize ());
		}

		public void IssueCommand_FallInDeepSleep ()
		{
			this.IssueCommand (new ProtocolCommandDeepSleepType ());
		}

		/// <summary>
		/// Avoid using this method more than once after connection as it cause desync. 
		/// [ User has to perform sync gesture once again. ]
		/// If all three values are set to None then the armband quickly falls asleep!
		/// </summary>
		public void IssueCommand_SetReceiveDataMode (EmgMode emg = EmgMode.None, ImuMode imu = ImuMode.None, 
		                                             MyoPoseMode pose = MyoPoseMode.Disabled)
		{
			this.IssueCommand (new ProtocolCommandSetModeType (
				(ProtocolEmgMode) emg, (ProtocolImuMode) imu, (ProtocolClassifierMode) pose));
		}

		public void IssueCommand_SetSleepMode (SleepMode sleep)
		{
			this.IssueCommand (new ProtocolCommandSetSleepModeType ((ProtocolSleepMode) sleep));
		}

		public void IssueCommand_Lock ()
		{
			this.IssueCommand (new ProtocolCommandUnlockType (ProtocolUnlockMode.Lock));
		}

		public void IssueCommand_Unlock (UnlockType unlock)
		{
			this.IssueCommand (new ProtocolCommandUnlockType ((ProtocolUnlockMode) unlock));
		}

		public void IssueCommand_NotifyUserAction ()
		{
			this.IssueCommand (new ProtocolCommandUserActionType (ProtocolUserAction.Single));
		}

		public void IssueCommand_Vibrate (VibrationType vibration)
		{
			this.IssueCommand (new ProtocolCommandVibrateType ((ProtocolVibration) vibration));
		}

		/// <summary>
		/// Ignores steps which are more than the defined in the protocol.
		/// As of 10th January 2016 - six vibration steps
		/// </summary>
		public void IssueCommand_CustomVibrate (List<CustomVibration> steps)
		{
			if (steps != null && steps.Count > 0)
			{
				ProtocolCommandVibrate2Type cmd = new ProtocolCommandVibrate2Type ();

				for (int i = 0; i < Math.Min (steps.Count, ProtocolCommandVibrate2Type.VIBRATION_STEPS_COUNT); i++)
				{
					cmd.Steps[i].Duration = steps[i].Length;
					cmd.Steps[i].Strength = steps[i].Power;
				}

				this.IssueCommand (cmd);
			}
		}
	}
}

