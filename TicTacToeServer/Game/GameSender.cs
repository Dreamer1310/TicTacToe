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
				GridSize = state.BoardSize,
				CurrentPlayerId = state.CurrentPlayer.ID,
				Players = state.Players.Select(x => new PlayerDto(x)).ToList(),
				CurrentRound = state.Rounds == null ? null : new RoundDto
				{
					ID = state.Rounds.LastIndexOf(state.Rounds.Last()) + 1,
					Status = state.Rounds.Last().Status,
					GameBoard = state.Rounds.Last().gameBoard.Select(x => new CellDto
					{
						Point = new PointDto
                        {
							x = x.Point.x,
							y = x.Point.y
                        },
						GameFigure = new GameFigureDto
                        {
							Shape = x.GameFigure.Shape,
							Size = x.GameFigure.Size
                        }
					})
					.ToList(),
				}
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
					.ToDictionary(x => x.ID, x => x.Score),
				WinningLine = state.Rounds.Last().WinningLine?.Select(x => new PointDto
				{
					x = x.x,
					y = x.y
				})
				?.ToList()
	        });
        }

        public void SendGameFinished(Player<IGameClient> player, GameState state, GameFinishReasons finishReason)
        {
	        player.Client.GameFinished(new GameFinishedDto
	        {
		        GameFinishReason = finishReason,
		        WinnerID = state.Winner?.ID,
                Scores = state.Rounds == null ? new Dictionary<String, Int32>() : state.Rounds
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

        internal void SendPlayerMadeMove(Player<IGameClient> player, Move move)
        {
			player.Client.PlayerMadeMove(new MoveDto
			{
				Player = new PlayerDto(player),
				Point = new PointDto
                {
					x = move.Point.x,
					y = move.Point.y
                },
				Figure = new GameFigureDto
                {
					Shape = move.Figure.Shape,
					Size = move.Figure.Size
                }
			});
        }
    }
}
