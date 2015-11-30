using GameEngine.Components.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Assets;

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
            if (Camera.main == null)
                return;

            if (!active)
            {
                base.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, Camera.main.TransformMatrix);
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
            if (Camera.main == null)
                return;

            if (!active)
            {
                base.Begin(sortMode, blendState, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, Camera.main.TransformMatrix);
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
            if (Camera.main == null)
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
            if (Camera.main == null)
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
            if (Camera.main == null)
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
        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture,
        ///     destination rectangle, and color. Reference page contains links to related
        ///     code samples.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destinationRectangle">A rectangle that specifies (in screen coordinates) the destination for drawing
        ///     the sprite.</param>
        /// <param name="color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        public new void Draw(Texture2D texture, Rectangle destinationRectangle, Color color, bool UI = true)
        {
            if (active)
            {
                if (UI)
                {
                    base.End();
                    base.Begin();
                }
                base.Draw(texture, destinationRectangle, color);
            }
        }
        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture,
        ///    position and color. Reference page contains links to related code samples.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        public new void Draw(Texture2D texture, Vector2 position, Color color, bool UI = true)
        {
            if (active)
            {
                if (UI)
                {
                    base.End();
                    base.Begin();
                }
                base.Draw(texture, position, color);
            }
        }
        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture,
        ///     destination rectangle, source rectangle, and color.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destinationRectangle">A rectangle that specifies (in screen coordinates) the destination for drawing
        ///     the sprite. If this rectangle is not the same size as the source rectangle,
        ///     the sprite will be scaled to fit.</param>
        /// <param name="sourceRectangle">A rectangle that specifies (in texels) the source texels from a texture.
        ///     Use null to draw the entire texture.</param>
        /// <param name="color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        public new void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, bool UI = true)
        {
            if (active)
            {
                if (UI)
                {
                    base.End();
                    base.Begin();
                }
                base.Draw(texture, destinationRectangle, sourceRectangle, color);
            }
        }
        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture,
        ///     position, source rectangle, and color.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="sourceRectangle">A rectangle that specifies (in texels) the source texels from a texture.
        ///     Use null to draw the entire texture.</param>
        /// <param name="color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        public new void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, bool UI = true)
        {
            if (active)
            {
                if (UI)
                {
                    base.End();
                    base.Begin();
                }
                base.Draw(texture, position, sourceRectangle, color);
            }
        }
        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture,
        ///     destination rectangle, source rectangle, color, rotation, origin, effects
        ///     and layer.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destinationRectangle">A rectangle that specifies (in screen coordinates) the destination for drawing
        ///     the sprite. If this rectangle is not the same size as the source rectangle,
        ///     the sprite will be scaled to fit.</param>
        /// <param name="sourceRectangle">A rectangle that specifies (in texels) the source texels from a texture.
        ///     Use null to draw the entire texture.</param>
        /// <param name="color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        /// <param name="rotation">Specifies the angle (in radians) to rotate the sprite about its center.</param>
        /// <param name="origin">The sprite origin; the default is (0,0) which represents the upper-left corner.</param>
        /// <param name="effects">Effects to apply.</param>
        /// <param name="layerDepth">The depth of a layer. By default, 0 represents the front layer and 1 represents
        ///     a back layer. Use SpriteSortMode if you want sprites to be sorted during
        ///     drawing.</param>
        public new void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth, bool UI = true)
        {
            if (active)
            {
                if (UI)
                {
                    base.End();
                    base.Begin();
                }
                base.Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
            }
        }
        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture,
        ///     position, source rectangle, color, rotation, origin, scale, effects, and
        ///     layer. Reference page contains links to related code samples.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="sourceRectangle">A rectangle that specifies (in texels) the source texels from a texture.
        ///     Use null to draw the entire texture.</param>
        /// <param name="color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        /// <param name="rotation">Specifies the angle (in radians) to rotate the sprite about its center.</param>
        /// <param name="origin">The sprite origin; the default is (0,0) which represents the upper-left corner.</param>
        /// <param name="scale">Scale factor.</param>
        /// <param name="effects">Effects to apply.</param>
        /// <param name="layerDepth">The depth of a layer. By default, 0 represents the front layer and 1 represents
        ///     a back layer. Use SpriteSortMode if you want sprites to be sorted during
        ///     drawing.</param>
        public new void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, bool UI = true)
        {
            if (active)
            {
                if (UI)
                {
                    base.End();
                    base.Begin();
                }
                base.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
            }
        }
        /// <summary>
        /// Adds a sprite to a batch of sprites for rendering using the specified texture,
        ///     position, source rectangle, color, rotation, origin, scale, effects and layer.
        ///     Reference page contains links to related code samples.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The location (in screen coordinates) to draw the sprite.</param>
        /// <param name="sourceRectangle">A rectangle that specifies (in texels) the source texels from a texture.
        ///     Use null to draw the entire texture.</param>
        /// <param name="color">The color to tint a sprite. Use Color.White for full color with no tinting.</param>
        /// <param name="rotation">Specifies the angle (in radians) to rotate the sprite about its center.</param>
        /// <param name="origin">The sprite origin; the default is (0,0) which represents the upper-left corner.</param>
        /// <param name="scale">Scale factor.</param>
        /// <param name="effects">Effects to apply.</param>
        /// <param name="layerDepth">The depth of a layer. By default, 0 represents the front layer and 1 represents
        ///     a back layer. Use SpriteSortMode if you want sprites to be sorted during
        ///     drawing.</param>
        public new void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth, bool UI = true)
        {
            if (active)
            {
                if (UI)
                {
                    base.End();
                    base.Begin();
                }
                base.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
            }
        }

        public void DrawText(Font font, string text, Vector2 position, Handle relativeHandle, Color color, bool UI = true)
        {
            if (active)
            {
                if (UI)
                {
                    base.End();
                    base.Begin();
                }

                int charoffset = 0;
                int heightoffset = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    base.Draw(font[text[i]], position + Vector2.UnitX * charoffset + Vector2.UnitY * heightoffset, color);
                    charoffset += font[text[i]].Width;
                }
            }
        }
    }
}