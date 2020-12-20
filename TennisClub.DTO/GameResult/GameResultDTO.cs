namespace TennisClub.DTO.Game
{
    public class GameResultDTO
    {
        public GameDTO Game { get; set; }
        public int SetNr { get; set; }
        public int ScoreTeamMember { get; set; }
        public int ScoreOpponent { get; set; }
    }
}
