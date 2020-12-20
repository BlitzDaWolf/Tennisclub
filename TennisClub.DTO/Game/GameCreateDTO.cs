using System;

namespace TennisClub.DTO.Game
{
    public class GameCreateDTO
    {
        public string GameNumber { get; set; }
        public int MemberId { get; set; }
        public int LeagueId { get; set; }
        public DateTime Date { get; set; }
    }
}
