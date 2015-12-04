using GameEngine.Assets;
using GameEngine.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameEngine.Pipeline
{
    public class LevelEventArgs : EventArgs
    {
        public string LevelName;
        public long LevelSize;

        public LevelEventArgs()
            : base()
        {

        }
    }

    public static class LevelLoader
    {
        public static List<GameObject> unloadedGameObjects;
        private static Task loadingTask;
        private static Task generateTask;
        private static byte[] LevelContent;
        private static Thread generatorThread;

        public delegate void LevelEventHandler(LevelEventArgs e);

        public static event LevelEventHandler LevelLoadBegin;
        public static event LevelEventHandler LevelLoadFinished;

        static LevelLoader()
        {
            generatorThread = new Thread(GenerateLevel);
        }

        public static void LoadFromFile(string Filename)
        {
            loadingTask = LoadLevelAsync(Filename);
            Task.Factory.ContinueWhenAll(new Task[] { loadingTask }, _ => InitializeLevelGeneration());
        }

        private static async Task LoadLevelAsync(string Filename)
        {
            OnLevelLoadBegin(Filename);
            using (FileStream sourceStream = File.Open(Filename, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                LevelContent = new byte[sourceStream.Length];
                await sourceStream.ReadAsync(LevelContent, 0, (int)sourceStream.Length);
            };
        }

        private static void InitializeLevelGeneration()
        {
            generatorThread.Start();
        }

        private static void GenerateLevel()
        {
            using (MemoryStream stream = new MemoryStream(LevelContent))
            {

            }
        }

        private static void OnLevelLoadBegin(string filename)
        {
            LevelEventArgs e = new LevelEventArgs();
            e.LevelName = filename;
            e.LevelSize = new FileInfo(filename).Length;

            if (LevelLoadBegin != null)
                LevelLoadBegin(e);
        }

        private static void OnLevelLoadFinished(string filename)
        {
            LevelEventArgs e = new LevelEventArgs();
            e.LevelName = filename;
            e.LevelSize = new FileInfo(filename).Length;

            if (LevelLoadFinished != null)
                LevelLoadFinished(e);
        }
    }
}
