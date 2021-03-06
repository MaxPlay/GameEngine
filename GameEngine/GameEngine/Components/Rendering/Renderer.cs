﻿using GameEngine.Assets;
using GameEngine.Core;
using GameEngine.Exception;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Components.Rendering
{
    class Renderer : BaseRenderer
    {
        public Material Material { get { return this.material; } set { this.material = value; } }
        protected Image texture;
        protected bool isAnimated;

        public ImageMap AnimatedTexture { get { return isAnimated ? (ImageMap)texture : null; } }
        public Image Texture { get { return texture; } set { texture = value; if (texture is ImageMap) isAnimated = true; } }

        public Renderer()
            : base(null)
        {

        }

        public Renderer(GameObject gameObject)
            : base(gameObject)
        {

        }

        public override void Reset()
        {
            texture = null;
            Material = null;
            isAnimated = false;
        }

        public override void Draw(Matrix TransformMatrix)
        {
            if (TransformMatrix == null)
                Bootstrap.spriteBatch.Begin(material.Shader, TransformMatrix);
            else
                Bootstrap.spriteBatch.Begin(material.Shader);
            if (!isAnimated)
                Bootstrap.spriteBatch.Draw(
                    Texture.Texture,
                    this.Transform.Position,
                    Texture.Texture.Bounds,
                    Color.White,
                    this.Transform.Rotation,
                    Texture.Pivot,
                    this.Transform.LossyScale,
                    SpriteEffects.None,
                    0
                    );
        }
    }
}
