using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TennisClub.DAL.Context;
using TennisClub.Data.Model;

namespace TennisClub.DAL.Repository
{
    public class RoleRepository : GenericRepository<Role>
    {
        public RoleRepository(TennisContext context) : base(context) { }

        public IQueryable<Role> GetAll()
        {
            try
            {
                return context.Roles.FromSqlRaw("SELECT * FROM dbo.tblRoles");
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public IQueryable<Role> GetById(int id)
        {
            var selectedRole = context.Roles.FromSqlInterpolated($"EXECUTE dbo.SelectRoleById @Id = {id}").IgnoreQueryFilters();
            return selectedRole;
        }
    }
}
