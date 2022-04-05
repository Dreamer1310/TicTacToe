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
        private readonly Board _board;

        internal List<Cell> gameBoard => _gameBoard; 
        internal Board Board => _board; 
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
                    cell.GameFigure = new GameFigure();
                    _gameBoard.Insert(i * _boardSize + j, cell);
                    //_gameBoard[i * _boardSize + j] = cell;
                }
            }
        }

        internal void MakeMove(Move move)
        {
            try
            {
                ValidateMove(move);
                if (!_board.SetGameFigureAt(move.Point, move.Figure))
                {
                    throw new Exception($"Couldn't place figure... Point: {move.Point}; Figure: {move.Figure}");
                }
                //_gameBoard[move.Point.x * _boardSize + move.Point.y].GameFigure = move.Figure;
                Moves.Add(move);

                CheckBoard();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.White;
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

            WinningLine = _board.GetFilledLine(move.Point, move.Figure);

            if (WinningLine != null)
            {
                FinishRound(RoundFinishReasons.PlayerWon);
                return;
            }
                
            if (_board.AllFull())
            {
                FinishRound(RoundFinishReasons.Tie);
                return;
            }
        }

        private void ValidateMove(Move move)
        {
            if (_board.IsOutOfBounds(move.Point))
            {
                throw new Exception("Point is out of bounds!");
            }

            var figure = _board.GetFigureAt(move.Point);

            if (figure.IsBiggerThanOrEquals(move.Figure))
            {
                throw new Exception("Figure is not big enough to place");
            }

            if (figure.Shape != GameShapes.None && figure.Size == FigureSizes.None)
            {
                throw new Exception("Place is already taken!");
            }
        }

    }
}
