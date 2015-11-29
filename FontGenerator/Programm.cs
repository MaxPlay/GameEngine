using System.Runtime.InteropServices;

namespace FontGenerator
{
    class Programm
    {
        [DllImport("kernel32.dll")]
        static extern int FreeConsole();

        public static void Main(string[] args)
        {
            new Converter(args);
        }
    }
}
