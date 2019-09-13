using System.Collections.Generic;
using System.Linq;

namespace BGLibExt
{
    public static class BleAdvertisingDataParser
    {
        public static List<BleAdvertisingData> Parse(byte[] rawData)
        {
            var advertisingData = new List<BleAdvertisingData>();

            if (rawData.Length == 0)
            {
                return advertisingData;
            }

            var index = 0;
            do
            {
                var length = rawData[index];
                var type = rawData[index + 1];
                var data = rawData.Skip(index + 2).Take(length - 1).ToArray();
                advertisingData.Add(new BleAdvertisingData((BleAdvertisingDataType)type, data));
                index += length + 1;
            } while (index < rawData.Length);

            return advertisingData;
        }
    }
}
