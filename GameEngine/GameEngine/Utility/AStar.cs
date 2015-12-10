using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.Utility
{
    public static class AStar
    {
        public static List<Point> Search(Point dimension, ref bool[] map, Point start, Point end)
        {
            List<Point> OpenList = new List<Point>();
            List<Point> ClosedList = new List<Point>();

            return ClosedList;
        }

        public static List<Point> Search(Point dimension, ref bool[,] map, Point start, Point end)
        {
            List<Point> OpenList = new List<Point>();
            List<Point> ClosedList = new List<Point>();

            return ClosedList;
        }

        public static bool CheckPath(Point dimension, ref bool[] map, Point start, Point end)
        {
            return (Search(dimension, ref map, start, end).Count != 0);
        }

        public static bool CheckPath(Point dimension, ref bool[,] map, Point start, Point end)
        {
            return (Search(dimension, ref map, start, end).Count != 0);
        }
    }
}
