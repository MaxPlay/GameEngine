using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding
{
    public abstract class AStar
    {
        protected AStar()
        {

        }

        public static AStar Create()
        {
            return new BoolAStar();
        }
    }
}
