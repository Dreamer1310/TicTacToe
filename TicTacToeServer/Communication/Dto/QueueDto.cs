using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeServer.Communication.Dto
{
    public class QueueDto
    {
        public Int64 ID { get; set; }
        public String Name { get; set; }
        public Int64 PlayerCount { get; set; }
        public Int64 BoadrSize { get; set; }
        public Int64 Till { get; set; }
    }
}
