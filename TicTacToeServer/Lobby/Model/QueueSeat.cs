using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Models;

namespace TicTacToeServer.Lobby.Model
{
    internal class QueueSeat
    {
        public Player<ILobbyClient> player { get; set; }
    }
}
