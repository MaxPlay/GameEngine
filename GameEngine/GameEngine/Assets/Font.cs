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

        public Texture2D this[char character] { get { if (this.characters.ContainsKey(character)) return characters[character]; return new Texture2D(Bootstrap.graphics.GraphicsDevice, 1, 1); } }

        public Font(string name, string filename)
            : base(name, filename)
        {
            Load();
        }

        public override void Load()
        {
            using (BinaryReader reader = new BinaryReader(File.Open("Fonts/" + filename + ".gef", FileMode.Open)))
            {
                GameEngine.Font.Decoder decoder = new GameEngine.Font.Decoder();
                this.characters = decoder.Convert(reader, Bootstrap.graphics.GraphicsDevice);
            }
        }
    }
}
