using GameEngine.Core;
using GameEngine.Pipeline;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Assets
{
    /// <summary>
    /// The Font-Class represents a set of chars saved as images. These charsets are represented and saved by this class.
    /// The files used to store fonts have the extension gef. These are binary-files containing the char and the image as GDI+-Bitmap.
    /// The font needs the external library GameEngine.Font to load and process the gef-files.
    /// </summary>
    public class Font : Asset
    {
        Dictionary<char, Texture2D> characters;

        public Texture2D this[char character] { get { if (this.characters.ContainsKey(character)) return characters[character]; return new Texture2D(Bootstrap.graphics.GraphicsDevice, 1, 1); } }

        public Font() : base(string.Empty, string.Empty) { }
        public Font(string name, string filename)
            : base(name, filename)
        {
            Load();
        }

        public override void Load()
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(Settings.GetLocation(typeof(Font)) + filename, FileMode.Open)))
                {
                    GameEngine.Font.Decoder decoder = new GameEngine.Font.Decoder();
                    this.characters = decoder.Convert(reader, Bootstrap.graphics.GraphicsDevice);
                }
                this.loaded = true;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public static Asset Create(string filename, string name)
        {
            return Settings.AquireAsset<Font>(filename, name);
        }

        /// <summary>
        /// Gets the Offset of the text when applying a relativeHandle to a text
        /// </summary>
        /// <param name="relativeHandle">The applied handle.</param>
        /// <param name="text">The given text.</param>
        /// <returns>A Vector2 representing the offset of the handle.</returns>
        /// <remarks>When used on a text, the negative value needs to be used.</remarks>
        public Vector2 GetHandleOffset(Handle relativeHandle, string text)
        {
            Vector2 offset = this.GetDimension(text);

            switch (relativeHandle)
            {
                case Handle.BottomCenter:
                    return new Vector2(offset.X/2, offset.Y);
                case Handle.BottomLeft:
                    return new Vector2(0, offset.Y);
                case Handle.BottomRight:
                    return offset;
                case Handle.MiddleCenter:
                    return offset/2;
                case Handle.MiddleLeft:
                    return new Vector2(0, offset.Y/2);
                case Handle.MiddleRight:
                    return new Vector2(offset.X, offset.Y/2);
                case Handle.TopCenter:
                    return new Vector2(offset.X/2, 0);
                case Handle.TopRight:
                    return new Vector2(offset.X, 0);
                default:
                    return new Vector2();
            }
        }

        /// <summary>
        /// The dimension of a given text by using this font.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A Vector2 representing the dimensions of the string using this font.</returns>
        public Vector2 GetDimension(string text)
        {
            float xOffset = 0;
            float yOffset = 0;
            for (int i = 0; i < text.Length; i++)
            {
                xOffset += this[text[i]].Width;
                if (yOffset < this[text[i]].Height)
                    yOffset = this[text[i]].Height;
            }

            return new Vector2(xOffset, yOffset);
        }
    }
}
