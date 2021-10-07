using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Models;

namespace TicTacToeServer.Game
{
    internal class GameSender
    {
	    public void SendAskMove(Player<IGameClient> player)
	    {
		    throw new NotImplementedException();
	    }

	    public void SendWaitingFor(Player<IGameClient> player, Player<IGameClient> waitingFor)
	    {
		    throw new NotImplementedException();
	    }

	    public void SendWaitingToJoin(Player<IGameClient> player, List<Player<IGameClient>> notJoinedPlayers)
	    {
		    throw new NotImplementedException();
	    }
    }
}
