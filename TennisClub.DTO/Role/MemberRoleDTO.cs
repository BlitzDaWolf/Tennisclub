using System;
using TennisClub.DTO.Member;

namespace TennisClub.DTO.Role
{
    public class MemberRoleDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RoleDTO Role { get; set; }
        public MemberDTO Member { get; set; }
    }
}
