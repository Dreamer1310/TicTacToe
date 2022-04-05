using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Game.Enums;

namespace TicTacToeServer.Communication.Dto
{
    public class GameFigureDto
    {
        public GameShapes Shape { get; set; }
        public FigureSizes Size { get; set; }
    }
}
