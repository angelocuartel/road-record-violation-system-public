using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadRecordViolationSystem.Models;

namespace RoadRecordViolationSystem.EntityConfigurations
{
    public class LicenseDetailsEntityConfigurations : IEntityTypeConfiguration<LicenseDetails>
    {
        public void Configure(EntityTypeBuilder<LicenseDetails> builder)
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

            builder.Property(w => w.Weight)
                .HasMaxLength(10);

            builder.Property(h => h.Height)
                .HasMaxLength(10);

            builder.Property(n => n.Nationality)
                .HasMaxLength(50);

            builder.Property(a => a.Address)
                .HasMaxLength(200);

            builder.Property(c => c.City)
                .HasMaxLength(100);

            builder.Property(e => e.EyeColor)
                .HasMaxLength(20);

            builder.Property(l => l.LicenseNo)
                .HasMaxLength(50);

            builder.HasOne(i => i.Violator)
                   .WithOne(i => i.LicenseDetails)
                   .HasForeignKey<LicenseDetails>(i => i.ViolatorId)
                   .OnDelete(DeleteBehavior.Cascade);

            

        }
    }
}
