using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.EntityConfigurations
{
    public class AccidentEntityConfiguration : IEntityTypeConfiguration<Accident>
    {
        public void Configure(EntityTypeBuilder<Accident> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.AccidentImage)
                    .HasMaxLength(1500);
        }
    }
}
