using Calempus360.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Infrastructure.Data.ModelConfiguration
{
    internal class ClassroomEquipmentConfiguration : IEntityTypeConfiguration<ClassroomEquipment>
    {
        public void Configure(EntityTypeBuilder<ClassroomEquipment> builder)
        {
            builder.HasKey(ce => new { ce.EquipmentId, ce.ClassroomId, ce.AcademicYearId });
            
            builder.Property(ce => ce.ClassroomId).IsRequired();
            
            builder.Property(ce => ce.EquipmentId).IsRequired();
            
            builder.Property(ce => ce.AcademicYearId).IsRequired();
            
            builder
                .HasOne(ce => ce.Classroom)
                .WithMany(ce => ce.ClassroomEquipments)
                .HasForeignKey(ce => ce.EquipmentId);
            
            builder
                .HasOne(ce => ce.Equipment)
                .WithOne(ce => ce.ClassroomEquipment)
                .HasForeignKey<ClassroomEquipment>(ce => ce.EquipmentId);
            
            builder
                .HasOne(ce => ce.AcademicYear)
                .WithMany(ce => ce.ClassroomEquipments)
                .HasForeignKey(ce => ce.AcademicYearId);
        }
    }
}
