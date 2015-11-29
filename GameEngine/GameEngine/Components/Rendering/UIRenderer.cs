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

        public Image BaseImage { get { return baseImage; } }

        public UIRenderer(GameObject gameObject)
            : base(gameObject)
        {

        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        public override void Draw(Matrix TransformMatrix)
        {
            throw new NotImplementedException();
        }
    }
}
