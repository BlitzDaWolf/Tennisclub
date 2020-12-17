using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TennisClub.Data.Model;

namespace TennisClub.DAL.Configurations
{
    public class FineConfiguration : BaseEntityTypeConfiguration<Fine>
    {
        public FineConfiguration() : base("tblMemberFines") { }

        public override void Configure(EntityTypeBuilder<Fine> e)
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).ValueGeneratedOnAdd();

            e.Property(c => c.FineNumber).IsRequired().HasColumnType("INT").HasMaxLength(10);
            e.HasIndex(c => c.FineNumber).IsUnique();

            e.Property(c => c.Amount).IsRequired().HasColumnType("DECIMAL(7, 2)");
            e.Property(c => c.handOutDate).IsRequired().HasColumnType("DATE");
            e.Property(c => c.PaymentDate).HasColumnType("DATE");

            e.HasOne(c => c.Member).WithMany();

            e.HasOne(c => c.Member).WithMany().HasForeignKey(k => k.MemberId);

            base.Configure(e);
        }
    }
}
