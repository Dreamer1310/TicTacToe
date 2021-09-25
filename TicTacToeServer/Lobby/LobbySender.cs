using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Models;

namespace TicTacToeServer.Lobby
{
    internal class LobbySender
    {
        internal void SendQueueData(Player<ILobbyClient> player)
        {
            player.Client.QueueData(new 
            {
                Queues = LobbyManager.QueueCollection.Select(x => new
                {
                    x.ID,
                    x.Name,
                    x.PlayerCount,
                    x.BoadrSize,
                    x.Till
                }).ToList()
            });
        }
        internal void SendYouSetOnQueue(Player<ILobbyClient> player, Int64 queueId)
        {
            player.Client.YouSetOnQueue(queueId);
        }
        internal void SendYouLeftQueue (Player<ILobbyClient> player)
        {
            player.Client.YouLeftQueue();
        }
        internal void SendCanSeat(Player<ILobbyClient> player)
        {
            player.Client.CanSeat(LobbyManager.CanSeat(player));
        }
    }
}
