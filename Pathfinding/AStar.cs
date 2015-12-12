using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Pathfinding
{
    public abstract class AStar
    {
        protected Point dimension;
        protected Point start;
        protected Point end;
        protected DistanceType distanceType;

        protected Node[,] map;

        public Point Dimension { get { return this.Dimension; } }
        public Point Start { get { return this.start; } set { this.start = value; } }
        public Point End { get { return this.end; } set { this.end = value; } }
        public Node this[int index1, int index2] { get { return map[index1, index2]; } }
        public DistanceType DistanceType { get { return this.distanceType; } }

        public static AStar Create(ref bool[,] boolArray, Point start, Point end, DistanceType distanceType = DistanceType.Chebyshev)
        {
            return new BoolAStar(ref boolArray, start, end, distanceType);
        }

        public static AStar Create(ref byte[,] byteArray, Point start, Point end, DistanceType distanceType = DistanceType.Chebyshev)
        {
            return new ByteAStar(ref byteArray, start, end, distanceType);
        }

        public static AStar Create(ref byte[,] byteArray, Point start, Point end, bool asBool, DistanceType distanceType = DistanceType.Chebyshev)
        {
            if (asBool)
                return new BoolAStar(ref byteArray, start, end, distanceType);
            else
                return Create(ref byteArray, start, end, distanceType);
        }

        public bool CheckPath()
        {
            return (this.Search().Count != 0);
        }

        public abstract List<Point> Search();

        protected abstract void CreateMap(ref byte[,] byteArray);
    }
}
