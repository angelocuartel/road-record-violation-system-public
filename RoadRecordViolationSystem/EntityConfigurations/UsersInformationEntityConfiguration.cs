using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.EntityConfigurations
{
    public class UsersInformationEntityConfiguration : IEntityTypeConfiguration<UsersInformation>
    {
        public void Configure(EntityTypeBuilder<UsersInformation> builder)
        {
            builder.HasKey(i => i.UserInfoId);

            builder.Property(g => g.GivenName)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(m => m.MiddleName)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(l => l.LastName)
                  .HasMaxLength(50)
                  .IsRequired();

            builder.Property(g => g.Gender)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(c => c.City)
                  .HasMaxLength(50)
                  .IsRequired();

            builder.Property(a => a.Address)
                  .HasMaxLength(50)
                  .IsRequired();
        }
    }
}
