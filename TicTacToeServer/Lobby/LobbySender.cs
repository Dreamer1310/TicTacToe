using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Communication.Dto;
using TicTacToeServer.Models;

namespace TicTacToeServer.Lobby
{
    internal class LobbySender
    {
        internal void SendQueueData(Player<ILobbyClient> player)
        {
            player.Client.QueueData(LobbyManager.QueueCollection.Select(x => new QueueDto
                {
                    ID = x.ID,
                    Name = x.Name,
                    PlayerCount = x.PlayerCount,
                    BoadrSize = x.BoadrSize,
                    Till = x.Till
                })
                .ToList());
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

        internal void SendPlayers(Player<ILobbyClient> player)
        {
            player.Client.Players(LobbyManager.OnlineUsers.ToDictionary(x => x.ID, x => new PlayerDto(x)));
        }
    }
}
