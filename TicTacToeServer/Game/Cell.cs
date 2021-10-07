using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Game.Enums;

namespace TicTacToeServer.Game
{
    internal class Cell
    {
        internal Point Point { get; set; }
        internal GameFigures GameFigure { get; set; }
    }
}
