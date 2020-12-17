using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TennisClub.Data.Model;

namespace TennisClub.DAL.Configurations
{
    public class GenderConfiguration : BaseEntityTypeConfiguration<Gender>
    {
        public GenderConfiguration() : base("tblGenders") { }

        public override void Configure(EntityTypeBuilder<Gender> e)
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).ValueGeneratedOnAdd();

            e.Property(c => c.Name).IsRequired().HasColumnType("VARCHAR(10)").HasMaxLength(10);
            e.HasIndex(c => c.Name).IsUnique();

            e.HasData(
                new Gender { Id = 1, Name = "Male" },
                new Gender { Id = 2, Name = "Female" }
            );

            base.Configure(e);
        }
    }
}
