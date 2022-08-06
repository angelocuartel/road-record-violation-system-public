using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.EntityConfigurations
{
    public class VehicleInformationEntityConfiguration : IEntityTypeConfiguration<VehicleInformation>
    {
        public void Configure(EntityTypeBuilder<VehicleInformation> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Model)
                    .HasMaxLength(20);

            builder.Property(i => i.PlateNo)
                    .HasMaxLength(30);

            builder.Property(i => i.Type)
                    .HasMaxLength(20);

            builder.Property(i => i.StickerNo)
                    .HasMaxLength(14);


            builder.HasOne(l => l.Violator)
                    .WithMany(v => v.Vehicles)
                    .HasForeignKey(l => l.ViolatorId)
                    .OnDelete(DeleteBehavior.Cascade);

                    
        }
    }
}
