using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Models;

namespace TicTacToeServer.Communication.Dto
{
    public class PlayerDto
    {
        public String ID { get; set; }
        public String Name { get; set; }

        public PlayerDto(Player<IGameClient> player)
        {
            ID = player.ID;
            Name = player.ConnectionID;
        }

        public PlayerDto(Player<ILobbyClient> player)
        {
            ID = player.ID;
            Name = player.ConnectionID;
        }
    }
}
