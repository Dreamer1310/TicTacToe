using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeServer.Game
{
    public class Board
    {
        private List<Cell> _cells { get; set; }

        private readonly Int32 _size;

        public Board(Int32 size)
        {
            _size = size;

            _cells = new List<Cell>(_size * _size);

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    var cell = new Cell();
                    cell.Point = new Point(i, j);
                    cell.GameFigure = new GameFigure();
                    _cells.Insert(i * _size + j, cell);
                    //_gameBoard[i * _boardSize + j] = cell;
                }
            }
        }

        internal Boolean IsOutOfBounds(Point point)
        {
            return point.x < 0 || point.y < 0 || point.x >= _size || point.y >= _size;
        }

        internal GameFigure GetFigureAt(Point p)
        {
            return _cells.FirstOrDefault(x => x.Point.Equals(p))?.GameFigure;
        }

        internal Boolean SetGameFigureAt(Point p, GameFigure figure)
        {
            var cell = _cells.FirstOrDefault(x => x.Point.Equals(p));
            
            if (cell == null)
            {
                return false;
            }

            cell.GameFigure = figure;
            return true;
        }

        internal List<Point> GetFilledLine(Point lastP, GameFigure lastFigure)
        {
            if (_cells.Where(x => x.Point.x == lastP.x).All(x => x.GameFigure.SameShape(lastFigure)))
            {
                var winningLine = _cells.Where(x => x.Point.x == lastP.x).Select(x => x.Point).ToList();
                return winningLine;
            }

            if (_cells.Where(x => x.Point.y == lastP.y).All(x => x.GameFigure.SameShape(lastFigure)))
            {
                var winningLine = _cells.Where(x => x.Point.y == lastP.y).Select(x => x.Point).ToList();
                return winningLine;
            }

            if (_cells.Where(x => x.Point.x == x.Point.y).All(x => x.GameFigure.SameShape(lastFigure)))
            {
                var winningLine = _cells.Where(x => x.Point.x == x.Point.y).Select(x => x.Point).ToList();                
                return winningLine;
            }

            if (_cells.Where(x => x.Point.x + x.Point.y == _size - 1).All(x => x.GameFigure.SameShape(lastFigure)))
            {
                var winningLine = _cells.Where(x => x.Point.x + x.Point.y == _size - 1).Select(x => x.Point).ToList();
                return winningLine;
            }

            return null;
        }

        internal Boolean AllFull()
        {
            return _cells.All(x => x.GameFigure.Shape != Enums.GameShapes.None);
        }
    }
}
