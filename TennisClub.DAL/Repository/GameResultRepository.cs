using System;
using System.Collections.Generic;
using System.Text;
using TennisClub.DAL.Context;
using TennisClub.Data.Model;

namespace TennisClub.DAL.Repository
{
    public class GameResultRepository : GenericRepository<GameResult>
    {
        public GameResultRepository(TennisContext context) : base(context) { }
    }
}
