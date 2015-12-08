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
using System.Xml;

namespace GameEngine.Assets
{
    class Material : Asset
    {
        public Shader Shader;

        public Material(string name, string filename)
            : base(name, filename)
        {
            Load();
        }

        public override void Load()
        {
            using (XmlReader reader = XmlReader.Create(File.Open("Materials/" + this.Filename, FileMode.Open)))
            {
                while (reader.Read())
                {

                }
            }
        }
    }
}
