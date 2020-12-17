using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TennisClub.Data.Model;

namespace TennisClub.DAL.Configurations
{
    public class GameResultConfiguration : BaseEntityTypeConfiguration<GameResult>
    {
        public GameResultConfiguration() : base("tblGameResults") { }

        public override void Configure(EntityTypeBuilder<GameResult> e)
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).ValueGeneratedOnAdd();

            e.Property(c => c.SetNr).IsRequired().HasColumnType("INT").HasMaxLength(3);
            e.Property(c => c.ScoreTeamMember).IsRequired().HasColumnType("INT").HasMaxLength(3);
            e.Property(c => c.ScoreOpponent).IsRequired().HasColumnType("INT").HasMaxLength(3);

            e.HasIndex(c => new { c.SetNr, c.GameId }).IsUnique();

            base.Configure(e);
        }
    }
}
