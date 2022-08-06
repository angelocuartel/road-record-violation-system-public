using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.EntityConfigurations
{
    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(u => u.UsersInformation)
                  .WithOne(a => a.ApplicationUser)
                  .HasForeignKey<ApplicationUser>(u => u.UsersInfoId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.ProfilePicture)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(a => a.AccountStatus)
                   .HasMaxLength(20)
                   .IsRequired();
        }
    }
}
