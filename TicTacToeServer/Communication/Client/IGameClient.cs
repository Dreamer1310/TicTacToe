using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeServer.Communication.Client
{
    public interface IGameClient
    {
        Task Disconnect();

        Task AskMove();
        Task WaitingFor(Object player);
        Task WaitingToJoin(Object players);
        Task GameState(Object state);
        Task GameStarted();
        Task RoundStarted(Int32 roundId);
        Task RoundFinished(Object roundInfo);
        Task GameFinished(Object finalInfo);
        Task OperationError();
    }
}
