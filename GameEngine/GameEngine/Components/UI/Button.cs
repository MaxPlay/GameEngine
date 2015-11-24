using GameEngine.Attributes;
using GameEngine.Components.Rendering;
using GameEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Extra;
using Microsoft.Xna.Framework;

namespace GameEngine.Comonents.UI
{
    [RequireComponent(typeof(UIRenderer))]
    class Button : Component
    {
        UIRenderer renderer;
        Rectangle AABB;

        public Button(GameObject gameObject)
            : base(gameObject)
        {

        }

        public override void Initialize()
        {
            renderer = GetComponent<UIRenderer>();
            MouseInput.Moved += MouseInput_Moved;
            AABB = renderer.BaseImage.Dimension;
            base.Initialize();
        }

        private void MouseInput_Moved(MouseEventArgs e)
        {
            
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
