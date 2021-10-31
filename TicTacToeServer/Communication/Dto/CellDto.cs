using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Game;
using TicTacToeServer.Game.Enums;

namespace TicTacToeServer.Communication.Dto
{
    public class CellDto
    {
        public PointDto Point { get; set; }
        public GameFigures GameFigure { get; set; }
    }
}
