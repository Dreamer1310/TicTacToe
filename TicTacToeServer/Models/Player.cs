using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Game.Enums;
using TicTacToeServer.Lib;

namespace TicTacToeServer.Models
{
    internal class Player<T>
    {
        internal T Client { get; set; }
        internal String ID { get; set; }
        internal String ConnectionID { get; set; }
        internal GameFigures PlayerFigure { get; set; }
        internal CountDownTimer Timer { get; set; }
        internal Boolean Ready { get; set; }


        public virtual Player<TClone> Clone<TClone>()
        {
            return new Player<TClone>
            {
                ID = ID
            };
        }
    }
}
