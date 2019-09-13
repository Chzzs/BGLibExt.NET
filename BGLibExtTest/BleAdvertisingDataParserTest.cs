using BGLibExt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BGLibExtTest
{
    [TestClass]
    public class BleAdvertisingDataParserTest
    {
        /// <summary>
        /// Test vector from https://www.silabs.com/community/wireless/bluetooth/knowledge-base.entry.html/2017/02/10/bluetooth_advertisin-hGsf
        /// </summary>
        [TestMethod]
        public void Parse_TestVector1_AdvertisingDataParsedCorrectly()
        {
            // Arrange
            var bytes = "020106030309181409546865726d6f6d65746572204578616d706c65".HexStringToByteArray();

            // Act
            var advertisingData = BleAdvertisingDataParser.Parse(bytes);

            // Assert
            Assert.AreEqual(3, advertisingData.Count);
            Assert.AreEqual(BleAdvertisingDataType.Flags, advertisingData[0].Type);
            Assert.AreEqual(0x06, advertisingData[0].ToUint8());
            Assert.AreEqual(BleAdvertisingDataType.CompleteListof16BitServiceClassUUIDs, advertisingData[1].Type);
            Assert.AreEqual((ushort)0x1809, advertisingData[1].ToUint16());
            Assert.AreEqual(BleAdvertisingDataType.CompleteLocalName, advertisingData[2].Type);
            Assert.AreEqual("Thermometer Example", advertisingData[2].ToAsciiString());
        }

        /// <summary>
        /// Test vector from https://www.silabs.com/community/wireless/bluetooth/knowledge-base.entry.html/2017/02/10/bluetooth_advertisin-hGsf
        /// </summary>
        [TestMethod]
        public void Parse_TestVector2_AdvertisingDataParsedCorrectly()
        {
            // Arrange
            var bytes = "020106110707b9f9d750a420897740cbfd2cc18048090842474d3131312053".HexStringToByteArray();

            // Act
            var advertisingData = BleAdvertisingDataParser.Parse(bytes);

            // Assert
            Assert.AreEqual(3, advertisingData.Count);
            Assert.AreEqual(BleAdvertisingDataType.Flags, advertisingData[0].Type);
            Assert.AreEqual(0x06, advertisingData[0].ToUint8());
            Assert.AreEqual(BleAdvertisingDataType.CompleteListof128BitServiceClassUUIDs, advertisingData[1].Type);
            Assert.AreEqual(new Guid("4880c12c-fdcb-4077-8920-a450d7f9b907"), advertisingData[1].ToGuid());
            Assert.AreEqual(BleAdvertisingDataType.ShortenedLocalName, advertisingData[2].Type);
            Assert.AreEqual("BGM111 S", advertisingData[2].ToAsciiString());
        }
    }
}
