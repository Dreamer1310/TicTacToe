using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeServer.Game.Enums;

namespace TicTacToeServer.Communication.Dto
{
    public class RoundFinishedDto  
    {
        public RoundFinishReasons RoundFinishReason { get; set; }
        public String WinnerID { get; set; }
        public Dictionary<String, Int32> Scores { get; set; }
        public List<PointDto> WinningLine { get; set; }
    }
}
