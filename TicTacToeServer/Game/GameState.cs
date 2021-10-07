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
        internal Player<IGameClient> CurrentPlayer { get; set; }
        internal List<Player<IGameClient>> Players;
        internal GameFigures[][] GameBoard;


        public GameState(GameConfig config, List<Player<IGameClient>> players)
        {
	        Players = players;

	        for (int i = 0; i < Players.Count; i++)
	        {
		        Players[i].PlayerFigure = (GameFigures)(i + 1);
	        }

	        CurrentPlayer = Players.First(x => x.PlayerFigure == GameFigures.Cross);

	        GameBoard = new GameFigures[config.BoardSize][];

            for (int i = 0; i < config.BoardSize; i++)
	        {
		        var arr = new GameFigures[config.BoardSize];
                for (int j = 0; j < config.BoardSize; j++)
                {
	                arr[j] = GameFigures.None;
                }

                GameBoard[i] = arr;
	        }
        }

        internal void PlayerMadeMode(Move playerMove)
        {

        }

        internal Player<IGameClient> OpponnetOf(Player<IGameClient> player)
        {
            return Players.First(x => x.ID != player.ID);
        }

        internal void StartGame()
        {
	        throw new NotImplementedException();
        }
    }
}
