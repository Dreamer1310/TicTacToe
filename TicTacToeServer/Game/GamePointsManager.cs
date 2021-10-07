using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Models;

namespace TicTacToeServer.Game
{
    internal class GamePointsManager
    {
        private Int32 _till;
        private List<Player<IGameClient>> players;
        internal Dictionary<String, Int32> Scores = new Dictionary<String, Int32>();

        public GamePointsManager(Int32 till, List<Player<IGameClient>> players)
        {
            _till = till;
            this.players = players;
        }
    }
}
