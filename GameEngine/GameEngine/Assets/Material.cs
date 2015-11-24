using GameEngine.Core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEngine.Assets
{
    class Material : Asset
    {
        public Effect Shader;

        public Material(string name, string filename)
            : base(name, filename)
        {

        }

        public override void Load()
        {
            using (FileStream stream = new FileStream("Materials/" + this.Filename, FileMode.Open))
            {

                stream.Close();
            }
        }
    }
}
