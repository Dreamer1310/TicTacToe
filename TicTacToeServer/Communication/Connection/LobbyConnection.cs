using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Lib;
using TicTacToeServer.Lobby;
using TicTacToeServer.Models;

namespace TicTacToeServer.Communication.Connection
{
    public class LobbyConnection : Hub<ILobbyClient>
    {
        public override Task OnConnectedAsync()
        {
            try
            {
                LobbyManager.PlayerConnected(Clients.Caller, Context.UserIdentifier, Context.ConnectionId);
                return base.OnConnectedAsync();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                LobbyManager.PlayerDisconnected(Context.UserIdentifier);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public void Seat(Int64 queueId)
        {
            LobbyManager.Seat(queueId, Context.UserIdentifier);
        }

        public void SeatOut(Int64 queueId)
        {
            LobbyManager.SeatOut(queueId, Context.UserIdentifier);
        }
    }
}
