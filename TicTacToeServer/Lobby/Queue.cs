using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Client;
using TicTacToeServer.Lobby.Model;
using TicTacToeServer.Models;

namespace TicTacToeServer.Lobby
{
    /// <summary>
    /// Lobby table, after this fills with players game starts.
    /// </summary>
    internal class Queue
    {
        // synchronization object
        private Object _queueLock = new();

        private readonly List<QueueSeat> _seats = new List<QueueSeat> {new QueueSeat(), new QueueSeat() };
        internal List<QueueSeat> Seats => _seats;
        internal String Name { get; set; }
        internal Int64 ID { get; set; }
        internal Int32 Till { get; set; }
        internal Int32 BoadrSize { get; set; }
        internal Boolean IsFull
        {
            get
            {
                lock (_queueLock)
                {
                    return _seats.All(x => x.player != null);
                }
                
            }
        }
        internal Int32 PlayerCount
        {
            get
            {
                return _seats.Count(x => x.player != null);
            }
        }

        internal void SeatOnQueue(Player<ILobbyClient> player)
        {
            lock (_queueLock)
            {
                if (_seats.Any(x => x.player?.ID == player.ID))
                {
                    throw new Exception("You already are in this queue.");
                }

                if (_seats.All(x => x.player != null))
                {
                    throw new Exception("Queue already full.");
                }

                _seats.FirstOrDefault(x => x.player == null).player = player;
            }   
        }

        internal void SeatOut(Player<ILobbyClient> player)
        {
            lock(_queueLock)
            {
                if (_seats.Any(x => x.player?.ID == player.ID))
                {
                    _seats.First(x => x.player.ID == player.ID).player = null;
                    return;
                }
                throw new Exception("You are not in Queue");
            }
        }
    }
}
