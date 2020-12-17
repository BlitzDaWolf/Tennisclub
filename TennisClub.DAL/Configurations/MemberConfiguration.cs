using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisClub.Data.Model;

namespace TennisClub.DAL.Configurations
{
    public class MemberConfiguration : BaseEntityTypeConfiguration<Member>
    {
        public MemberConfiguration() : base("tblMembers") { }

        public override void Configure(EntityTypeBuilder<Member> e)
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).ValueGeneratedOnAdd();

            e.Property(c => c.FederationNr).IsRequired().HasColumnType("VARCHAR(10)");
            e.HasIndex(c => c.FederationNr).IsUnique();

            e.Property(c => c.FirstName).IsRequired().HasColumnType("VARCHAR(25)");
            e.Property(c => c.LastName).IsRequired().HasColumnType("VARCHAR(35)");
            e.Property(c => c.BirthDate).IsRequired().HasColumnType("DATE");
            e.Property(c => c.Address).IsRequired().HasColumnType("VARCHAR(70)");
            e.Property(c => c.Number).IsRequired().HasColumnType("VARCHAR(6)");
            e.Property(c => c.Addition).HasColumnType("VARCHAR(2)");
            e.Property(c => c.Zipcode).IsRequired().HasColumnType("VARCHAR(6)");
            e.Property(c => c.City).IsRequired().HasColumnType("VARCHAR(30)");
            e.Property(c => c.PhoneNr).HasColumnType("VARCHAR(15)");

            e.HasMany(c => c.Fines).WithOne(c => c.Member).HasForeignKey(c => c.MemberId);
            e.HasMany(c => c.Roles).WithOne(c => c.Member).HasForeignKey(c => c.MemberId);
            e.HasMany(c => c.Games).WithOne(c => c.Member).HasForeignKey(c => c.MemberId);

            e.HasOne(c => c.Gender).WithMany().HasForeignKey(k => k.GenderId);

            base.Configure(e);
        }
    }
}
