using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Game;
using TicTacToeServer.Game.Enums;
using TicTacToeServer.Lib;

namespace TicTacToeServer.Models
{
    public class Player<T>
    {
        internal T Client { get; set; }
        internal String ID { get; set; }
        internal String ConnectionID { get; set; }
        internal GameShapes PlayerFigure { get; set; }
        internal CountDownTimer Timer { get; set; }
        internal Boolean Ready { get; set; }
        internal List<GameFigure> HandFingures { get; set; }

        public virtual Player<TClone> Clone<TClone>()
        {
            return new Player<TClone>
            {
                ID = ID
            };
        }

        /// <summary>
        /// Checks if player has specific figure in hand
        /// </summary>
        /// <param name="figure">figure to find</param>
        /// <returns></returns>
        public Boolean HasFigure(GameFigure figure)
        {
            return HandFingures.Any(x => x.Equal(figure));
        }

        /// <summary>
        /// Removes specific figure from hand
        /// </summary>
        /// <param name="figure">figure to remove from hand</param>
        public void RemoveFigure(GameFigure figure)
        {
            HandFingures.Remove(HandFingures.Single(x => x.Equal(figure)));
        }

        /// <summary>
        /// Assings players hand figures
        /// </summary>
        /// <param name="figures">list of figures</param>
        public void DealFigures(List<GameFigure> figures)
        {
            HandFingures = figures;
        }

        /// <summary>
        /// Sorts players hand figures
        /// </summary>
        public void OrderFigures()
        {
            HandFingures.OrderByDescending(x => x.Size);
        }
    }
}
