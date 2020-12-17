using System;
using System.Collections.Generic;
using System.Text;
using TennisClub.DAL.Context;
using TennisClub.Data.Model;

namespace TennisClub.DAL.Repository
{
    public class LeagueRepository : GenericRepository<League>
    {
        public LeagueRepository(TennisContext context) : base(context) { }
    }
}
