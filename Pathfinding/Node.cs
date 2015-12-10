using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Pathfinding
{
    internal class Node
    {
        private Point location;
        private bool isWalkable;
        private float g;
        private float h;

        public bool IsWalkable { get { return this.isWalkable; } set { this.isWalkable = value; } }
        public float G { get { return this.g; } }
        public float H { get { return this.h; } }
        public float F { get { return this.h + this.g; } }
        public Point Location { get { return this.location; } }
    }
}
