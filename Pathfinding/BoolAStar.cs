using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pathfinding
{
    class BoolAStar : AStar
    {
        public BoolAStar(ref bool[,] boolArray, Point start, Point end, DistanceType distanceType)
        {
            this.start = start;
            this.end = end;
            this.distanceType = distanceType;
            CreateMap(ref boolArray);
        }

        public BoolAStar(ref byte[,] byteArray, Point start, Point end, DistanceType distanceType)
        {
            this.start = start;
            this.end = end;
            this.distanceType = distanceType;
            CreateMap(ref byteArray);
        }

        public override List<Point> Search()
        {
            List<Point> OpenList = new List<Point>();
            List<Point> ClosedList = new List<Point>();

            Node currentNode = map[start.X, start.Y];
            OpenList.Add(currentNode.Location);

            return ClosedList;
        }

        protected override void CreateMap(ref byte[,] byteArray)
        {
            this.dimension = new Point(byteArray.GetLength(0), byteArray.GetLength(1));

            map = new Node[dimension.X, dimension.Y];

            for (int x = 0; x < this.dimension.X; x++)
                for (int y = 0; y < this.dimension.Y; y++)
                {
                    map[x, y] = new Node(new Point(x, y), byteArray[x, y], this.start, this.end, this.distanceType);
                }
        }

        protected void CreateMap(ref bool[,] boolArray)
        {
            this.dimension = new Point(boolArray.GetLength(0), boolArray.GetLength(1));

            map = new Node[dimension.X, dimension.Y];

            for (int x = 0; x < this.dimension.X; x++)
                for (int y = 0; y < this.dimension.Y; y++)
                {
                    map[x, y] = new Node(new Point(x, y), boolArray[x, y], this.start, this.end, this.distanceType);
                }
        }
    }
}
