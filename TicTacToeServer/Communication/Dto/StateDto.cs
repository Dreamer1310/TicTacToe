using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;

namespace TicTacToeServer.Communication.Dto
{
    public class StateDto
    {
        public String CurrentPlayerId { get; set; }
        public List<PlayerDto> Players { get; set; }
        public RoundDto CurrentRound { get; set; }
    }
}
