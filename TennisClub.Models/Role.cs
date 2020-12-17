using TennisClub.Data.Model.Interface;

namespace TennisClub.Data.Model
{
    public class Role : IIsDeleted
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
