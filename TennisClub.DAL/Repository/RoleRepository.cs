using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
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
            try
            {
                var selectedRole = context.Roles.FromSqlInterpolated($"EXECUTE dbo.SelectRoleById @Id = {id}").IgnoreQueryFilters();
                return selectedRole;
            }
            catch (Exception)
            {
                throw new Exception($"Couldn't retrieve entity with id: {id}");
            }
        }
        public async Task AddAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await context.Database.ExecuteSqlInterpolatedAsync($"EXECUTE dbo.CreateRole @RoleName = {role.Name}");
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(role)} could not be saved: {ex.Message}");
            }
        }
        public async Task UpdateAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");
            }

            try
            {
                await context.Database.ExecuteSqlInterpolatedAsync($"EXECUTE dbo.UpdateRole @Id = {role.Id}, @RoleName = {role.Name}");
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(role)} could not be updated: {ex.Message}");
            }
        }

    }
}
