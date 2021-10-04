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


        public GameState()
        {

        }

        internal void PlayerMadeMode(Move playerMove)
        {

        }

        internal Player<IGameClient> OpponnetOf(Player<IGameClient> player)
        {
            return Players.First(x => x.ID != player.ID);
        }
    }
}
