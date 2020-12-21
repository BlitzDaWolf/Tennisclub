using System;

namespace TennisClub.DTO.Game
{
    public class GameUpdateDTO
    {
        public int Id { get; set; }
        public string GameNumber { get; set; }
        public int MemberId { get; set; }
        public int LeagueId { get; set; }
        public DateTime Date { get; set; }

    }
}
