using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Game.Enums;

namespace TicTacToeServer.Models
{
    internal class Player<T>
    {
        internal T Client { get; set; }
        internal String ConnectionId { get; set; }
        internal GameFigures PlayerFigure { get; set; }



        //public virtual Player<TClone> Clone<TClone>()
        //{
        //    return new Player<TClone>
        //    {
        //        ConnectionId = ConnectionId
        //    };
        //}
    }
}
