using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.EntityConfigurations
{
    public class ViolatorTicketInformationEntityConfiguration : IEntityTypeConfiguration<ViolatorTicketInformation>
    {
        public void Configure(EntityTypeBuilder<ViolatorTicketInformation> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(q => q.QrSecurityHash)
                    .HasMaxLength(1000)
                    .IsRequired();

            builder.Property(t => t.TicketNo)
                    .HasMaxLength(10)
                    .IsRequired();

            builder.Property(t => t.TotalAmountToBePayed)
                    .HasColumnType<decimal>("decimal")
                    .HasPrecision(18, 2);


            builder.HasOne(n => n.Violator)   
                   .WithMany(v => v.ViolatorTicketInformations)
                   .HasForeignKey(n => n.ViolatorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.VehicleInformation)
                    .WithMany(i => i.violationTickets)
                    .HasForeignKey(n => n.vehicleId);

        }
    }
}
