using GameEngine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components.Rendering
{
    class Camera : Component
    {
        /// <summary>
        /// The active main Camera.
        /// </summary>
        public static Camera main;

        private Color backgroundColor;
        private float zoom;
        private Rectangle bounds;

        /// <summary>
        /// The color that will be used as Clear-Color.
        /// </summary>
        public Color BackgroundColor { get { return this.backgroundColor; } set { this.backgroundColor = value; } }

        /// <summary>
        /// The Transformation Matrix of the 2D-Camera
        /// </summary>
        public Matrix TransformMatrix
        {
            get
            {
                return 
                    Matrix.CreateTranslation(new Vector3(-this.Transform.Position.X, -this.Transform.Position.Y, 0)) *
                    Matrix.CreateRotationZ(this.Transform.Rotation) *
                    Matrix.CreateScale(this.zoom) *
                    Matrix.CreateTranslation(new Vector3(this.bounds.Width * 0.5f, this.bounds.Height * 0.5f, 0)
                    );
            }
        }
        /// <summary>
        /// The current Zoomvalue
        /// </summary>
        public float Zoom { get { return this.zoom; } set { this.zoom = value; } }
        /// <summary>
        /// The Viewport in the real world
        /// </summary>
        public Viewport Viewport
        {
            get
            {
                return
                    new Viewport(
                        (int)(this.bounds.Width * -0.5f * (1f / this.zoom) + Transform.Position.X),
                        (int)(this.bounds.Height * -0.5f * (1f / this.zoom) + Transform.Position.Y),
                        (int)(this.bounds.Width * (1f / this.zoom)),
                        (int)(this.bounds.Height * (1f / this.zoom))
                        );
            }
        }

        public Camera()
            : base(null)
        {
            if (main == null)
                main = this;

            this.bounds = new Rectangle(0, 0, Bootstrap.graphics.PreferredBackBufferWidth, Bootstrap.graphics.PreferredBackBufferHeight);
            this.zoom = 1;
        }

        public Camera(GameObject gameObject)
            : base(gameObject)
        {
            if (main == null)
                main = this;

            this.bounds = new Rectangle(0, 0, Bootstrap.graphics.PreferredBackBufferWidth, Bootstrap.graphics.PreferredBackBufferHeight);
            this.zoom = 1;
        }

        /// <summary>
        /// Resets the camera to its default values.
        /// </summary>
        public override void Reset()
        {
            this.bounds = new Rectangle(0, 0, Bootstrap.graphics.PreferredBackBufferWidth, Bootstrap.graphics.PreferredBackBufferHeight);
            this.zoom = 1;
        }

        /// <summary>
        /// This method is used to get the Mouseposition on the world, translated by the camera. If the camera is moved, the position of the mouse in the world will change as well.
        /// </summary>
        /// <returns>A Vector2 for the translated MousePosition.</returns>
        public Vector2 TranslatedCursorPosition()
        {
            return new Vector2(Mouse.GetState().X, Mouse.GetState().Y) + this.Transform.Position - new Vector2(this.bounds.Width * 0.5f, this.bounds.Height * 0.5f);
        }
    }
}
