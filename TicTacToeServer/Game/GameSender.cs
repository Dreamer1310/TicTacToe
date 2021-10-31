using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Communication.Dto;
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
		    player.Client.WaitingFor(new PlayerDto(waitingFor));
	    }

	    public void SendWaitingToJoin(Player<IGameClient> player, List<Player<IGameClient>> notJoinedPlayers)
	    {
		    player.Client.WaitingToJoin(notJoinedPlayers.Select(x => new PlayerDto(x)).ToList());
	    }

        internal void SendGameState(Player<IGameClient> player, GameState state)
        {
	        player.Client.GameState(new StateDto
			{
				CurrentPlayerId = state.CurrentPlayer.ID,
				Players = state.Players.Select(x => new PlayerDto(x)).ToList(),
				CurrentRound = new RoundDto
				{
					Status = state.Rounds.Last().Status,
					GameBoard = state.Rounds.Last().gameBoard.Select(x => new CellDto
					{
						Point = new PointDto
                        {
							x = x.Point.x,
							y = x.Point.y
                        },
						GameFigure = x.GameFigure
					})
					.ToList(),
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
	        player.Client.RoundFinished(new RoundFinishedDto
	        {
                RoundFinishReason = state.Rounds.Last().FinishReason,
                WinnerID = state.Rounds.Last().Winner?.ID,
                Scores = state.Rounds
	                .Where(x => x.Winner != null)
	                .GroupBy(x => x.Winner.ID)
	                .Select(x => new
	                {
                        ID = x.Key,
                        Score = x.Count()
	                })
					.ToDictionary(x => x.ID, x => x.Score)
	        });
        }

        public void SendGameFinished(Player<IGameClient> player, GameState state, GameFinishReasons finishReason)
        {
	        player.Client.GameFinished(new GameFinishedDto
	        {
		        GameFinishReason = finishReason,
		        WinnerID = state.Winner?.ID,
                Scores = state.Rounds
	                .Where(x => x.Winner != null)
	                .GroupBy(x => x.Winner.ID)
	                .Select(x => new
	                {
		                ID = x.Key,
		                Score = x.Count()
	                })
					.ToDictionary(x => x.ID, x => x.Score)
            });
        }
    }
}
