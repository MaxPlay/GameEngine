using GameEngine.Assets;
using GameEngine.Core;
using GameEngine.Exception;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components.Rendering
{
    abstract class BaseRenderer : Component
    {

        private bool isRendering;
        public bool IsRendering { get { return this.isRendering; } set { this.isRendering = value; this.registerRenderer(value); } }

        private void registerRenderer(bool value)
        {
            if (Level.main != null)
                throw new LevelNotSetException();

            if (value)
                Level.main.RegisterRenderer(this);

        }

        public BaseRenderer()
            : base(null)
        {

        }

        public BaseRenderer(GameObject gameObject)
            : base(gameObject)
        {

        }

        public abstract override void Reset();

        public abstract void Draw(Matrix TransformMatrix);
    }
}
