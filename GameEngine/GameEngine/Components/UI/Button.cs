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

namespace GameEngine.Components.UI
{
    public class Button : UIBase
    {        
        public Button()
            : base(null)
        {
            Initialize();
        }

        public Button(GameObject gameObject)
            : base(gameObject)
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Reset()
        {
            if (hover)
                OnMouseOut();
            hover = false;
        }
    }
}
