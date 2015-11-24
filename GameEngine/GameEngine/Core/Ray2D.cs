using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2D.XNA;

namespace GameEngine.Core
{
    public class Ray2D
    {
        /// <summary>
        /// The position of the ray.
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// The direction of the ray.
        /// </summary>
        public Vector2 Direction;
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>Representive string.</returns>
        public override string ToString()
        {
            return "Position: " + this.Position + ", Direction: " + this.Direction;
        }
    }
}
