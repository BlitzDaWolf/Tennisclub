using TennisClub.Data.Model.Interface;

namespace TennisClub.Data.Model
{
    public class GameResult : IIsDeleted
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        public int SetNr { get; set; }
        public int ScoreTeamMember { get; set; }
        public int ScoreOpponent { get; set; }
        public bool IsDeleted { get; set; }
    }
}
