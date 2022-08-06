using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.EntityConfigurations
{
    public class OfficerInvolveEntityConfiguration : IEntityTypeConfiguration<OfficerInvolve>
    {
        public void Configure(EntityTypeBuilder<OfficerInvolve> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Accident)
                 .WithMany(i => i.OfficerInvolves)
                 .HasForeignKey(i => i.AccidentId)
                 .OnDelete(DeleteBehavior.Cascade);
                
        }
    }
}
