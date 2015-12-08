using GameEngine.Components;
using GameEngine.Attributes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Core;

namespace GameEngine.Components
{
    /// <summary>
    /// The Transform-Component is used to locate the GameObject in the world and control its rotation and scale. There can only be one Transform on a GameObject and it can't be removed.
    /// </summary>
    [SingleInstanceComponent]
    public class Transform : Component
    {
        private List<Transform> children;

        private Vector2 position;
        private float rotation;
        private Vector2 scale;

        /// <summary>
        /// The position of the Transform, ignoring the offset of the parent.
        /// </summary>
        public Vector2 LocalPosition { get { return this.position * Settings.Unit; } set { this.position = value / Settings.Unit; } }
        /// <summary>
        /// The position of the Transform.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                if (parent != null)
                    return new Vector2(
                        parent.CosineOfRotation * LocalPosition.X - parent.SineOfRotation * LocalPosition.Y,
                        parent.SineOfRotation * LocalPosition.X + parent.CosineOfRotation * LocalPosition.Y
                        ) + parent.Position;
                return this.LocalPosition;
            }
            set
            {
                this.LocalPosition = value;
            }
        }

        /// <summary>
        /// The rotation of the Transform, ignoring the offset of the parent.
        /// </summary>
        public float LocalRotation
        {
            get { return this.rotation; }
            set
            {
                this.rotation = value;
                this.SineOfRotation = (float)Math.Sin(this.rotation);
                this.CosineOfRotation = (float)Math.Cos(this.rotation);
            }
        }
        /// <summary>
        /// The rotation of the Transform.
        /// </summary>
        public float Rotation
        {
            get
            {
                if (parent != null)
                {
                    return this.rotation + parent.Rotation;
                }
                return this.rotation;
            }
            set
            {
                if (parent != null)
                    this.rotation = value - parent.Rotation;
                else
                    this.rotation = value;

                this.SineOfRotation = (float)Math.Sin(this.rotation);
                this.CosineOfRotation = (float)Math.Cos(this.rotation);
            }
        }

        /// <summary>
        /// The scale of the Transform, ignoring the offset of the parent.
        /// </summary>
        public Vector2 LocalScale { get { return this.scale; } set { this.scale = value; } }
        /// <summary>
        /// The scale of the Transform.
        /// </summary>
        public Vector2 LossyScale
        {
            get
            {
                if (parent == null)
                    return this.scale;
                if (parent.LossyScale == Vector2.Zero)
                    return Vector2.Zero;
                return this.scale / parent.LossyScale;
            }
        }

        //Speedups for the Sine/Cosine calculations each frame.
        private float SineOfRotation;
        private float CosineOfRotation;

        /// <summary>
        /// The up-vector of the Transform.
        /// </summary>
        public Vector2 Up { get { return new Vector2(-SineOfRotation, CosineOfRotation); } }
        /// <summary>
        /// The down-vector of the Transform.
        /// </summary>
        public Vector2 Down { get { return new Vector2(SineOfRotation, -CosineOfRotation); } }
        /// <summary>
        /// The right-vector of the Transform.
        /// </summary>
        public Vector2 Right { get { return new Vector2(CosineOfRotation, SineOfRotation); } }
        /// <summary>
        /// The left-vector of the Transform.
        /// </summary>
        public Vector2 Left { get { return new Vector2(-CosineOfRotation, -SineOfRotation); } }

        Transform parent;
        /// <summary>
        /// The parent of the Transform.
        /// </summary>
        public Transform Parent
        {
            get { return this.parent; }
            set
            {
                if (parent != null)
                    parent.children.Remove(this);

                parent = value;

                if (parent != null)
                    parent.children.Add(this);
            }
        }

        public Transform()
            : base(null)
        {

        }

        public Transform(GameObject gameObject)
            : base(gameObject)
        {
            if (gameObject.GetComponent<Transform>() != null)
            {

            }

            this.children = new List<Transform>();
            this.position = Vector2.Zero;
            this.rotation = 0f;
            this.scale = Vector2.One;

            this.SineOfRotation = (float)Math.Sin(rotation);
            this.CosineOfRotation = (float)Math.Cos(rotation);
        }

        /// <summary>
        /// Resets the Component to its default values.
        /// </summary>
        public override void Reset()
        {
            this.children.Clear();
            this.position = Vector2.Zero;
            this.rotation = 0f;
            this.scale = Vector2.One;

            this.SineOfRotation = (float)Math.Sin(rotation);
            this.CosineOfRotation = (float)Math.Cos(rotation);
        }

        /// <summary>
        /// Rotates the Transform.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        public void Rotate(float degrees)
        {
            this.rotation += MathHelper.ToRadians(degrees);
        }
        /// <summary>
        /// Translates the Transform
        /// </summary>
        /// <param name="translation">The offset of the translation.</param>
        public void Translate(Vector2 translation, Space relativeSpace = Space.World)
        {
            if (relativeSpace == Space.World)
                this.position += translation;
            else
                this.position += new Vector2(
                        this.CosineOfRotation * translation.X - this.SineOfRotation * translation.Y,
                        this.SineOfRotation * translation.X + this.CosineOfRotation * translation.Y
                        );
        }
        /// <summary>
        /// Translates the Transform.
        /// </summary>
        /// <param name="x">The x-offset of the translation.</param>
        /// <param name="y">The y-offset of the translation.</param>
        public void Translate(float x, float y, Space relativeSpace = Space.World)
        {
            Translate(new Vector2(x, y), relativeSpace);
        }
        /// <summary>
        /// Scales the Transform.
        /// </summary>
        /// <param name="scale">The scale multiplier.</param>
        public void Scale(float scale)
        {
            this.scale *= scale;
        }
        /// <summary>
        /// Scales the Transform.
        /// </summary>
        /// <param name="scale">The scale multiplier.</param>
        public void Scale(Vector2 scale)
        {
            this.scale *= scale;
        }
        /// <summary>
        /// Scales the Transform.
        /// </summary>
        /// <param name="x">The x-scale multiplier</param>
        /// <param name="y">The y-scale multiplier</param>
        public void Scale(float x, float y)
        {
            this.scale *= new Vector2(x, y);
        }

        public string ToString(string format)
        {
            switch (format)
            {
                case "full":
                    return this.position.ToString() + "; " + rotation.ToString() + "; " + scale.ToString();
                default:
                    return base.ToString();
            }
        }
    }
}
