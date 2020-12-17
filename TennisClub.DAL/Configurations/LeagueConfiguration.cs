using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TennisClub.Data.Model;

namespace TennisClub.DAL.Configurations
{
    public class LeagueConfiguration : BaseEntityTypeConfiguration<League>
    {
        public LeagueConfiguration() : base("tblLeagues") { }

        public override void Configure(EntityTypeBuilder<League> e)
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).ValueGeneratedOnAdd();

            e.Property(c => c.Name).IsRequired().HasColumnType("VARCHAR(10)").HasMaxLength(10);
            e.HasIndex(c => c.Name).IsUnique();

            e.HasData(
                new League { Id = 1, Name = "Junoir"},
                new League { Id = 2, Name = "Pro"}
            );

            base.Configure(e);
        }
    }
}
