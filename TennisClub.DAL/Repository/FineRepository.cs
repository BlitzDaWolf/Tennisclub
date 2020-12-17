using System;
using System.Collections.Generic;
using System.Text;
using TennisClub.DAL.Context;
using TennisClub.Data.Model;

namespace TennisClub.DAL.Repository
{
    public class FineRepository : GenericRepository<Fine>
    {
        public FineRepository(TennisContext context) : base(context) { }
    }
}
