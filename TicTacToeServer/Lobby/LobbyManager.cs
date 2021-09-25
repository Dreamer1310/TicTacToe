using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Models;

namespace TicTacToeServer.Lobby
{
    internal class LobbyManager
    {
        private static Object _queueLock = new();
        private static readonly List<Player<ILobbyClient>> _onlineUsers = new List<Player<ILobbyClient>>();
        private static readonly LobbySender _sender = new LobbySender();

        private static readonly Dictionary<Int64, Queue> _queues = new();
        internal static List<Queue> QueueCollection => _queues.Values.ToList();
        internal static Int32 OnlineUsersCount => _onlineUsers.Count;

        internal static void InitializeGameQueues()
        {
            _queues.Add(1, new Queue
            {
                ID = 1,
                BoadrSize = 3,
                Name = "3x3",
                Till = 3
            });

            _queues.Add(2, new Queue
            {
                ID = 2,
                BoadrSize = 5,
                Name = "5x5",
                Till = 5
            });
        }


        internal static void Seat(Int64 queueId, String connectionId)
        {
            try
            {
                var player = GetPlayerByConnectionId(connectionId);
                if (CanSeat(player))
                {
                    var queue = _queues[queueId];
                    queue.SeatOnQueue(player);
                    if (queue.IsFull)
                    {
                        RecreateQueue(queue);

                        // TODO: Create Game, Send StartGame
                    }
                    _sender.SendYouSetOnQueue(player, queueId);
                    _sender.SendCanSeat(player);
                    _onlineUsers.ForEach(x => _sender.SendQueueData(x));
                }
                else
                {
                    throw new Exception("You can't seat on queue");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.White;
            }

        }

        internal static void SeatOut(Int64 queueId, String connectionId)
        {
            try
            {
                var player = GetPlayerByConnectionId(connectionId);
                var queue = _queues[queueId];
                queue.SeatOut(player);

                _sender.SendYouLeftQueue(player);
                _sender.SendCanSeat(player);
                _onlineUsers.ForEach(x => _sender.SendQueueData(x));
            }
            catch (Exception ex)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        internal static Boolean CanSeat(Player<ILobbyClient> player)
        {
            return !_queues.Values.Any(x => x.Seats.Any(x => x.player?.ConnectionId == player.ConnectionId));
        }


        internal static void AddOnlineUser(Player<ILobbyClient> player)
        {
            _onlineUsers.Add(player);
        }

        internal static Player<ILobbyClient> GetPlayerByConnectionId(String connectionId)
        {
            return _onlineUsers.FirstOrDefault(x => x.ConnectionId == connectionId);
        }


        internal static void PlayerConnected(ILobbyClient caller, String connectionId)
        {
            try
            {
                var player = GetPlayerByConnectionId(connectionId);

                if (player != null)
                {
                    player.Client = caller;
                    player.ConnectionId = connectionId;
                }
                else
                {
                    player = new Player<ILobbyClient>
                    {
                        Client = caller,
                        ConnectionId = connectionId
                    };
                    _onlineUsers.Add(player);
                }
                _sender.SendQueueData(player);
                _sender.SendCanSeat(player);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        internal static void PlayerDisconnected(String connectionId)
        {
            var player = GetPlayerByConnectionId(connectionId);
            if (player != null)
            {
                var queue = _queues.Values.FirstOrDefault(x => x.Seats.Any(x => x.player?.ConnectionId == player.ConnectionId));
                if (queue != null)
                {
                    queue.SeatOut(player);
                    _onlineUsers.ForEach(x => _sender.SendQueueData(x));
                }
            }
            _onlineUsers.Remove(player);
        }

        private static void RecreateQueue(Queue queue)
        {
            _queues[queue.ID] = new Queue
            {
                ID = queue.ID,
                BoadrSize = queue.BoadrSize,
                Name = queue.Name,
                Till = queue.Till
            };
        }


    }
}
