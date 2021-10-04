using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Models;

namespace TicTacToeServer.Game
{
    internal class GameManager
    {
        private static readonly List<Game> _games = new List<Game>();
        internal static List<Game> OnGoingGames => _games;

        internal static void AddGame(Game game) => _games.Add(game);
        internal static void RemoveGame(Game game) => _games.Remove(game);
        
        internal static Game CreateGame(List<Player<IGameClient>> players, GameConfig config)
        {
            var game = new Game(players, config);
            game.ID = 1;
            return game;
        }



    }
}
