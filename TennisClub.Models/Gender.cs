using System.Collections;
using System.Collections.Generic;
using TennisClub.Data.Model.Interface;

namespace TennisClub.Data.Model
{
    public class Gender : IIsDeleted
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}