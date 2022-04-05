using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Game.Enums;

namespace TicTacToeServer.Game
{
    public class GameFigure
    {
        public GameShapes Shape { get; set; } = GameShapes.None;
        public FigureSizes Size { get; set; } = FigureSizes.None;

        public Int32 CompareSize(GameFigure other)
        {
            return Size - other.Size;
        }

        public Boolean SameShape(GameFigure other)
        {
            return Shape == other.Shape;
        }

        public Boolean IsBiggerThanOrEquals(GameFigure other)
        {
            if (Size == FigureSizes.None && other.Size == FigureSizes.None)
            {
                return false;
            }

            return CompareSize(other) >= 0;
        }

        public Boolean Equal(GameFigure other)
        {
            return Shape == other.Shape && Size == other.Size;
        }

        public override string ToString()
        {
            return $"Shape - {Shape}, Size - {Size}";
        }
    }
}
