using TennisClub.DAL.Context;
using TennisClub.Data.Model;

namespace TennisClub.DAL.Repository
{
    public class MemberRoleRepository : GenericRepository<MemberRole>
    {
        public MemberRoleRepository(TennisContext context) : base(context) { }
    }
}
