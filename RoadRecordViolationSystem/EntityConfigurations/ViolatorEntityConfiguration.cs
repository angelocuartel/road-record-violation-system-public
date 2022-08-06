using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.EntityConfigurations
{
    public class ViolatorEntityConfiguration : IEntityTypeConfiguration<Violator>
    {
        public void Configure(EntityTypeBuilder<Violator> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(g => g.Gender)
                   .HasMaxLength(6);

            builder.Property(g => g.GivenName)
                   .HasMaxLength(50);

            builder.Property(l => l.LastName)
                .HasMaxLength(50);

            builder.Property(m => m.MiddleName)
                .HasMaxLength(50);

            builder.Property(a => a.Address)
               .HasMaxLength(200);
        }
    }
}
