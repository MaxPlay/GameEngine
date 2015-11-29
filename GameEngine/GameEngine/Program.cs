using System;
using GameEngine.Core;

namespace GameEngine
{
#if WINDOWS
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Bootstrap game = new Bootstrap())
            {
                game.Run();
            }
        }
    }
#endif
}

