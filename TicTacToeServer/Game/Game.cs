using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private Boolean _isOnHold;
        internal Int64 ID { get; set; }
        internal List<Player<IGameClient>> Players;

        private Boolean _allReady => Players.All(x => x.Ready);

        private Boolean _isStarted => Status == GameStatus.Started;
        internal Int32 BoardSize;
        internal Func<Int64?> OnGameFinished { get; set; }

        internal GameStatus Status;
        private GameState _state;
        private GameSender _sender;


        public Game(List<Player<IGameClient>> players, GameConfig config)
        {
            Players = players;
            _state = new GameState(config, players);
            _sender = new GameSender();
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
	            }
                else if (!_isStarted)
                {
	                _sender.SendWaitingToJoin(player, Players.Where(x => !x.Ready && x.ID != player.ID).ToList());
                }
	            else if (_isStarted && player.ID == _state.CurrentPlayer.ID && !_isOnHold)
                {
	                _sender.SendAskMove(player);
                }

                if((_state.CurrentPlayer?.Timer?.IsRunning) ?? false && !_isOnHold)
                {
                    _sender.SendWaitingFor(player, _state.CurrentPlayer);
                }
                _sender.SendGameState(player, _state);
            }
        }

        internal void MakeMove(String playerId, Int32 x, Int32 y)
        {
            lock (_sync)
            {
                try
                {
                    CheckHold();

                    var player = GetPlayer(playerId);

                    if (player == null)
                    {
	                    throw new Exception("Invalid Player!");
                    }

                    var move = new Move(x, y, player);

                    if (_state.CurrentPlayer != player)
                    {
                        throw new Exception("Not your turn!");
                    }

                    _state.PlayerMadeMove(move);
                    StopLastTimer();
                    Players.ForEach(x => _sender.SendPlayerMadeMove(x, move));


                    if (_state.GameFinished)
                    {
	                    HoldGame(() =>
	                    {
		                    FinishGame(GameFinishReasons.TillPointsReach);
	                    }, 1000);
	                    return;
                    }

                    if (_state.Rounds.Last().IsFinished)
                    {
	                    HoldGame(() =>
                        {
	                        Players.ForEach(x => _sender.SendRoundFinished(x, _state));
                            NextRound();
                        }, 500);
	                    return;
                    }

                    HoldGame(() =>
                    {
                        AskMove();
                    }, 500);
                    
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void FinishGame(GameFinishReasons finishReason)
        {
	        if (_state.IsFinished)
	        {
		        throw new Exception("Game Is already finished");
	        }

	        _state.IsFinished = true;

	        if (finishReason == GameFinishReasons.PlayerTimedOut)
	        {
                _state.Rounds.Last().FinishRound(RoundFinishReasons.PlayerTimedOut);
                Players.ForEach(x => _sender.SendRoundFinished(x, _state));
            }

            OnGameFinished();
            Status = GameStatus.Finished;

            Players.ForEach(x => _sender.SendGameFinished(x, _state, finishReason));
            

        }

        private void StopLastTimer()
        {
            _state.Players.ForEach(x =>
            {
                if (x.Timer.IsRunning)
                {
                    x.Timer.Pause();
                }
            });
        }

        private void StartGame()
        {
	        if (_isStarted)
	        {
		        return;
	        }
            
            _state.Players.ForEach(x =>
            {
	            x.Timer = new CountDownTimer(30 * 1000, ID, x.ID);
	            x.Timer.OnTimeOut = () =>
	            {
                    x.Timer.Pause();
                    PlayerTimedOut(x);
	            };
            });

            
            _state.StartGame();
            Status = GameStatus.Started;
            Players.ForEach(x => _sender.SendGameStarted(x));
            NextRound();           
        }

        private void NextRound()
        {
            _state.ChangeFigures();
            var round = new Round(BoardSize);
            _state.AddRound(round);

            HoldGame(() =>
            {
                Players.ForEach(x => _sender.SendRoundStarted(x, _state.Rounds.LastIndexOf(_state.Rounds.Last())));
                AskMove();
            }, 500);
        }

        private void StartPlayerTimer()
        {
            _state.CurrentPlayer.Timer.Play();
        }

        private void PlayerTimedOut(Player<IGameClient> player)
        {
	        lock (_sync)
	        {
		        StopLastTimer();

		        HoldGame(() =>
		        {
			        FinishGame(GameFinishReasons.PlayerTimedOut);
		        }, 500);
	        }
        }

        private void HoldGame(Action action, Int32 delay)
        {
            _isOnHold = true;
            Task.Run(() => {
                Thread.Sleep(delay);
                try
                {
                    _isOnHold = false;
                    action.Invoke();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
        }

        private void CheckHold()
        {
            if (_isOnHold)
            {
                throw new Exception("On Hold!");
            }
        }

        private void AskMove()
        {
            StartPlayerTimer();

            _sender.SendAskMove(_state.CurrentPlayer);
            Players.ForEach(x => {
                if (x.ID != _state.CurrentPlayer.ID)
                {
                    _sender.SendWaitingFor(x, _state.CurrentPlayer);
                }
            });
        }
    }
}
