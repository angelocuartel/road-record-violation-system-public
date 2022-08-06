using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadRecordViolationSystem.Models;


namespace RoadRecordViolationSystem.EntityConfigurations
{
    public class ViolationsEntityConfigurations : IEntityTypeConfiguration<Violations>
    {
        public void Configure(EntityTypeBuilder<Violations> builder)
        {
            builder.HasKey(i => i.Id);


            builder.Property(i => i.Cost)
                   .HasColumnType("Decimal")
                   .HasPrecision(18, 2);



            builder.HasOne(i => i.ViolatorTicketInformation)
                .WithMany(i => i.Violations)
                .HasForeignKey(i => i.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
