using Bluegiga;

namespace BGLibExt.BleCommands
{
    internal abstract class BleCommand
    {
        protected const int DefaultTimeout = 10000;

        protected BGLib _bgLib;
        protected BleModuleConnection _bleModuleConnection;

        protected BleCommand(BGLib bgLib, BleModuleConnection bleModuleConnection)
        {
            _bgLib = bgLib;
            _bleModuleConnection = bleModuleConnection;
        }
    }
}
