using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GameEngine.Core
{
    public static class Debug
    {
        public static DebugSettings settings;

        private static bool Initialized;

        static Debug()
        {
        }

        public static void Initialize()
        {
            if (!File.Exists("debugger.settings"))
            {
                using (StreamWriter writer = File.CreateText("debugger.settings"))
                {
                    writer.WriteLine(ConsoleColor.White);
                    writer.WriteLine(ConsoleColor.Red);
                    writer.WriteLine(ConsoleColor.Cyan);
                    writer.WriteLine(ConsoleColor.Yellow);
                    writer.WriteLine("logfile.log");
                }
            }

            string[] settingStrings = File.ReadAllLines("debugger.settings");
            DebugSettings dSettings = new DebugSettings();
            dSettings.DefaultConsoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), settingStrings[0]);
            dSettings.ErrorConsoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), settingStrings[1]);
            dSettings.ImportantConsoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), settingStrings[2]);
            dSettings.WarningConsoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), settingStrings[3]);
            dSettings.loggerFile = settingStrings[4];

            settings = dSettings;

            Initialized = true;
            using (StreamWriter writer = File.CreateText(dSettings.loggerFile))
            {
                writer.WriteLine(DateTime.Now.ToString());
                writer.WriteLine("Logfile generated.");
            }
        }

        public static void Log(string message)
        {
            if (!Initialized)
                return;
#if DEBUG
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
#endif

            using (StreamWriter writer = File.AppendText(settings.loggerFile))
            {
                writer.WriteLine(message);
            }
        }

        public static void Log(string message, params object[] messageArgs)
        {
            if (!Initialized)
                return;
#if DEBUG
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message, messageArgs);
#endif

            using (StreamWriter writer = File.AppendText(settings.loggerFile))
            {
                writer.WriteLine(message, messageArgs);
            }
        }

        public static void LogError(string message)
        {
            if (!Initialized)
                return;
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR] " + message);
#endif

            using (StreamWriter writer = File.AppendText(settings.loggerFile))
            {
                writer.WriteLine("[ERROR] " + message);
            }
        }

        public static void LogError(string message, params object[] messageArgs)
        {
            if (!Initialized)
                return;
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR] " + message, messageArgs);
#endif

            using (StreamWriter writer = File.AppendText(settings.loggerFile))
            {
                writer.WriteLine("[ERROR] " + message, messageArgs);
            }
        }

        public static void LogWarning(string message)
        {
            if (!Initialized)
                return;
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[WARNING] " + message);
#endif

            using (StreamWriter writer = File.AppendText(settings.loggerFile))
            {
                writer.WriteLine("[WARNING] " + message);
            }
        }

        public static void LogWarning(string message, params object[] messageArgs)
        {
            if (!Initialized)
                return;
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[WARNING] " + message, messageArgs);
#endif

            using (StreamWriter writer = File.AppendText(settings.loggerFile))
            {
                writer.WriteLine("[WARNING] " + message, messageArgs);
            }
        }
        public static void LogImportant(string message)
        {
            if (!Initialized)
                return;
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[IMPORTANT] " + message);
#endif

            using (StreamWriter writer = File.AppendText(settings.loggerFile))
            {
                writer.WriteLine("[IMPORTANT] " + message);
            }
        }

        public static void LogImportant(string message, params object[] messageArgs)
        {
            if (!Initialized)
                return;
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[IMPORTANT] " + message, messageArgs);
#endif

            using (StreamWriter writer = File.AppendText(settings.loggerFile))
            {
                writer.WriteLine("[IMPORTANT] " + message, messageArgs);
            }
        }
    }
}
