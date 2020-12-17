using System;
using TennisClub.DTO.Member;

namespace TennisClub.DTO.Fine
{
    public class FineDTO
    {
        public int Id { get; set; }
        public int FineNumber { get; set; }
        public MemberDTO Member { get; set; }
        public decimal Amount { get; set; }
        public DateTime handOutDate { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
