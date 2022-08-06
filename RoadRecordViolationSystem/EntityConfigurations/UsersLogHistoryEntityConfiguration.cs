using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.EntityConfigurations
{
    public class UsersLogHistoryEntityConfiguration : IEntityTypeConfiguration<UsersLogHistory>
    {
        public void Configure(EntityTypeBuilder<UsersLogHistory> builder)
        {
            builder.HasKey(l => l.LogId);

            builder.Property(ti => ti.TimeIn)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(to => to.TimeOut)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(di => di.DateIn)
                    .IsRequired()
                    .HasMaxLength(150); 

            builder.Property(d => d.DateOut)
                    .IsRequired()
                    .HasMaxLength(150);

            builder.HasOne(u => u.User)
                   .WithMany(l => l.UsersLogHistories)
                   .HasForeignKey(u => u.Id)
                   .OnDelete(DeleteBehavior.Cascade);
        }       
    }
}
