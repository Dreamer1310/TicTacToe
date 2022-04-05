using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Dto;

namespace TicTacToeServer.Communication.Client
{
    // Actions that can server tell to player in lobby coonection
    public interface ILobbyClient
    {
        Task QueueData(List<QueueDto> queueDto);
        Task YouLeftQueue();
        Task YouSetOnQueue(Int64 queueId);
        Task CanSeat(Boolean canSeat);
        Task StartGame(Int64 gameId);
        Task Players(Dictionary<String, PlayerDto> players);
        Task Disconnect();
    }
}
