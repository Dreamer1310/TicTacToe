using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Game.Enums;
using TicTacToeServer.Models;

namespace TicTacToeServer.Game
{
    internal class GameState
    {
        private readonly GameConfig _config;
        internal Player<IGameClient> CurrentPlayer { get; set; }
        internal List<Player<IGameClient>> Players { get; set; }
        internal List<Round> Rounds { get; set; }
        internal GamePointsManager PointsManager { get; private set; }
        internal Boolean GameFinished => Rounds.Count == _config.Till && Rounds.Last().IsFinished;
        internal Boolean IsFinished { get; set; }

        internal Player<IGameClient> Winner { get; set; }

        public GameState(GameConfig config, List<Player<IGameClient>> players)
        {
            _config = config;
            IsFinished = false;
	        Players = players;
            PointsManager = new GamePointsManager(config.Till, players);

	        for (int i = 0; i < Players.Count; i++)
	        {
		        Players[i].PlayerFigure = (GameFigures)(i + 1);
	        }

	        CurrentPlayer = Players.First(x => x.PlayerFigure == GameFigures.Cross);
        }

        internal Player<IGameClient> OpponentOf(Player<IGameClient> player)
        {
            return Players.First(x => x.ID != player.ID);
        }

        internal void StartGame()
        {
            Rounds = new List<Round>();
        }

        internal void AddRound(Round round)
        {
            Rounds.Add(round);
        }

        internal void ChangeFigures()
        {
            var temp = Players[0].PlayerFigure;
            Players[0].PlayerFigure = Players[1].PlayerFigure;
            Players[1].PlayerFigure = temp;
        }

        internal void PlayerMadeMove(Move move)
        {
            try
            {
                var round = Rounds.Last();
                round.MakeMove(move);
                CurrentPlayer = OpponentOf(move.Player);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal void SendPlayerMadeMove(Player<IGameClient> player, Move move)
        {
            throw new NotImplementedException();
        }
    }
}
