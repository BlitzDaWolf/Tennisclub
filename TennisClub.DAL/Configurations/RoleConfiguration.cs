using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TennisClub.Data.Model;

namespace TennisClub.DAL.Configurations
{
    public class RoleConfiguration : BaseEntityTypeConfiguration<Role>
    {
        public RoleConfiguration() : base("tblRoles") { }

        public override void Configure(EntityTypeBuilder<Role> e)
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).ValueGeneratedOnAdd();

            e.Property(c => c.Name).IsRequired().HasColumnType("VARCHAR(20)");
            e.HasIndex(c => c.Name).IsUnique();

            e.HasData(
                new Role { Id = 1, Name = "User" },
                new Role { Id = 2, Name = "Admin" }
            );

            base.Configure(e);
        }
    }
}
