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
    internal class Game
    {
        internal List<Player<IGameClient>> Players;
        internal Int32 BoardSize;
        private GameState _state;


        public Game(List<Player<IGameClient>> players, GameConfig config)
        {
            Players = players;
            _state = new GameState();

            // Randomize GameFigures.

            _state.CurrentPlayer = players.First(x => x.PlayerFigure == GameFigures.Cross);
        }

    }
}
