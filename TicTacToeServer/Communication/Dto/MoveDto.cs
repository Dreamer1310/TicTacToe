using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Game.Enums;

namespace TicTacToeServer.Communication.Dto
{
    public class MoveDto
    {
        public PlayerDto Player { get; set; }
        public PointDto Point { get; set; }
        public GameFigureDto Figure { get; set; }
    }
}
