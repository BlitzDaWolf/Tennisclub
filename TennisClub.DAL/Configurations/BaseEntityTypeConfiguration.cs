using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisClub.Data.Model.Interface;

namespace TennisClub.DAL.Configurations
{
    public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : IIsDeleted
    {
        public string TableName { get; set; }

        public BaseEntityTypeConfiguration(string TableName)
        {
            this.TableName = TableName;
        }

        public virtual void Configure(EntityTypeBuilder<TBase> e)
        {
            e.HasQueryFilter(p => !p.IsDeleted);
            e.Property(c => c.IsDeleted).HasDefaultValue(false);
            e.ToTable(TableName);
        }
    }
}
