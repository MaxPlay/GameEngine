using GameEngine.Components.Rendering;
using GameEngine.Core;
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

        List<Renderer> renderers;

        public Level(string name, string filename)
            : base(name, filename)
        {
            gameObjects = new List<GameObject>();
            renderers = new List<Renderer>();
        }

        public void Update()
        {

        }

        public void Draw()
        {
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].Draw();
            }
        }

        public override void Load()
        {
            using (FileStream stream = new FileStream("Levels/" + filename, FileMode.Open))
            {

                stream.Close();
            }
        }
    }
}
