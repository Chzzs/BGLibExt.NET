using System;
using System.Collections.Generic;
using System.Linq;

namespace git.jedinja.monomyo.BleInfrastructure.BleBackbone
{
	internal class BlePeripheralMap
	{
		public List<BlePeripheralService> Services  { get; private set; }

		public BlePeripheralMap ()
		{
			this.Services = new List<BlePeripheralService> ();
		}

		public BlePeripheralCharacteristic FindCharacteristicByUUID (Bytes uuid)
		{
			return CharacteristicMap[uuid];
		}

		private Dictionary<Bytes, BlePeripheralCharacteristic> _characteristicsMap;
		private Dictionary<Bytes, BlePeripheralCharacteristic> CharacteristicMap {
			get {
				if (_characteristicsMap == null)
				{
					_characteristicsMap = new Dictionary<Bytes, BlePeripheralCharacteristic> ();

					this.Services.ForEach (serv => serv.Characteristics.ForEach (characteristic => 
						_characteristicsMap.Add (characteristic.AttributeUUID, characteristic)));
				}
				return _characteristicsMap;
			}
		}

		public bool FindCharacteristicByHandle (ushort handle, out BlePeripheralCharacteristic characteristic)
		{
            return HandleMap.TryGetValue(handle, out characteristic);
		}

		private Dictionary<ushort, BlePeripheralCharacteristic> _handleMap;
		private Dictionary<ushort, BlePeripheralCharacteristic> HandleMap {
			get {
				if (_handleMap == null)
				{
					_handleMap = new Dictionary<ushort, BlePeripheralCharacteristic> ();

					this.Services.ForEach (serv => serv.Characteristics.ForEach (characteristic => 
						_handleMap.Add (characteristic.Handle, characteristic)));
				}
				return _handleMap;
			}
		}
	}
}

