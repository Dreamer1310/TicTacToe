using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Dto;

namespace TicTacToeServer.Communication.Client
{
    public interface ILobbyClient
    {
        Task QueueData(List<QueueDto> queueDto, Int64 number = 3);
        Task YouLeftQueue();
        Task YouSetOnQueue(Int64 queueId);
        Task CanSeat(Boolean canSeat);
        Task StartGame(Int64 gameId);
        Task Started(String msg = "Started");
        Task Stopped(String msg = "Stopped");
        Task Players(Dictionary<String, PlayerDto> players);
        Task Disconnect();
    }
}
