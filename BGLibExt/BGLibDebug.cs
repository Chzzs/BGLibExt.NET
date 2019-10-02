using BGLibExt;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Bluegiga
{
    public class BGLibDebug : BGLib
    {
        private readonly ILogger<BGLibDebug> _logger;

        public BGLibDebug(ILogger<BGLibDebug> logger)
        {
            _logger = logger;

            BLEResponseSystemReset += (sender, e) => Log(e);
            BLEResponseSystemHello += (sender, e) => Log(e);
            BLEResponseSystemAddressGet += (sender, e) => Log(e);
            BLEResponseSystemRegWrite += (sender, e) => Log(e);
            BLEResponseSystemRegRead += (sender, e) => Log(e);
            BLEResponseSystemGetCounters += (sender, e) => Log(e);
            BLEResponseSystemGetConnections += (sender, e) => Log(e);
            BLEResponseSystemReadMemory += (sender, e) => Log(e);
            BLEResponseSystemGetInfo += (sender, e) => Log(e);
            BLEResponseSystemEndpointTX += (sender, e) => Log(e);
            BLEResponseSystemWhitelistAppend += (sender, e) => Log(e);
            BLEResponseSystemWhitelistRemove += (sender, e) => Log(e);
            BLEResponseSystemWhitelistClear += (sender, e) => Log(e);
            BLEResponseSystemEndpointRX += (sender, e) => Log(e);
            BLEResponseSystemEndpointSetWatermarks += (sender, e) => Log(e);
            BLEResponseSystemAesSetkey += (sender, e) => Log(e);
            BLEResponseSystemAesEncrypt += (sender, e) => Log(e);
            BLEResponseSystemAesDecrypt += (sender, e) => Log(e);
            BLEResponseFlashPSDefrag += (sender, e) => Log(e);
            BLEResponseFlashPSDump += (sender, e) => Log(e);
            BLEResponseFlashPSEraseAll += (sender, e) => Log(e);
            BLEResponseFlashPSSave += (sender, e) => Log(e);
            BLEResponseFlashPSLoad += (sender, e) => Log(e);
            BLEResponseFlashPSErase += (sender, e) => Log(e);
            BLEResponseFlashErasePage += (sender, e) => Log(e);
            BLEResponseFlashWriteData += (sender, e) => Log(e);
            BLEResponseFlashReadData += (sender, e) => Log(e);
            BLEResponseAttributesWrite += (sender, e) => Log(e);
            BLEResponseAttributesRead += (sender, e) => Log(e);
            BLEResponseAttributesReadType += (sender, e) => Log(e);
            BLEResponseAttributesUserReadResponse += (sender, e) => Log(e);
            BLEResponseAttributesUserWriteResponse += (sender, e) => Log(e);
            BLEResponseAttributesSend += (sender, e) => Log(e);
            BLEResponseConnectionDisconnect += (sender, e) => Log(e);
            BLEResponseConnectionGetRssi += (sender, e) => Log(e);
            BLEResponseConnectionUpdate += (sender, e) => Log(e);
            BLEResponseConnectionVersionUpdate += (sender, e) => Log(e);
            BLEResponseConnectionChannelMapGet += (sender, e) => Log(e);
            BLEResponseConnectionChannelMapSet += (sender, e) => Log(e);
            BLEResponseConnectionFeaturesGet += (sender, e) => Log(e);
            BLEResponseConnectionGetStatus += (sender, e) => Log(e);
            BLEResponseConnectionRawTX += (sender, e) => Log(e);
            BLEResponseATTClientFindByTypeValue += (sender, e) => Log(e);
            BLEResponseATTClientReadByGroupType += (sender, e) => Log(e);
            BLEResponseATTClientReadByType += (sender, e) => Log(e);
            BLEResponseATTClientFindInformation += (sender, e) => Log(e);
            BLEResponseATTClientReadByHandle += (sender, e) => Log(e);
            BLEResponseATTClientAttributeWrite += (sender, e) => Log(e);
            BLEResponseATTClientWriteCommand += (sender, e) => Log(e);
            BLEResponseATTClientIndicateConfirm += (sender, e) => Log(e);
            BLEResponseATTClientReadLong += (sender, e) => Log(e);
            BLEResponseATTClientPrepareWrite += (sender, e) => Log(e);
            BLEResponseATTClientExecuteWrite += (sender, e) => Log(e);
            BLEResponseATTClientReadMultiple += (sender, e) => Log(e);
            BLEResponseSMEncryptStart += (sender, e) => Log(e);
            BLEResponseSMSetBondableMode += (sender, e) => Log(e);
            BLEResponseSMDeleteBonding += (sender, e) => Log(e);
            BLEResponseSMSetParameters += (sender, e) => Log(e);
            BLEResponseSMPasskeyEntry += (sender, e) => Log(e);
            BLEResponseSMGetBonds += (sender, e) => Log(e);
            BLEResponseSMSetOobData += (sender, e) => Log(e);
            BLEResponseSMWhitelistBonds += (sender, e) => Log(e);
            BLEResponseGAPSetPrivacyFlags += (sender, e) => Log(e);
            BLEResponseGAPSetMode += (sender, e) => Log(e);
            BLEResponseGAPDiscover += (sender, e) => Log(e);
            BLEResponseGAPConnectDirect += (sender, e) => Log(e);
            BLEResponseGAPEndProcedure += (sender, e) => Log(e);
            BLEResponseGAPConnectSelective += (sender, e) => Log(e);
            BLEResponseGAPSetFiltering += (sender, e) => Log(e);
            BLEResponseGAPSetScanParameters += (sender, e) => Log(e);
            BLEResponseGAPSetAdvParameters += (sender, e) => Log(e);
            BLEResponseGAPSetAdvData += (sender, e) => Log(e);
            BLEResponseGAPSetDirectedConnectableMode += (sender, e) => Log(e);
            BLEResponseHardwareIOPortConfigIrq += (sender, e) => Log(e);
            BLEResponseHardwareSetSoftTimer += (sender, e) => Log(e);
            BLEResponseHardwareADCRead += (sender, e) => Log(e);
            BLEResponseHardwareIOPortConfigDirection += (sender, e) => Log(e);
            BLEResponseHardwareIOPortConfigFunction += (sender, e) => Log(e);
            BLEResponseHardwareIOPortConfigPull += (sender, e) => Log(e);
            BLEResponseHardwareIOPortWrite += (sender, e) => Log(e);
            BLEResponseHardwareIOPortRead += (sender, e) => Log(e);
            BLEResponseHardwareSPIConfig += (sender, e) => Log(e);
            BLEResponseHardwareSPITransfer += (sender, e) => Log(e);
            BLEResponseHardwareI2CRead += (sender, e) => Log(e);
            BLEResponseHardwareI2CWrite += (sender, e) => Log(e);
            BLEResponseHardwareSetTxpower += (sender, e) => Log(e);
            BLEResponseHardwareTimerComparator += (sender, e) => Log(e);
            BLEResponseHardwareIOPortIrqEnable += (sender, e) => Log(e);
            BLEResponseHardwareIOPortIrqDirection += (sender, e) => Log(e);
            BLEResponseHardwareAnalogComparatorEnable += (sender, e) => Log(e);
            BLEResponseHardwareAnalogComparatorRead += (sender, e) => Log(e);
            BLEResponseHardwareAnalogComparatorConfigIrq += (sender, e) => Log(e);
            BLEResponseHardwareSetRxgain += (sender, e) => Log(e);
            BLEResponseHardwareUsbEnable += (sender, e) => Log(e);
            BLEResponseTestPHYTX += (sender, e) => Log(e);
            BLEResponseTestPHYRX += (sender, e) => Log(e);
            BLEResponseTestPHYEnd += (sender, e) => Log(e);
            BLEResponseTestPHYReset += (sender, e) => Log(e);
            BLEResponseTestGetChannelMap += (sender, e) => Log(e);
            BLEResponseTestDebug += (sender, e) => Log(e);
            BLEResponseTestChannelMode += (sender, e) => Log(e);
            BLEResponseDFUReset += (sender, e) => Log(e);
            BLEResponseDFUFlashSetAddress += (sender, e) => Log(e);
            BLEResponseDFUFlashUpload += (sender, e) => Log(e);
            BLEResponseDFUFlashUploadFinish += (sender, e) => Log(e);

            BLEEventSystemBoot += (sender, e) => Log(e);
            BLEEventSystemDebug += (sender, e) => Log(e);
            BLEEventSystemEndpointWatermarkRX += (sender, e) => Log(e);
            BLEEventSystemEndpointWatermarkTX += (sender, e) => Log(e);
            BLEEventSystemScriptFailure += (sender, e) => Log(e);
            BLEEventSystemNoLicenseKey += (sender, e) => Log(e);
            BLEEventSystemProtocolError += (sender, e) => Log(e);
            BLEEventFlashPSKey += (sender, e) => Log(e);
            BLEEventAttributesValue += (sender, e) => Log(e);
            BLEEventAttributesUserReadRequest += (sender, e) => Log(e);
            BLEEventAttributesStatus += (sender, e) => Log(e);
            BLEEventConnectionStatus += (sender, e) => Log(e);
            BLEEventConnectionVersionInd += (sender, e) => Log(e);
            BLEEventConnectionFeatureInd += (sender, e) => Log(e);
            BLEEventConnectionRawRX += (sender, e) => Log(e);
            BLEEventConnectionDisconnected += (sender, e) => Log(e);
            BLEEventATTClientIndicated += (sender, e) => Log(e);
            BLEEventATTClientProcedureCompleted += (sender, e) => Log(e);
            BLEEventATTClientGroupFound += (sender, e) => Log(e);
            BLEEventATTClientAttributeFound += (sender, e) => Log(e);
            BLEEventATTClientFindInformationFound += (sender, e) => Log(e);
            BLEEventATTClientAttributeValue += (sender, e) => Log(e);
            BLEEventATTClientReadMultipleResponse += (sender, e) => Log(e);
            BLEEventSMSMPData += (sender, e) => Log(e);
            BLEEventSMBondingFail += (sender, e) => Log(e);
            BLEEventSMPasskeyDisplay += (sender, e) => Log(e);
            BLEEventSMPasskeyRequest += (sender, e) => Log(e);
            BLEEventSMBondStatus += (sender, e) => Log(e);
            BLEEventGAPScanResponse += (sender, e) => Log(e);
            BLEEventGAPModeChanged += (sender, e) => Log(e);
            BLEEventHardwareIOPortStatus += (sender, e) => Log(e);
            BLEEventHardwareSoftTimer += (sender, e) => Log(e);
            BLEEventHardwareADCResult += (sender, e) => Log(e);
            BLEEventHardwareAnalogComparatorStatus += (sender, e) => Log(e);
            BLEEventDFUBoot += (sender, e) => Log(e);
        }

        public override UInt16 SendCommand(System.IO.Ports.SerialPort port, Byte[] cmd)
        {
            _logger.LogTrace($"Send bglib command, Data={cmd.ByteArrayToHexString()}");

            return base.SendCommand(port, cmd);
        }

        private void Log(object e)
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
            _logger.LogTrace($"{type} triggered, {string.Join(", ", fields)}");
        }
    }
}
