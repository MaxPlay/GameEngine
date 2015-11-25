using GameEngine.Components.Rendering;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Core
{
    public class EngineSpriteBatch : SpriteBatch
    {
        private bool active;
        public bool Active { get { return active; } }

        public EngineSpriteBatch(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
            active = false;
        }
        /// <summary>
        /// Begins a sprite batch operation using deferred sort and default state objects
        ///    (BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None,
        ///    RasterizerState.CullCounterClockwise).
        /// </summary>
        public new void Begin()
        {
            if(Camera.main == null)
                return;

            if (!active)
            {
                base.Begin();
                active = true;
            }
        }
        /// <summary>
        /// Begins a sprite batch operation using the specified sort and blend state
        ///    object and default state objects (DepthStencilState.None, SamplerState.LinearClamp,
        ///    RasterizerState.CullCounterClockwise). If you pass a null blend state, the
        ///    default is BlendState.AlphaBlend.
        /// </summary>
        /// <param name="sortMode">Sprite drawing order.</param>
        /// <param name="blendState">Blending options.</param>
        public new void Begin(SpriteSortMode sortMode, BlendState blendState)
        {
            if(Camera.main == null)
                return;

            if (!active)
            {
                base.Begin(sortMode, blendState, SamplerState.AnisotropicClamp,DepthStencilState.Default,RasterizerState.CullCounterClockwise, null, Camera.main.TransformMatrix);
                active = true;
            }
        }
        /// <summary>
        /// Begins a sprite batch operation using the specified sort, blend, sampler,
        ///    depth stencil and rasterizer state objects. Passing null for any of the state
        ///    objects selects the default default state objects (BlendState.AlphaBlend,
        ///    SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise).
        /// </summary>
        /// <param name="sortMode">Sprite drawing order.</param>
        /// <param name="blendState">Blending options.</param>
        /// <param name="samplerState">Texture sampling options.</param>
        /// <param name="depthStencilState">Depth and stencil options.</param>
        /// <param name="rasterizerState">Rasterization options.</param>
        public new void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState)
        {
            if(Camera.main == null)
                return;

            if (!active)
            {
                base.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, null, Camera.main.TransformMatrix);
                active = true;
            }
        }
        /// <summary>
        /// Begins a sprite batch operation using the specified sort, blend, sampler,
        ///    depth stencil and rasterizer state objects, plus a custom effect. Passing
        ///    null for any of the state objects selects the default default state objects
        ///    (BlendState.AlphaBlend, DepthStencilState.None, RasterizerState.CullCounterClockwise,
        ///    SamplerState.LinearClamp). Passing a null effect selects the default SpriteBatch
        ///    Class shader.
        /// </summary>
        /// <param name="sortMode">Sprite drawing order.</param>
        /// <param name="blendState">Blending options.</param>
        /// <param name="samplerState">Texture sampling options.</param>
        /// <param name="depthStencilState">Depth and stencil options.</param>
        /// <param name="rasterizerState">Rasterization options.</param>
        /// <param name="effect">Effect state options.</param>
        public new void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect)
        {
            if(Camera.main == null)
                return;

            if (!active)
            {
                base.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, Camera.main.TransformMatrix);
                active = true;
            }
        }
        /// <summary>
        /// Begins a sprite batch operation using the specified sort, blend, sampler,
        ///    depth stencil and rasterizer state objects, plus a custom effect. Passing
        ///    null for any of the state objects selects the default default state objects
        ///    (BlendState.AlphaBlend, DepthStencilState.None, RasterizerState.CullCounterClockwise,
        ///    SamplerState.LinearClamp). Passing a null effect selects the default SpriteBatch
        ///    Class shader.
        /// </summary>
        /// <param name="effect">Effect state options.</param>
        public void Begin(Effect effect)
        {
            if(Camera.main == null)
                return;

            if (!active)
            {
                base.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise, effect, Camera.main.TransformMatrix);
                active = true;
            }
            else
            {
                base.End();
                base.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise, effect, Camera.main.TransformMatrix);
            }
        }
        /// <summary>
        /// Flushes the sprite batch and restores the device state to how it was before
        /// Begin was called.
        /// </summary>
        public new void End()
        {
            if (active)
                base.End();

            active = false;
        }
    }
}