using System;

namespace TennisClub.DTO.Role
{
    public class MemberRoleUpdateDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int RoleId { get; set; }
        public int MemberId { get; set; }
    }
}
