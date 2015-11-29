using System.Runtime.InteropServices;

namespace FontGenerator
{
    class Programm
    {
        /***
         * We want to use the cmd console.
         ***/
        [DllImport("kernel32.dll")]
        static extern int FreeConsole();

        public static void Main(string[] args)
        {
            new Encoder(args);
        }
    }
}
