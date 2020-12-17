using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TennisClub.Data.Model;

namespace TennisClub.DAL.Configurations
{
    public class GameConfiguration : BaseEntityTypeConfiguration<Game>
    {
        public GameConfiguration() : base("tblGames") { }

        public override void Configure(EntityTypeBuilder<Game> e)
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).ValueGeneratedOnAdd();

            e.Property(c => c.GameNumber).HasColumnType("VARCHAR(10)").IsRequired();
            e.HasIndex(c => c.GameNumber).IsUnique();

            e.Property(c => c.Date).IsRequired().HasColumnType("DATE");

            e.HasOne(c => c.Member).WithMany();
            e.HasOne(c => c.League).WithMany();

            e.HasOne(c => c.Member).WithMany().HasForeignKey(k => k.MemberId);
            e.HasOne(c => c.League).WithMany().HasForeignKey(k => k.LeagueId);

            e.HasMany(c => c.Results).WithOne(c => c.Game).HasForeignKey(k => k.GameId);

            base.Configure(e);
        }
    }
}
