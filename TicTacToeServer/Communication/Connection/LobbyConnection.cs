using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Lobby;
using TicTacToeServer.Models;

namespace TicTacToeServer.Communication.Connection
{
    public class LobbyConnection : Hub<ILobbyClient>
    {
        public override Task OnConnectedAsync()
        {
            LobbyManager.PlayerConnected(Clients.Caller, Context.ConnectionId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("User Succesfully Connected!");
            Console.ForegroundColor = ConsoleColor.White;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            LobbyManager.PlayerDisconnected(Context.ConnectionId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("User Succesfully Disconnected!");
            Console.ForegroundColor = ConsoleColor.White;
            return base.OnDisconnectedAsync(exception);
        }

        public void Seat(Int64 queueId)
        {
            LobbyManager.Seat(queueId, Context.ConnectionId);
        }

        public void SeatOut(Int64 queueId)
        {
            LobbyManager.SeatOut(queueId, Context.ConnectionId);
        }
    }
}
