using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Communication.Dto;
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

        public void TestDto(DemoDto demo)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(demo.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void TestDictionary(Dictionary<Int64, QueueDto> queues)
        {
            // todo: BoardSize was 0!!!


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(queues.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void TestList(List<DemoDto> demos)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(demos.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
