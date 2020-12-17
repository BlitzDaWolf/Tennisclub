using System;

namespace TennisClub.DTO.Role
{
    public class MemberRoleCreateDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int RoleId { get; set; }
        public int MemberId { get; set; }
    }
}
