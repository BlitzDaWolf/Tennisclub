﻿using System;
using TennisClub.DTO.Member;

namespace TennisClub.DTO.Game
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string GameNumber { get; set; }
        public MemberDTO Member { get; set; }
        public LeagueDTO League { get; set; }
        public DateTime Date { get; set; }
    }
}
