using System;

namespace TennisClub.DTO.Fine
{
    public class FineCreateDTO
    {

        public int FineNumber { get; set; }
        public int MemberId { get; set; }
        public decimal Amount { get; set; }
        public DateTime handOutDate { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
