using GameEngine.Components.Audio;
using GameEngine.Components.Rendering;
using GameEngine.Core;
using GameEngine.Pipeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEngine.Assets
{
    class Level : Asset
    {
        List<GameObject> gameObjects;

        List<BaseRenderer> renderers;
        public Camera MainCamera { get { return Camera.main; } }
        public AudioListener MainAudioListener { get { return AudioListener.main; } }

        protected List<int> audioSourceIndices;

        public static Level main;

        public Level() : base(string.Empty, string.Empty) { }
        public Level(string name, string filename)
            : base(name, filename)
        {
            gameObjects = new List<GameObject>();
            renderers = new List<BaseRenderer>();
            audioSourceIndices = new List<int>();

            if (main == null)
                main = this;
            Load();
        }

        public void Update()
        {

        }

        public void Draw()
        {
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].Draw(MainCamera.TransformMatrix);
            }
        }

        public override void Load()
        {
            try
            {
                LevelLoader.LoadFromFile(Settings.GetLocation(typeof(Level)) + filename);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public bool RegisterRenderer(BaseRenderer renderer)
        {
            if (renderers.Contains(renderer))
                return false;
            renderers.Add(renderer);
            return true;
        }

        public bool UnregisterRenderer(Renderer renderer)
        {
            return renderers.Remove(renderer);
        }

        public static Asset Create(string filename, string name)
        {
            return Settings.AquireAsset<Level>(filename, name);
        }
    }
}
