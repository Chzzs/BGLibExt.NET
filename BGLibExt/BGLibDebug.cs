using BGLibExt;
using System;
using System.Collections.Generic;

namespace Bluegiga
{
    public class BGLibDebug : BGLib
    {
        public BGLibDebug()
        {
            BLEResponseSystemReset += (sender, e) => Print(e);
            BLEResponseSystemHello += (sender, e) => Print(e);
            BLEResponseSystemAddressGet += (sender, e) => Print(e);
            BLEResponseSystemRegWrite += (sender, e) => Print(e);
            BLEResponseSystemRegRead += (sender, e) => Print(e);
            BLEResponseSystemGetCounters += (sender, e) => Print(e);
            BLEResponseSystemGetConnections += (sender, e) => Print(e);
            BLEResponseSystemReadMemory += (sender, e) => Print(e);
            BLEResponseSystemGetInfo += (sender, e) => Print(e);
            BLEResponseSystemEndpointTX += (sender, e) => Print(e);
            BLEResponseSystemWhitelistAppend += (sender, e) => Print(e);
            BLEResponseSystemWhitelistRemove += (sender, e) => Print(e);
            BLEResponseSystemWhitelistClear += (sender, e) => Print(e);
            BLEResponseSystemEndpointRX += (sender, e) => Print(e);
            BLEResponseSystemEndpointSetWatermarks += (sender, e) => Print(e);
            BLEResponseSystemAesSetkey += (sender, e) => Print(e);
            BLEResponseSystemAesEncrypt += (sender, e) => Print(e);
            BLEResponseSystemAesDecrypt += (sender, e) => Print(e);
            BLEResponseFlashPSDefrag += (sender, e) => Print(e);
            BLEResponseFlashPSDump += (sender, e) => Print(e);
            BLEResponseFlashPSEraseAll += (sender, e) => Print(e);
            BLEResponseFlashPSSave += (sender, e) => Print(e);
            BLEResponseFlashPSLoad += (sender, e) => Print(e);
            BLEResponseFlashPSErase += (sender, e) => Print(e);
            BLEResponseFlashErasePage += (sender, e) => Print(e);
            BLEResponseFlashWriteData += (sender, e) => Print(e);
            BLEResponseFlashReadData += (sender, e) => Print(e);
            BLEResponseAttributesWrite += (sender, e) => Print(e);
            BLEResponseAttributesRead += (sender, e) => Print(e);
            BLEResponseAttributesReadType += (sender, e) => Print(e);
            BLEResponseAttributesUserReadResponse += (sender, e) => Print(e);
            BLEResponseAttributesUserWriteResponse += (sender, e) => Print(e);
            BLEResponseAttributesSend += (sender, e) => Print(e);
            BLEResponseConnectionDisconnect += (sender, e) => Print(e);
            BLEResponseConnectionGetRssi += (sender, e) => Print(e);
            BLEResponseConnectionUpdate += (sender, e) => Print(e);
            BLEResponseConnectionVersionUpdate += (sender, e) => Print(e);
            BLEResponseConnectionChannelMapGet += (sender, e) => Print(e);
            BLEResponseConnectionChannelMapSet += (sender, e) => Print(e);
            BLEResponseConnectionFeaturesGet += (sender, e) => Print(e);
            BLEResponseConnectionGetStatus += (sender, e) => Print(e);
            BLEResponseConnectionRawTX += (sender, e) => Print(e);
            BLEResponseATTClientFindByTypeValue += (sender, e) => Print(e);
            BLEResponseATTClientReadByGroupType += (sender, e) => Print(e);
            BLEResponseATTClientReadByType += (sender, e) => Print(e);
            BLEResponseATTClientFindInformation += (sender, e) => Print(e);
            BLEResponseATTClientReadByHandle += (sender, e) => Print(e);
            BLEResponseATTClientAttributeWrite += (sender, e) => Print(e);
            BLEResponseATTClientWriteCommand += (sender, e) => Print(e);
            BLEResponseATTClientIndicateConfirm += (sender, e) => Print(e);
            BLEResponseATTClientReadLong += (sender, e) => Print(e);
            BLEResponseATTClientPrepareWrite += (sender, e) => Print(e);
            BLEResponseATTClientExecuteWrite += (sender, e) => Print(e);
            BLEResponseATTClientReadMultiple += (sender, e) => Print(e);
            BLEResponseSMEncryptStart += (sender, e) => Print(e);
            BLEResponseSMSetBondableMode += (sender, e) => Print(e);
            BLEResponseSMDeleteBonding += (sender, e) => Print(e);
            BLEResponseSMSetParameters += (sender, e) => Print(e);
            BLEResponseSMPasskeyEntry += (sender, e) => Print(e);
            BLEResponseSMGetBonds += (sender, e) => Print(e);
            BLEResponseSMSetOobData += (sender, e) => Print(e);
            BLEResponseSMWhitelistBonds += (sender, e) => Print(e);
            BLEResponseGAPSetPrivacyFlags += (sender, e) => Print(e);
            BLEResponseGAPSetMode += (sender, e) => Print(e);
            BLEResponseGAPDiscover += (sender, e) => Print(e);
            BLEResponseGAPConnectDirect += (sender, e) => Print(e);
            BLEResponseGAPEndProcedure += (sender, e) => Print(e);
            BLEResponseGAPConnectSelective += (sender, e) => Print(e);
            BLEResponseGAPSetFiltering += (sender, e) => Print(e);
            BLEResponseGAPSetScanParameters += (sender, e) => Print(e);
            BLEResponseGAPSetAdvParameters += (sender, e) => Print(e);
            BLEResponseGAPSetAdvData += (sender, e) => Print(e);
            BLEResponseGAPSetDirectedConnectableMode += (sender, e) => Print(e);
            BLEResponseHardwareIOPortConfigIrq += (sender, e) => Print(e);
            BLEResponseHardwareSetSoftTimer += (sender, e) => Print(e);
            BLEResponseHardwareADCRead += (sender, e) => Print(e);
            BLEResponseHardwareIOPortConfigDirection += (sender, e) => Print(e);
            BLEResponseHardwareIOPortConfigFunction += (sender, e) => Print(e);
            BLEResponseHardwareIOPortConfigPull += (sender, e) => Print(e);
            BLEResponseHardwareIOPortWrite += (sender, e) => Print(e);
            BLEResponseHardwareIOPortRead += (sender, e) => Print(e);
            BLEResponseHardwareSPIConfig += (sender, e) => Print(e);
            BLEResponseHardwareSPITransfer += (sender, e) => Print(e);
            BLEResponseHardwareI2CRead += (sender, e) => Print(e);
            BLEResponseHardwareI2CWrite += (sender, e) => Print(e);
            BLEResponseHardwareSetTxpower += (sender, e) => Print(e);
            BLEResponseHardwareTimerComparator += (sender, e) => Print(e);
            BLEResponseHardwareIOPortIrqEnable += (sender, e) => Print(e);
            BLEResponseHardwareIOPortIrqDirection += (sender, e) => Print(e);
            BLEResponseHardwareAnalogComparatorEnable += (sender, e) => Print(e);
            BLEResponseHardwareAnalogComparatorRead += (sender, e) => Print(e);
            BLEResponseHardwareAnalogComparatorConfigIrq += (sender, e) => Print(e);
            BLEResponseHardwareSetRxgain += (sender, e) => Print(e);
            BLEResponseHardwareUsbEnable += (sender, e) => Print(e);
            BLEResponseTestPHYTX += (sender, e) => Print(e);
            BLEResponseTestPHYRX += (sender, e) => Print(e);
            BLEResponseTestPHYEnd += (sender, e) => Print(e);
            BLEResponseTestPHYReset += (sender, e) => Print(e);
            BLEResponseTestGetChannelMap += (sender, e) => Print(e);
            BLEResponseTestDebug += (sender, e) => Print(e);
            BLEResponseTestChannelMode += (sender, e) => Print(e);
            BLEResponseDFUReset += (sender, e) => Print(e);
            BLEResponseDFUFlashSetAddress += (sender, e) => Print(e);
            BLEResponseDFUFlashUpload += (sender, e) => Print(e);
            BLEResponseDFUFlashUploadFinish += (sender, e) => Print(e);

            BLEEventSystemBoot += (sender, e) => Print(e);
            BLEEventSystemDebug += (sender, e) => Print(e);
            BLEEventSystemEndpointWatermarkRX += (sender, e) => Print(e);
            BLEEventSystemEndpointWatermarkTX += (sender, e) => Print(e);
            BLEEventSystemScriptFailure += (sender, e) => Print(e);
            BLEEventSystemNoLicenseKey += (sender, e) => Print(e);
            BLEEventSystemProtocolError += (sender, e) => Print(e);
            BLEEventFlashPSKey += (sender, e) => Print(e);
            BLEEventAttributesValue += (sender, e) => Print(e);
            BLEEventAttributesUserReadRequest += (sender, e) => Print(e);
            BLEEventAttributesStatus += (sender, e) => Print(e);
            BLEEventConnectionStatus += (sender, e) => Print(e);
            BLEEventConnectionVersionInd += (sender, e) => Print(e);
            BLEEventConnectionFeatureInd += (sender, e) => Print(e);
            BLEEventConnectionRawRX += (sender, e) => Print(e);
            BLEEventConnectionDisconnected += (sender, e) => Print(e);
            BLEEventATTClientIndicated += (sender, e) => Print(e);
            BLEEventATTClientProcedureCompleted += (sender, e) => Print(e);
            BLEEventATTClientGroupFound += (sender, e) => Print(e);
            BLEEventATTClientAttributeFound += (sender, e) => Print(e);
            BLEEventATTClientFindInformationFound += (sender, e) => Print(e);
            BLEEventATTClientAttributeValue += (sender, e) => Print(e);
            BLEEventATTClientReadMultipleResponse += (sender, e) => Print(e);
            BLEEventSMSMPData += (sender, e) => Print(e);
            BLEEventSMBondingFail += (sender, e) => Print(e);
            BLEEventSMPasskeyDisplay += (sender, e) => Print(e);
            BLEEventSMPasskeyRequest += (sender, e) => Print(e);
            BLEEventSMBondStatus += (sender, e) => Print(e);
            BLEEventGAPScanResponse += (sender, e) => Print(e);
            BLEEventGAPModeChanged += (sender, e) => Print(e);
            BLEEventHardwareIOPortStatus += (sender, e) => Print(e);
            BLEEventHardwareSoftTimer += (sender, e) => Print(e);
            BLEEventHardwareADCResult += (sender, e) => Print(e);
            BLEEventHardwareAnalogComparatorStatus += (sender, e) => Print(e);
            BLEEventDFUBoot += (sender, e) => Print(e);
        }

        private void Print(object e)
        {
            var type = e.GetType();
            var fields = new List<string>();
            foreach (var field in type.GetFields())
            {
                var value = field.GetValue(e);
                if (value is byte[])
                {
                    fields.Add($"{field.Name}={((byte[])value).ByteArrayToHexString()}");
                }
                else
                {
                    fields.Add($"{field.Name}={value}");
                }
            }
            Console.WriteLine($"{type} triggered, {string.Join(", ", fields)}");
        }
    }
}
