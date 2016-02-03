using System;

namespace git.jrowberg.bglib.Bluegiga
{
	public class BGLibDebug : BGLib
	{
		private void Print (object e)
		{
			Type t = e.GetType ();
			DateTime now = DateTime.Now;
			Console.WriteLine ($"{now} - {t} triggered");
		}

		public BGLibDebug ()
		{

			//public event 		Bluegiga.BLE.Responses.System.ResetEventHandler 
			BLEResponseSystemReset += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.HelloEventHandler 
			BLEResponseSystemHello += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.AddressGetEventHandler 
			BLEResponseSystemAddressGet += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.RegWriteEventHandler 
			BLEResponseSystemRegWrite += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.RegReadEventHandler 
			BLEResponseSystemRegRead += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.GetCountersEventHandler 
			BLEResponseSystemGetCounters += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.GetConnectionsEventHandler 
			BLEResponseSystemGetConnections += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.ReadMemoryEventHandler 
			BLEResponseSystemReadMemory += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.GetInfoEventHandler 
			BLEResponseSystemGetInfo += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.EndpointTXEventHandler 
			BLEResponseSystemEndpointTX += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.WhitelistAppendEventHandler 
			BLEResponseSystemWhitelistAppend += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.WhitelistRemoveEventHandler 
			BLEResponseSystemWhitelistRemove += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.WhitelistClearEventHandler 
			BLEResponseSystemWhitelistClear += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.EndpointRXEventHandler 
			BLEResponseSystemEndpointRX += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.EndpointSetWatermarksEventHandler 
			BLEResponseSystemEndpointSetWatermarks += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.AesSetkeyEventHandler 
			BLEResponseSystemAesSetkey += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.AesEncryptEventHandler 
			BLEResponseSystemAesEncrypt += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.System.AesDecryptEventHandler 
			BLEResponseSystemAesDecrypt += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Flash.PSDefragEventHandler 
			BLEResponseFlashPSDefrag += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Flash.PSDumpEventHandler 
			BLEResponseFlashPSDump += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Flash.PSEraseAllEventHandler 
			BLEResponseFlashPSEraseAll += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Flash.PSSaveEventHandler 
			BLEResponseFlashPSSave += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Flash.PSLoadEventHandler 
			BLEResponseFlashPSLoad += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Flash.PSEraseEventHandler 
			BLEResponseFlashPSErase += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Flash.ErasePageEventHandler 
			BLEResponseFlashErasePage += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Flash.WriteDataEventHandler 
			BLEResponseFlashWriteData += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Flash.ReadDataEventHandler 
			BLEResponseFlashReadData += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Attributes.WriteEventHandler 
			BLEResponseAttributesWrite += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Attributes.ReadEventHandler 
			BLEResponseAttributesRead += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Attributes.ReadTypeEventHandler 
			BLEResponseAttributesReadType += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Attributes.UserReadResponseEventHandler 
			BLEResponseAttributesUserReadResponse += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Attributes.UserWriteResponseEventHandler 
			BLEResponseAttributesUserWriteResponse += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Attributes.SendEventHandler 
			BLEResponseAttributesSend += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Connection.DisconnectEventHandler 
			BLEResponseConnectionDisconnect += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Connection.GetRssiEventHandler 
			BLEResponseConnectionGetRssi += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Connection.UpdateEventHandler 
			BLEResponseConnectionUpdate += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Connection.VersionUpdateEventHandler 
			BLEResponseConnectionVersionUpdate += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Connection.ChannelMapGetEventHandler 
			BLEResponseConnectionChannelMapGet += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Connection.ChannelMapSetEventHandler 
			BLEResponseConnectionChannelMapSet += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Connection.FeaturesGetEventHandler 
			BLEResponseConnectionFeaturesGet += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Connection.GetStatusEventHandler 
			BLEResponseConnectionGetStatus += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Connection.RawTXEventHandler 
			BLEResponseConnectionRawTX += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.ATTClient.FindByTypeValueEventHandler 
			BLEResponseATTClientFindByTypeValue += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.ATTClient.ReadByGroupTypeEventHandler 
			BLEResponseATTClientReadByGroupType += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.ATTClient.ReadByTypeEventHandler 
			BLEResponseATTClientReadByType += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.ATTClient.FindInformationEventHandler 
			BLEResponseATTClientFindInformation += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.ATTClient.ReadByHandleEventHandler 
			BLEResponseATTClientReadByHandle += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.ATTClient.AttributeWriteEventHandler 
			BLEResponseATTClientAttributeWrite += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.ATTClient.WriteCommandEventHandler 
			BLEResponseATTClientWriteCommand += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.ATTClient.IndicateConfirmEventHandler 
			BLEResponseATTClientIndicateConfirm += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.ATTClient.ReadLongEventHandler 
			BLEResponseATTClientReadLong += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.ATTClient.PrepareWriteEventHandler 
			BLEResponseATTClientPrepareWrite += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.ATTClient.ExecuteWriteEventHandler 
			BLEResponseATTClientExecuteWrite += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.ATTClient.ReadMultipleEventHandler 
			BLEResponseATTClientReadMultiple += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.SM.EncryptStartEventHandler 
			BLEResponseSMEncryptStart += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.SM.SetBondableModeEventHandler 
			BLEResponseSMSetBondableMode += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.SM.DeleteBondingEventHandler 
			BLEResponseSMDeleteBonding += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.SM.SetParametersEventHandler 
			BLEResponseSMSetParameters += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.SM.PasskeyEntryEventHandler 
			BLEResponseSMPasskeyEntry += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.SM.GetBondsEventHandler 
			BLEResponseSMGetBonds += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.SM.SetOobDataEventHandler 
			BLEResponseSMSetOobData += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.SM.WhitelistBondsEventHandler 
			BLEResponseSMWhitelistBonds += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.GAP.SetPrivacyFlagsEventHandler 
			BLEResponseGAPSetPrivacyFlags += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.GAP.SetModeEventHandler 
			BLEResponseGAPSetMode += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.GAP.DiscoverEventHandler 
			BLEResponseGAPDiscover += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.GAP.ConnectDirectEventHandler 
			BLEResponseGAPConnectDirect += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.GAP.EndProcedureEventHandler 
			BLEResponseGAPEndProcedure += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.GAP.ConnectSelectiveEventHandler 
			BLEResponseGAPConnectSelective += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.GAP.SetFilteringEventHandler 
			BLEResponseGAPSetFiltering += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.GAP.SetScanParametersEventHandler 
			BLEResponseGAPSetScanParameters += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.GAP.SetAdvParametersEventHandler 
			BLEResponseGAPSetAdvParameters += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.GAP.SetAdvDataEventHandler 
			BLEResponseGAPSetAdvData += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.GAP.SetDirectedConnectableModeEventHandler 
			BLEResponseGAPSetDirectedConnectableMode += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.IOPortConfigIrqEventHandler 
			BLEResponseHardwareIOPortConfigIrq += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.SetSoftTimerEventHandler 
			BLEResponseHardwareSetSoftTimer += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.ADCReadEventHandler 
			BLEResponseHardwareADCRead += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.IOPortConfigDirectionEventHandler 
			BLEResponseHardwareIOPortConfigDirection += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.IOPortConfigFunctionEventHandler 
			BLEResponseHardwareIOPortConfigFunction += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.IOPortConfigPullEventHandler 
			BLEResponseHardwareIOPortConfigPull += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.IOPortWriteEventHandler 
			BLEResponseHardwareIOPortWrite += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.IOPortReadEventHandler 
			BLEResponseHardwareIOPortRead += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.SPIConfigEventHandler 
			BLEResponseHardwareSPIConfig += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.SPITransferEventHandler 
			BLEResponseHardwareSPITransfer += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.I2CReadEventHandler 
			BLEResponseHardwareI2CRead += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.I2CWriteEventHandler 
			BLEResponseHardwareI2CWrite += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.SetTxpowerEventHandler 
			BLEResponseHardwareSetTxpower += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.TimerComparatorEventHandler 
			BLEResponseHardwareTimerComparator += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.IOPortIrqEnableEventHandler 
			BLEResponseHardwareIOPortIrqEnable += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.IOPortIrqDirectionEventHandler 
			BLEResponseHardwareIOPortIrqDirection += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.AnalogComparatorEnableEventHandler 
			BLEResponseHardwareAnalogComparatorEnable += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.AnalogComparatorReadEventHandler 
			BLEResponseHardwareAnalogComparatorRead += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.AnalogComparatorConfigIrqEventHandler 
			BLEResponseHardwareAnalogComparatorConfigIrq += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.SetRxgainEventHandler 
			BLEResponseHardwareSetRxgain += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Hardware.UsbEnableEventHandler 
			BLEResponseHardwareUsbEnable += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Test.PHYTXEventHandler 
			BLEResponseTestPHYTX += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Test.PHYRXEventHandler 
			BLEResponseTestPHYRX += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Test.PHYEndEventHandler 
			BLEResponseTestPHYEnd += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Test.PHYResetEventHandler 
			BLEResponseTestPHYReset += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Test.GetChannelMapEventHandler 
			BLEResponseTestGetChannelMap += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Test.DebugEventHandler 
			BLEResponseTestDebug += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.Test.ChannelModeEventHandler 
			BLEResponseTestChannelMode += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.DFU.ResetEventHandler 
			BLEResponseDFUReset += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.DFU.FlashSetAddressEventHandler 
			BLEResponseDFUFlashSetAddress += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.DFU.FlashUploadEventHandler 
			BLEResponseDFUFlashUpload += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Responses.DFU.FlashUploadFinishEventHandler 
			BLEResponseDFUFlashUploadFinish += (sender, e) => Print (e);

			//public event 		Bluegiga.BLE.Events.System.BootEventHandler 
			BLEEventSystemBoot += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.System.DebugEventHandler 
			BLEEventSystemDebug += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.System.EndpointWatermarkRXEventHandler 
			BLEEventSystemEndpointWatermarkRX += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.System.EndpointWatermarkTXEventHandler 
			BLEEventSystemEndpointWatermarkTX += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.System.ScriptFailureEventHandler 
			BLEEventSystemScriptFailure += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.System.NoLicenseKeyEventHandler 
			BLEEventSystemNoLicenseKey += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.System.ProtocolErrorEventHandler 
			BLEEventSystemProtocolError += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.Flash.PSKeyEventHandler 
			BLEEventFlashPSKey += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.Attributes.ValueEventHandler 
			BLEEventAttributesValue += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.Attributes.UserReadRequestEventHandler 
			BLEEventAttributesUserReadRequest += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.Attributes.StatusEventHandler 
			BLEEventAttributesStatus += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.Connection.StatusEventHandler 
			BLEEventConnectionStatus += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.Connection.VersionIndEventHandler 
			BLEEventConnectionVersionInd += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.Connection.FeatureIndEventHandler 
			BLEEventConnectionFeatureInd += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.Connection.RawRXEventHandler 
			BLEEventConnectionRawRX += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.Connection.DisconnectedEventHandler 
			BLEEventConnectionDisconnected += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.ATTClient.IndicatedEventHandler 
			BLEEventATTClientIndicated += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.ATTClient.ProcedureCompletedEventHandler 
			BLEEventATTClientProcedureCompleted += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.ATTClient.GroupFoundEventHandler 
			BLEEventATTClientGroupFound += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.ATTClient.AttributeFoundEventHandler 
			BLEEventATTClientAttributeFound += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.ATTClient.FindInformationFoundEventHandler 
			BLEEventATTClientFindInformationFound += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.ATTClient.AttributeValueEventHandler 
			BLEEventATTClientAttributeValue += (sender, e) => Console.WriteLine (e.atthandle);
			//public event 		Bluegiga.BLE.Events.ATTClient.ReadMultipleResponseEventHandler 
			BLEEventATTClientReadMultipleResponse += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.SM.SMPDataEventHandler 
			BLEEventSMSMPData += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.SM.BondingFailEventHandler 
			BLEEventSMBondingFail += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.SM.PasskeyDisplayEventHandler 
			BLEEventSMPasskeyDisplay += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.SM.PasskeyRequestEventHandler 
			BLEEventSMPasskeyRequest += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.SM.BondStatusEventHandler 
			BLEEventSMBondStatus += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.GAP.ScanResponseEventHandler 
			BLEEventGAPScanResponse += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.GAP.ModeChangedEventHandler 
			BLEEventGAPModeChanged += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.Hardware.IOPortStatusEventHandler 
			BLEEventHardwareIOPortStatus += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.Hardware.SoftTimerEventHandler 
			BLEEventHardwareSoftTimer += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.Hardware.ADCResultEventHandler 
			BLEEventHardwareADCResult += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.Hardware.AnalogComparatorStatusEventHandler 
			BLEEventHardwareAnalogComparatorStatus += (sender, e) => Print (e);
			//public event 		Bluegiga.BLE.Events.DFU.BootEventHandler 
			BLEEventDFUBoot += (sender, e) => Print (e);
		}
	}
}

