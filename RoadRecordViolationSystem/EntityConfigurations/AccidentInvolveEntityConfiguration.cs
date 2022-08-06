using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.EntityConfigurations
{
    public class AccidentInvolveEntityConfiguration : IEntityTypeConfiguration<AccidentInvolve>
    {
        public void Configure(EntityTypeBuilder<AccidentInvolve> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(g => g.GivenName)
              .HasMaxLength(50);

            builder.Property(l => l.LastName)
                .HasMaxLength(50);

            builder.Property(m => m.MiddleName)
                .HasMaxLength(50);

            builder.Property(g => g.Gender)
                .HasMaxLength(6);

            builder.Property(a => a.Address)
              .HasMaxLength(200);

            builder.Property(e => e.EmergencyContactNo)
              .HasMaxLength(11);

            builder.HasOne(i => i.Accident)
                   .WithMany(i => i.AccidentInvolves)
                   .HasForeignKey(i => i.AccidentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
