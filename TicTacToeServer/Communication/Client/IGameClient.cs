using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Dto;
using TicTacToeServer.Game.Enums;

namespace TicTacToeServer.Communication.Client
{
    // Actions that can server tell to player in game coonection
    public interface IGameClient
    {
        Task Disconnect();
        Task AskMove();
        Task WaitingFor(PlayerDto player);
        Task WaitingToJoin(List<PlayerDto> players);
        Task GameState(StateDto state);
        Task GameStarted();
        Task RoundStarted(Int32 roundId);
        Task RoundFinished(RoundFinishedDto roundInfo);
        Task GameFinished(GameFinishedDto finalInfo);
        Task OperationError();
        Task PlayerMadeMove(MoveDto move);
        Task PlayerInfo(PlayerDto playerInfo);
    }
}
