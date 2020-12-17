using TennisClub.DAL.Context;
using TennisClub.Data.Model;

namespace TennisClub.DAL.Repository
{
    public class MemberRepository : GenericRepository<Member>
    {
        public MemberRepository(TennisContext context) : base(context) { }
    }
}
