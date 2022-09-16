﻿using Bluegiga;
using Bluegiga.BLE.Events.GAP;
using Microsoft.Extensions.Logging;

namespace BGLibExt
{
    public class BleDeviceDiscovery
    {
        private readonly BGLib _bgLib;
        private readonly BleModuleConnection _bleModuleConnection;
        private readonly ILogger<BleDeviceDiscovery> _logger;

        public event ScanResponseReceivedEventHandler ScanResponse;

        public delegate void ScanResponseReceivedEventHandler(object sender, BleScanResponseReceivedEventArgs e);

        public BleDeviceDiscovery(BGLib bgLib, BleModuleConnection bleModuleConnection, ILogger<BleDeviceDiscovery> logger = null)
        {
            _bgLib = bgLib;
            _bleModuleConnection = bleModuleConnection;
            _logger = logger;
        }

        /// <summary>
        /// Start device discovery
        /// </summary>
        public void StartDeviceDiscovery()
        {
            _logger?.LogDebug("Start device discovery");

            _bgLib.BLEEventGAPScanResponse += OnScanResponse;

            //    _bgLib.SendCommand(_bleModuleConnection.SerialPort, _bgLib.BLECommandGAPSetScanParameters(0xC8, 0xC8, 1));
            _bgLib.SendCommand(_bleModuleConnection.SerialPort, _bgLib.BLECommandGAPSetScanParameters(0x0, 0x07, 0x04));
            _bgLib.SendCommand(_bleModuleConnection.SerialPort, _bgLib.BLECommandSMSetBondableMode(1));
            //   _bgLib.SendCommand(_bleModuleConnection.SerialPort, _bgLib.BLECommandGAPDiscover(0x01));
            _bgLib.SendCommand(_bleModuleConnection.SerialPort, _bgLib.BLECommandGAPDiscover(0x02));
        }

        /// <summary>
        /// Stop device discovery
        /// </summary>
        public void StopDeviceDiscovery()
        {
            _logger?.LogDebug("Stop device discovery");

            _bgLib.SendCommand(_bleModuleConnection.SerialPort, _bgLib.BLECommandGAPEndProcedure());

            _bgLib.BLEEventGAPScanResponse -= OnScanResponse;
        }

        private void OnScanResponse(object sender, ScanResponseEventArgs e)
        {
            ScanResponse?.Invoke(sender, new BleScanResponseReceivedEventArgs(e.rssi, e.packet_type, e.sender, (BleAddressType)e.address_type, e.bond, e.data));
        }
    }
}
