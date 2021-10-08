using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Game.Enums;
using TicTacToeServer.Models;

namespace TicTacToeServer.Game
{
    internal class GameSender
    {
	    public void SendAskMove(Player<IGameClient> player)
	    {
		    player.Client.AskMove();
	    }

	    public void SendWaitingFor(Player<IGameClient> player, Player<IGameClient> waitingFor)
	    {
		    player.Client.WaitingFor(new
		    {
                ID = waitingFor.ID
		    });
	    }

	    public void SendWaitingToJoin(Player<IGameClient> player, List<Player<IGameClient>> notJoinedPlayers)
	    {
		    player.Client.WaitingToJoin(notJoinedPlayers.Select(x => new
		    {
                ID = x.ID
		    }));
	    }

        internal void SendGameState(Player<IGameClient> player, GameState state)
        {
	        player.Client.GameState(new
	        {
                CurrentPlayerId = state.CurrentPlayer.ID,
                Players = state.Players.Select(x => new {ID = x.ID}),
                CurrentRound = new
                {
                    state.Rounds.Last().Status,
                    state.Rounds.Last().gameBoard,
                },
                CurrentRoundId = state.Rounds.LastIndexOf(state.Rounds.Last()) + 1
	        });
        }

        internal void SendGameData(Player<IGameClient> player)
        {
            throw new NotImplementedException();
        }

        internal void SendGameStarted(Player<IGameClient> player)
        {
	        player.Client.GameStarted();
        }

        internal void SendRoundStarted(Player<IGameClient> player, Int32 roundIndex)
        {
	        player.Client.RoundStarted(roundIndex + 1);
        }

        internal void SendRoundFinished(Player<IGameClient> player, GameState state)
        {
	        player.Client.RoundFinished(new
	        {
                RoundFinishReason = state.Rounds.Last().FinishReason,
                WinnerId = state.Rounds.Last().Winner?.ID,
                Scores = state.Rounds
	                .Where(x => x.Winner != null)
	                .GroupBy(x => x.Winner.ID)
	                .Select(x => new
	                {
                        ID = x.Key,
                        Score = x.Count()
	                })
	        });
        }

        public void SendGameFinished(Player<IGameClient> player, GameState state, GameFinishReasons finishReason)
        {
	        player.Client.GameFinished(new
	        {
		        GameFinishReason = finishReason,
		        WinnerId = state.Winner?.ID,
                TotalScores = state.Rounds
	                .Where(x => x.Winner != null)
	                .GroupBy(x => x.Winner.ID)
	                .Select(x => new
	                {
		                ID = x.Key,
		                Score = x.Count()
	                })
            });
        }
    }
}
