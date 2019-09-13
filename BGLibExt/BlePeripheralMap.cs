using System.Collections.Generic;

namespace BGLibExt
{
    public class BlePeripheralMap
    {
        private Dictionary<string, BlePeripheralCharacteristic> _characteristicsMap;
        private Dictionary<ushort, BlePeripheralCharacteristic> _handleMap;

        public List<BlePeripheralService> Services { get; private set; }

        private Dictionary<string, BlePeripheralCharacteristic> CharacteristicMap
        {
            get
            {
                if (_characteristicsMap == null)
                {
                    _characteristicsMap = new Dictionary<string, BlePeripheralCharacteristic>();

                    Services.ForEach(x => x.Characteristics.ForEach(characteristic =>
                        _characteristicsMap.Add(characteristic.AttributeUuid.ByteArrayToHexString(), characteristic)));
                }
                return _characteristicsMap;
            }
        }

        private Dictionary<ushort, BlePeripheralCharacteristic> HandleMap
        {
            get
            {
                if (_handleMap == null)
                {
                    _handleMap = new Dictionary<ushort, BlePeripheralCharacteristic>();

                    Services.ForEach(x => x.Characteristics.ForEach(characteristic =>
                        _handleMap.Add(characteristic.Handle, characteristic)));
                }
                return _handleMap;
            }
        }

        public BlePeripheralMap()
        {
            Services = new List<BlePeripheralService>();
        }

        public bool FindCharacteristicByHandle(ushort handle, out BlePeripheralCharacteristic characteristic)
        {
            return HandleMap.TryGetValue(handle, out characteristic);
        }

        public BlePeripheralCharacteristic FindCharacteristicByUuid(byte[] uuid)
        {
            return CharacteristicMap[uuid.ByteArrayToHexString()];
        }
    }
}
