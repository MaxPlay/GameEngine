using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Core
{
    public struct DebugSettings
    {
        public ConsoleColor DefaultConsoleColor;
        public ConsoleColor ErrorConsoleColor;
        public ConsoleColor ImportantConsoleColor;
        public ConsoleColor WarningConsoleColor;

        public int LogDepth;

        public string loggerFile;
    }
}
