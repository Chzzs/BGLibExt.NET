using System;

namespace git.jedinja.monomyo.SDK
{
	public class DeviceInformation
	{
		public string SerialNumber  { get; private set; }
		public MyoPose UnlockPose { get; private set; }
		public ClassifierType ActiveClassifierType  { get; private set; }
		public byte ActiveClassifierIndex  { get; private set; }
		public bool HasCustomClassifier  { get; private set; }
		public byte StreamIndicating  { get; private set; }
		public DeviceType Device  { get; private set; }

		public DeviceInformation (string serial, MyoPose pose, ClassifierType classifier, byte classifierIndex,
		                          bool customClassifier, byte streamIndicating, DeviceType device)
		{
			this.SerialNumber = serial;
			this.UnlockPose = pose;
			this.ActiveClassifierType = classifier;
			this.ActiveClassifierIndex = classifierIndex;
			this.HasCustomClassifier = customClassifier;
			this.StreamIndicating = streamIndicating;
			this.Device = device;
		}
	}
}

