using Bluegiga;
using Microsoft.Extensions.Logging;

namespace BGLibExt.BleCommands
{
    internal abstract class BleCommand
    {
        protected const int DefaultTimeout = 10000;
        protected readonly ILogger Logger;
        protected BGLib BgLib;
        protected BleModuleConnection BleModuleConnection;

        protected BleCommand(BGLib bgLib, BleModuleConnection bleModuleConnection, ILogger logger)
        {
            BgLib = bgLib;
            BleModuleConnection = bleModuleConnection;
            Logger = logger;
        }
    }
}
