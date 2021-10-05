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
        private Object _sync = new();
        internal Int64 ID { get; set; }
        internal List<Player<IGameClient>> Players;
        internal Int32 BoardSize;
        private GameState _state;


        public Game(List<Player<IGameClient>> players, GameConfig config)
        {
            Players = players;
            _state = new GameState();

            // Randomize GameFigures.

            //_state.CurrentPlayer = players.FirstOrdefa(x => x.PlayerFigure == GameFigures.Cross);
        }

        internal Player<IGameClient> GetPlayer(String userId)
        {
            return Players.FirstOrDefault(x => x.ID == userId);
        }

        internal void Join(IGameClient client, String userId, String connectionId)
        {
            lock (_sync)
            {
                var player = GetPlayer(userId);
                if (player == null)
                {
                    throw new InvalidOperationException("Invalid player for this table!");
                }

                player.Client = client;
                player.ConnectionID = connectionId;
            }
        }
    }
}
