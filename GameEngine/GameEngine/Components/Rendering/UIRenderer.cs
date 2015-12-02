using GameEngine.Assets;
using GameEngine.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Components.Rendering
{
    class UIRenderer : BaseRenderer
    {
        Image baseImage;

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

        }
    }
}
