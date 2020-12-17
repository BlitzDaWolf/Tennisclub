using System;
using System.Collections.Generic;
using TennisClub.DAL.Context;
using TennisClub.Data.Model;

namespace TennisClub.DAL.Repository
{
    public class GenderRepository : GenericRepository<Gender>
    {
        public GenderRepository(TennisContext context) : base(context) { }

        public override void Insert(Gender entity)
        {
            throw new Exception("Could not insert a gender");
        }

        public override void Delete(Gender entityToDelete)
        {
            throw new Exception("Could not delete a gender");
        }

        public override List<Gender> FromQuery(string query, params object[] parameters)
        {
            throw new Exception("The function is not available for this type");
        }

        public override void Update(Gender entityToUpdate)
        {
            throw new Exception("Could not update a gender");
        }
    }
}
