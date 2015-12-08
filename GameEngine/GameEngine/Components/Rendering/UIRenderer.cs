using GameEngine.Assets;
using GameEngine.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components.Rendering
{
    class UIRenderer : BaseRenderer
    {
        Image baseImage;

        public Material Material { get { return this.material; } set { this.material = value; } }
        public Image BaseImage { get { return baseImage; } set { baseImage = value; if (UpdatedBaseImage != null) UpdatedBaseImage(this, new EventArgs()); } }

        public event EventHandler UpdatedBaseImage;

        public UIRenderer()
            : base(null)
        {

        }

        public UIRenderer(GameObject gameObject)
            : base(gameObject)
        {

        }

        public override void Reset()
        {
            baseImage = null;
        }

        public override void Draw(Matrix TransformMatrix)
        {
            Bootstrap.spriteBatch.Begin(material.Shader);
            Bootstrap.spriteBatch.Draw(
                baseImage.Texture,
                this.Transform.Position,
                baseImage.Texture.Bounds,
                Color.White,
                this.Transform.Rotation,
                baseImage.Pivot,
                this.Transform.LossyScale,
                SpriteEffects.None,
                0
                );
        }
    }
}
