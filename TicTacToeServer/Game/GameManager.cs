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
	    private static Int64 _gameId = 1;
        private static readonly Dictionary<Int64, Game> _games = new Dictionary<Int64, Game>();
        internal static List<Game> OnGoingGames => _games.Values.ToList();
        internal static void AddGame(Game game) => _games.Add(game.ID, game);
        internal static void RemoveGame(Game game) => _games.Remove(game.ID);
        
        internal static Game GetGameByUserId(String userId)
        {
            return _games.Values.FirstOrDefault(x => x.Players.Any(x => x.ID == userId));
        }
        
        internal static Game CreateGame(List<Player<IGameClient>> players, GameConfig config)
        {
            var game = new Game(players, config);
            game.ID = _gameId;
            _gameId++;

            game.OnGameFinished = () =>
            {
	            Task.Run(() =>
	            {
                    RemoveGame(game);
	            });
	            return null;
            };

            AddGame(game);

            return game;
        }

        internal static void Join(IGameClient client, String userId, String connectionId)
        {
	        var game = GameManager.GetGameByUserId(userId);

	        if (game == null)
	        {
		        throw new Exception("Invalid Game!");
	        }

	        var player = game.GetPlayer(userId);
	        if (player == null)
	        {
		        throw new InvalidOperationException("Invalid player for this table!");
	        }

	        player.Client = client;
	        player.ConnectionID = connectionId;

            game.Join(player);
        }
    }
}
