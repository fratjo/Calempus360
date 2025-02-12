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
    internal class CourseEquipmentTypeConfiguration : IEntityTypeConfiguration<CourseEquipmentType>
    {
        public void Configure(EntityTypeBuilder<CourseEquipmentType> builder)
        {
            builder.HasKey(ce => new
            {
                ce.EquipmentTypeId, ce.CourseId, ce.UniversityId, ce.AcademicYearId
            });
            builder.Property(ce => ce.AcademicYearId).IsRequired();
            builder.Property(ce => ce.Quantity).IsRequired();
            builder
                .HasOne(ce => ce.EquipmentType)
                .WithMany(ce => ce.CourseEquipmentTypes)
                .HasForeignKey(ce => ce.EquipmentTypeId);
            builder
                .HasOne(ce => ce.Course)
                .WithMany(ce => ce.EquipmentTypes)
                .HasForeignKey(ce => ce.CourseId);
            builder
                .HasOne(ce => ce.University)
                .WithMany(ce => ce.CourseEquipmentTypes)
                .HasForeignKey(ce => ce.UniversityId);
            builder
                .HasOne(ce => ce.AcademicYear)
                .WithMany(ce => ce.CourseEquipmentTypes)
                .HasForeignKey(ce => ce.AcademicYearId);
        }
    }
}
