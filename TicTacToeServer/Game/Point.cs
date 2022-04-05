using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeServer.Game
{
    internal class Point
    {
        internal Int32 x { get; private set; }
        internal Int32 y { get; private set; }

        public Point(Int32 x, Int32 y)
        {
            this.x = x;
            this.y = y;
        }

        internal Boolean Equals(Point other)
        {
            return x == other.x && y == other.y;
        }

        public override string ToString()
        {
            return $"x - {x}, y: - {y}";
        }
    }
}
