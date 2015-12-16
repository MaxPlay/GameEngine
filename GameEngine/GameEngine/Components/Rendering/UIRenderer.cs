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
    public enum UIState
    {
        none,
        hover,
        click
    }

    public class UIRenderer : BaseRenderer
    {
        protected Image baseImage;
        protected Image hoverImage;
        protected Image clickImage;
        protected UIState state;

        public Material Material { get { return this.material; } set { this.material = value; } }
        public Image BaseImage { get { return baseImage; } set { baseImage = value; OnUpdatedBaseImage(); } }
        public Image HoverImage { get { return hoverImage; } set { hoverImage = value; OnUpdatedHoverImage(); } }
        public Image ClickImage { get { return clickImage; } set { clickImage = value; OnUpdatedClickImage(); } }
        public UIState State { get { return this.state; } set { this.state = value; OnStateChanged(); } }

        protected void OnStateChanged()
        {
            if (StateChanged != null)
                StateChanged(this, new EventArgs());
        }

        protected void OnUpdatedBaseImage()
        {
            if (UpdatedBaseImage != null) UpdatedBaseImage(this, new EventArgs());
        }

        protected void OnUpdatedClickImage()
        {
            if (UpdatedClickImage != null) 
                UpdatedClickImage(this, new EventArgs());
        }

        protected void OnUpdatedHoverImage()
        {
            if (UpdatedHoverImage != null)
                UpdatedHoverImage(this, new EventArgs());
        }

        public event EventHandler UpdatedBaseImage;
        public event EventHandler UpdatedHoverImage;
        public event EventHandler UpdatedClickImage;
        public event EventHandler StateChanged;

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
