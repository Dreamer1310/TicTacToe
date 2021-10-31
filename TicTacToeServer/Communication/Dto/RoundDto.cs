using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Game.Enums;

namespace TicTacToeServer.Communication.Dto
{
    public class RoundDto
    {
        public RoundStatus Status { get; set; }
        public List<CellDto> GameBoard { get; set; }
    }
}
