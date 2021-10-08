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
    internal class Round
    {
        private readonly Int32 _boardSize;
        private readonly List<Cell> _gameBoard;

        internal List<Cell> gameBoard => _gameBoard; 
        internal Player<IGameClient> Winner { get; private set; } 
        internal List<Point> WinningLine { get; private set; }
        internal RoundFinishReasons FinishReason { get; private set; }
        internal RoundStatus Status { get; private set; }
        internal Boolean IsFinished => Status == RoundStatus.Finished;
        internal List<Move> Moves;

        public Round(Int32 boardSize)
        {
            _boardSize = boardSize;
            Moves = new List<Move>();
            _gameBoard = new List<Cell>(_boardSize * _boardSize);
            Status = RoundStatus.NotFinished;

            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    var cell = new Cell();
                    cell.Point = new Point(i, j);
                    cell.GameFigure = GameFigures.None;
                    _gameBoard[i * _boardSize + j] = cell;
                }
            }
        }

        internal void MakeMove(Move move)
        {
            try
            {
                ValidateMove(move);
                _gameBoard[move.Point.x * _boardSize + move.Point.y].GameFigure = move.Figure;
                Moves.Add(move);

                CheckBoard();
            }
            catch (Exception ex)
            {
                throw;
            }            
        }

        internal void FinishRound(RoundFinishReasons finishReason)
        {
            FinishReason = finishReason;
            Status = RoundStatus.Finished;
            Winner = null;
            if (FinishReason != RoundFinishReasons.Tie)
            {
                Winner = Moves.Last().Player;
            }
        }

        private void CheckBoard()
        {
            WinningLine = null;
            var move = Moves.Last();

            if (_gameBoard.Where(x => x.Point.x == move.Point.x).All(x => x.GameFigure == move.Figure))
            {
                WinningLine = _gameBoard.Where(x => x.Point.x == move.Point.x).Select(x => x.Point).ToList();
                FinishRound(RoundFinishReasons.PlayerWon);
                return;
            }

            if (_gameBoard.Where(x => x.Point.y == move.Point.y).All(x => x.GameFigure == move.Figure))
            {
                WinningLine = _gameBoard.Where(x => x.Point.y == move.Point.y).Select(x => x.Point).ToList();
                FinishRound(RoundFinishReasons.PlayerWon);
                return;
            }

            if (_gameBoard.Where(x => x.Point.x == x.Point.y).All(x => x.GameFigure == move.Figure))
            {
                WinningLine = _gameBoard.Where(x => x.Point.x == x.Point.y).Select(x => x.Point).ToList();
                FinishRound(RoundFinishReasons.PlayerWon);
                return;
            }

            if (_gameBoard.Where(x => x.Point.x + x.Point.y == _boardSize - 1).All(x => x.GameFigure == move.Figure))
            {
                WinningLine = _gameBoard.Where(x => x.Point.x + x.Point.y == _boardSize - 1).Select(x => x.Point).ToList();
                FinishRound(RoundFinishReasons.PlayerWon);
                return;
            }

            if (_gameBoard.All(x => x.GameFigure != GameFigures.None))
            {
                FinishRound(RoundFinishReasons.Tie);
                return;
            }
        }

        private void ValidateMove(Move move)
        {
            if (move.Point.x < 0 || move.Point.y < 0 || move.Point.x >= _boardSize || move.Point.y >= _boardSize)
            {
                throw new Exception("Point is out of bounds!");
            } 

            if (_gameBoard.FirstOrDefault(x => x.Point.x == move.Point.x && x.Point.y == move.Point.y).GameFigure != GameFigures.None)
            {
                throw new Exception("Place is already taken!");
            }
        }

    }
}
