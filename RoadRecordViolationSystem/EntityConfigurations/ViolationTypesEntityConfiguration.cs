using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadRecordViolationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.EntityConfigurations
{
    public class ViolationTypesEntityConfiguration : IEntityTypeConfiguration<ViolationTypes>
    {
        public void Configure(EntityTypeBuilder<ViolationTypes> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(d => d.Description)
               .HasMaxLength(1000);


            builder.Property(f => f.Fee)
                .IsRequired()
               .HasColumnType<decimal>("Decimal")
               .HasPrecision(18, 2);

        }
    }
}
