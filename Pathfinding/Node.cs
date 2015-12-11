using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Pathfinding
{
    /// <summary>
    /// The way the distance is calculated.
    /// </summary>
    public enum DistanceType
    {
        Octile,
        Chebyshev,
        Manhattan
    }

    /// <summary>
    /// Represents a node in the nodegraph.
    /// </summary>
    public class Node
    {
        private Point location;
        private byte cost;
        private float g;
        private float h;


        /// <summary>
        /// Is true when the node can be used in the heuristic search as walkable space
        /// </summary>
        public bool IsWalkable { get { return this.cost != 0; } set { this.cost = value ? (byte)1 : (byte)0; } }
        /// <summary>
        /// The regular cost is 1, but it can go up if desired. 0 always means that this Node is not walkable (IsWalkable will return false in this case).
        /// </summary>
        public byte Cost { get { return this.cost; } set { this.cost = value; } }
        /// <summary>
        /// The distance from the starting point to this node.
        /// </summary>
        public float G { get { return this.g; } }
        /// <summary>
        /// The distance from the ending point to this node.
        /// </summary>
        public float H { get { return this.h; } }
        /// <summary>
        /// The whole distance from start to end through this node.
        /// </summary>
        public float F { get { return this.h + this.g; } }
        public Point Location { get { return this.location; } }

        public Node(Point location, bool walkable, Point start, Point end, DistanceType distanceType = DistanceType.Chebyshev)
            : this(location, walkable ? (byte)1 : (byte)0, start, end, distanceType)
        {
            //Forwarding constructor
        }

        public Node(Point location, byte cost, Point start, Point end, DistanceType distanceType = DistanceType.Chebyshev)
        {
            this.location = location;
            this.cost = cost;

            switch (distanceType)
            {
                case DistanceType.Octile:
                    g = OHeuristic(end);
                    h = OHeuristic(start);
                    break;
                case DistanceType.Chebyshev:
                    g = CHeuristic(end);
                    h = CHeuristic(start);
                    break;
                case DistanceType.Manhattan:
                    g = MHeuristic(end);
                    h = MHeuristic(start);
                    break;
            }
        }
        /// <summary>
        /// Calculates the octile heuristic value between the given target and the node that called this method.
        /// </summary>
        /// <param name="target">The target point we want to calculate the the heuristic with.</param>
        /// <returns>The octile heuristic value.</returns>
        private float OHeuristic(Point target)
        {
            int dx = Math.Abs(this.location.X - target.X);
            int dy = Math.Abs(this.location.X - target.X);
            float D2 = cost * (float)Math.Sqrt(2);

            return cost * (dx + dy) + (D2 - 2 * cost) * Math.Min(dx, dy);
        }

        /// <summary>
        /// Calculates the manhattan heuristic value between the given target and the node that called this method.
        /// </summary>
        /// <param name="target">The target point we want to calculate the the heuristic with.</param>
        /// <returns>The manhattan heuristic value.</returns>
        private float MHeuristic(Point target)
        {
            int dx = Math.Abs(this.location.X - target.X);
            int dy = Math.Abs(this.location.X - target.X);

            return cost * (dx + dy);
        }

        /// <summary>
        /// Calculates the chebyshev heuristic value between the given target and the node that called this method.
        /// </summary>
        /// <param name="target">The target point we want to calculate the the heuristic with.</param>
        /// <returns>The chebyshev heuristic value.</returns>
        private float CHeuristic(Point target)
        {
            int dx = Math.Abs(this.location.X - target.X);
            int dy = Math.Abs(this.location.X - target.X);

            return cost * (dx + dy) + (cost - 2 * cost) * Math.Min(dx, dy);
        }
    }
}
