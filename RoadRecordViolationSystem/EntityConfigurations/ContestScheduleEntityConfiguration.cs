using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.EntityConfigurations
{
    public class ContestScheduleEntityConfiguration : IEntityTypeConfiguration<ContestSchedule>
    {
        public void Configure(EntityTypeBuilder<ContestSchedule> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Contest)
                .WithOne(i => i.ContestSchedule)
                .HasForeignKey<ContestSchedule>(i => i.ContestApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
