using GameEngine.Core;
using GameEngine.Pipeline;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Assets
{
    public class Font : Asset
    {
        Dictionary<char, Texture2D> characters;

        public Dictionary<char, Texture2D> chars { get { return characters; } }

        public Font(string name, string filename)
            : base(name, filename)
        {
            Load();
        }

        public override void Load()
        {
            using (BinaryReader reader = new BinaryReader(File.Open("Fonts/" + filename + ".gef", FileMode.Open)))
            {
                FontDecoder converter = new FontDecoder();
                this.characters = converter.Convert(reader);
            }
        }
    }
}
