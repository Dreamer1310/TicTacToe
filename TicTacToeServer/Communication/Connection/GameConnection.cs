using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Game;

namespace TicTacToeServer.Communication.Connection
{
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
            var game = GameManager.GetGameByUserId(Context.UserIdentifier);

            if(game == null)
            {
                throw new Exception("Invalid Game!");
            }

            game.Join(Clients.Caller, Context.UserIdentifier, Context.ConnectionId);
        }
    }
}
