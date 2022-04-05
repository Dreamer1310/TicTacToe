using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Communication.Dto;
using TicTacToeServer.Game;
using TicTacToeServer.Game.Enums;

namespace TicTacToeServer.Communication.Connection
{
    // Game connection. Actions that player call on server.
    public class GameConnection : Hub<IGameClient>
    {
        public override Task OnConnectedAsync()
        {
            var game = GameManager.GetGameByUserId(Context.UserIdentifier);

            if (game != null)
            {
                var player = game.GetPlayer(Context.UserIdentifier);
                if (player.Client != null)
                {
                    player.Client.Disconnect();
                    player.ConnectionID = Context.ConnectionId;
                    player.Client = Clients.Caller;
                }
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public void Join()
        {
	        GameManager.Join(Clients.Caller, Context.UserIdentifier, Context.ConnectionId);
        }

        public void MakeMove(PointDto point, GameShapes shape, FigureSizes size)
        {
	        var userId = Context.UserIdentifier;
	        var game = GameManager.GetGameByUserId(userId);

	        if (game == null)
	        {
		        Clients.Caller.OperationError();
		        return;
	        }

	        game.MakeMove(userId, point.x, point.y, shape, size);
        }
    }
}
