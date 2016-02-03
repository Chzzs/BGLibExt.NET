using System;
using System.Collections.Generic;
using git.jedinja.monomyo.SDK;
using git.jedinja.monomyo.SDK.Notifications;

namespace ConsoleMyo
{
	class MainClass
	{
		private static Myo _myo;
		private static MyoController _contr;

		public static void Connect ()
		{
			Console.WriteLine ("Connecting. Please wait!");
			_myo = Myo.ConnectEasyPreConfigured ("/dev/ttyACM0", NotificationAutoConfigurableValues.All, true);
			_contr = _myo.Controller;
			Console.WriteLine ("Connected");
		}

		public static void Main (string[] args)
		{
			Connect ();

			_myo.Dosconnected += Disconnected;

			using (_myo)
			{
				while (true)
				{
					Console.Write ("> ");
					string command = Console.ReadLine ();

					ProcessInput (command.Trim ());
				}
			}
		}

		static void Disconnected (object sender, EventArgs e)
		{
			Console.WriteLine ($"Myo was disconnected!");
			Connect ();
		}

		private static void ProcessInput (string command)
		{
			Action act;
			if (_commands.TryGetValue (command, out act))
			{
				act ();
			}
			else
			{
				Console.WriteLine ($"Command {command} not recognised! Type help for list of available commands");
			}
		}

		private static readonly Dictionary<string, Action> _commands = new Dictionary<string, Action> (StringComparer.OrdinalIgnoreCase) { 
			{ "help", Help },
			{ "vib-s", VibrateS },
			{ "vib-m", VibrateM },
			{ "vib-l", VibrateL },
			{ "exit", Exit },
			{ "battery", BatteryLevel },
			{ "fware", Firmware },
			{ "info", Info },
			{ "name", Name },
			{ "emg", ToggleEmg },
			{ "imu", ToggleImu },
			{ "pose", TogglePose }
		};

		private static bool pose;
		public static void TogglePose ()
		{
			if (!pose)
			{
				pose = true;
				_myo.Notifications.PoseChanged += _myo_Notifications_PoseChanged;
			}
			else
			{
				pose = false;
				_myo.Notifications.PoseChanged -= _myo_Notifications_PoseChanged;
			}
		}

		static void _myo_Notifications_PoseChanged (object sender, PoseChangedEventArgs e)
		{
			Console.WriteLine ($"{e.NewPose}");
		}

		private static bool imu;
		public static void ToggleImu ()
		{
			if (!imu)
			{
				_myo.Notifications.ImuDataReceived += _myo_Notifications_ImuDataReceived;
				imu = true;
			}
			else
			{
				_myo.Notifications.ImuDataReceived -= _myo_Notifications_ImuDataReceived;
				imu = false;
			}
		}

		private static int imu_not;
		static void _myo_Notifications_ImuDataReceived (object sender, ImuDataReceivedEventArgs e)
		{
			imu_not++;
			if (imu_not % 100 == 0)
			{
				Console.WriteLine ($"Imu data count reached {imu_not}");
			}
		}

		private static bool emg;
		public static void ToggleEmg ()
		{
			if (!emg)
			{
				_myo.Notifications.EmgDataReceived += _myo_Notifications_EmgDataReceived;
				emg = true;
			}
			else
			{
				_myo.Notifications.EmgDataReceived -= _myo_Notifications_EmgDataReceived;
				emg = false;
			}
		}

		private static int emg_not;
		static void _myo_Notifications_EmgDataReceived (object sender, EmgDataReceivedEventArgs e)
		{
			emg_not++;
			if (emg_not % 100 == 0)
			{
				Console.WriteLine ($"Emg data count reached {emg_not}");
			}
		}
		public static void Name ()
		{
			string name = _contr.GetDeviceName ();
			Console.WriteLine ($"{name}");
		}
		public static void Info ()
		{
			var info = _contr.GetDeviceInformation ();
			Console.WriteLine ($"{info.Device} {info.SerialNumber} {info.UnlockPose}");
		}
		public static void Firmware ()
		{
			var fw = _contr.GetFirmwareVersion ();
			Console.WriteLine ($"{fw.Major}.{fw.Minor}.{fw.Patch}.{fw.Revision}");
		}
		public static void BatteryLevel ()
		{
			var bl = _contr.GetBatteryLevel ();
			Console.WriteLine ($"Battery level is {bl.Value}%");
		}

		public static void Exit ()
		{
			_myo.Dispose ();
		}
		public static void VibrateS ()
		{
			_contr.IssueCommand_Vibrate (VibrationType.Short);
		}

		public static void VibrateM ()
		{
			_contr.IssueCommand_Vibrate (VibrationType.Medium);
		}

		public static void VibrateL ()
		{
			_contr.IssueCommand_Vibrate (VibrationType.Long);
		}

		public static void Help ()
		{
			Console.WriteLine ("List of available commands: ");
			foreach (string key in _commands.Keys)
			{
				Console.WriteLine (key);
			}
		}
	}
}
