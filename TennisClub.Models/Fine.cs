using System;
using TennisClub.Data.Model.Interface;

namespace TennisClub.Data.Model
{
    public class Fine : IIsDeleted
    {
        public int Id { get; set; }
        public int FineNumber { get; set; }
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        public decimal Amount { get; set; }
        public DateTime handOutDate { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
