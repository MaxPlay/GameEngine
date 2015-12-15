using GameEngine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace GameEngine.Assets
{
    public class Image : Asset
    {
        protected Texture2D texture;
        protected Handle handle;
        protected Vector2 pivot;

        public Texture2D Texture { get { return texture; } }

        public Vector2 Pivot { get { return pivot; } }
        public Rectangle Dimension { get { return texture.Bounds; } }
        public Handle Handle
        {
            get { return handle; }
            set
            {
                handle = value;
                switch (handle)
                {
                    case Handle.TopLeft:
                        pivot = new Vector2(0, 0);
                        break;
                    case Handle.TopCenter:
                        pivot = new Vector2(0 / 2f, 0);
                        break;
                    case Handle.TopRight:
                        pivot = new Vector2(0, 0);
                        break;

                    case Handle.MiddleLeft:
                        pivot = new Vector2(0, texture.Bounds.Height / 2f);
                        break;
                    case Handle.MiddleCenter:
                        pivot = new Vector2(texture.Bounds.Width / 2f, texture.Bounds.Height / 2f);
                        break;
                    case Handle.MiddleRight:
                        pivot = new Vector2(texture.Bounds.Width, texture.Bounds.Height / 2f);
                        break;

                    case Handle.BottomLeft:
                        pivot = new Vector2(0, texture.Bounds.Height);
                        break;
                    case Handle.BottomCenter:
                        pivot = new Vector2(texture.Bounds.Width / 2f, texture.Bounds.Height);
                        break;
                    default:
                        pivot = new Vector2(texture.Bounds.Width, texture.Bounds.Height);
                        break;
                }
            }
        }

        public Image() : base(string.Empty, string.Empty) { }
        public Image(string name, string filename)
            : base(name, filename)
        {
            Load();
        }

        public override void Load()
        {
            try
            {
                using (FileStream stream = new FileStream(Settings.GetLocation(typeof(Image)) + this.filename, FileMode.Open))
                {
                    texture = Texture2D.FromStream(Bootstrap.graphics.GraphicsDevice, stream);
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
            return Settings.AquireAsset<Image>(filename, name);
        }
    }
}
