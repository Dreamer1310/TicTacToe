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
    internal class Move
    {
        internal Player<IGameClient> Player { get; private set; }
        internal Point Point { get; private set; }
        internal GameFigures Figure { get; private set; }

        public Move(Int32 x, Int32 y, GameFigures figure, Player<IGameClient> player)
        {
            Point = new Point(x, y);
            Figure = figure;
            Player = player;
        }
    }
}
