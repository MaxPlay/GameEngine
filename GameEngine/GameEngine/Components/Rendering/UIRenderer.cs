using GameEngine.Assets;
using GameEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Components.Rendering
{
    class UIRenderer : Renderer
    {
        Image baseImage;

        public Image BaseImage { get { return baseImage; } }

        public UIRenderer(GameObject gameObject)
            : base(gameObject)
        {

        }
    }
}
