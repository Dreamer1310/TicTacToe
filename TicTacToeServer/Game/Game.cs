using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Game.Enums;
using TicTacToeServer.Lib;
using TicTacToeServer.Models;

namespace TicTacToeServer.Game
{
    internal class Game
    {
        private readonly Object _sync = new();
        internal Int64 ID { get; set; }
        internal List<Player<IGameClient>> Players;

        private Boolean _allReady => Players.All(x => x.Ready);

        private Boolean _isStarted => Status == GameStatus.Started;
        internal Int32 BoardSize;
        internal Action GameFinished;

        internal GameStatus Status;
        private GameState _state;

        private GameSender _sender;


        public Game(List<Player<IGameClient>> players, GameConfig config)
        {
            Players = players;
            _state = new GameState(config, players);
            BoardSize = config.BoardSize;
            Status = GameStatus.NotStarted;
        }

        internal Player<IGameClient> GetPlayer(String userId)
        {
            return Players.FirstOrDefault(x => x.ID == userId);
        }

        internal void Join(Player<IGameClient> player)
        {
            lock (_sync)
            {
	            player.Ready = true;

                if (_allReady && !_isStarted)
	            {
		            StartGame();
		            Status = GameStatus.Started;
	            }
                else if (!_isStarted)
                {
	                _sender.SendWaitingToJoin(player, Players.Where(x => !x.Ready && x.ID != player.ID).ToList());
                }
	            else if (_isStarted && player == _state.CurrentPlayer)
                {
	                _sender.SendAskMove(player);
                    return;
                }

                _sender.SendWaitingFor(player, _state.CurrentPlayer);
            }
        }

        private void StartGame()
        {
	        if (_isStarted)
	        {
		        return;
	        }
            
            _state.Players.ForEach(x =>
            {
	            x.Timer = new CountDownTimer(30000, ID, x.ID);
	            x.Timer.OnTimeOut = () =>
	            {
                    x.Timer.Pause();
                    PlayerTimedOut(x);
	            };
            });

            _state.StartGame();

            _sender.SendAskMove(_state.CurrentPlayer);
        }

        private void PlayerTimedOut(Player<IGameClient> player)
        {
	        throw new NotImplementedException();
        }
    }
}
