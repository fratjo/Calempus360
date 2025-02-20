using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Data.ModelConfiguration
{
    internal class ClassroomEquipmentConfiguration : IEntityTypeConfiguration<ClassroomEquipmentEntity>
    {
        public void Configure(EntityTypeBuilder<ClassroomEquipmentEntity> builder)
        {
            builder.HasKey(ce => new { ce.EquipmentId, ce.ClassroomId, ce.AcademicYearId });
            
            builder.Property(ce => ce.ClassroomId).IsRequired();
            
            builder.Property(ce => ce.EquipmentId).IsRequired();
            
            builder.Property(ce => ce.AcademicYearId).IsRequired();
            
            builder
                .HasOne(ce => ce.ClassroomEntity)
                .WithMany(ce => ce.ClassroomEquipments)
                .HasForeignKey(ce => ce.EquipmentId);
            
            builder
                .HasOne(ce => ce.EquipmentEntity)
                .WithOne(ce => ce.ClassroomEquipmentEntity)
                .HasForeignKey<ClassroomEquipmentEntity>(ce => ce.EquipmentId);
            
            builder
                .HasOne(ce => ce.AcademicYearEntity)
                .WithMany(ce => ce.ClassroomEquipments)
                .HasForeignKey(ce => ce.AcademicYearId);
        }
    }
}
