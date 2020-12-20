using System;
using System.Collections.Generic;
using System.Text;

namespace TennisClub.DTO.Game
{
    public class GameResultUpdateDTO
    {
        public int Id { get; set; }
        public int SetNr { get; set; }
        public int ScoreTeamMember { get; set; }
        public int ScoreOpponent { get; set; }
    }
}
