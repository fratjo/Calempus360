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
    internal class CourseEquipmentTypeConfiguration : IEntityTypeConfiguration<CourseEquipmentTypeEntity>
    {
        public void Configure(EntityTypeBuilder<CourseEquipmentTypeEntity> builder)
        {
            builder.HasKey(ce => new
            {
                ce.EquipmentTypeId,
                ce.CourseId,
                ce.UniversityId,
                ce.AcademicYearId
            });

            builder.Property(ce => ce.UniversityId).IsRequired();

            builder.Property(ce => ce.CourseId).IsRequired();

            builder.Property(ce => ce.EquipmentTypeId).IsRequired();

            builder.Property(ce => ce.AcademicYearId).IsRequired();

            builder
                .HasOne(ce => ce.EquipmentTypeEntity)
                .WithMany(ce => ce.CourseEquipmentTypes)
                .HasForeignKey(ce => ce.EquipmentTypeId).OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(ce => ce.CourseEntity)
                .WithMany(ce => ce.EquipmentTypes)
                .HasForeignKey(ce => ce.CourseId).OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(ce => ce.UniversityEntity)
                .WithMany(ce => ce.CourseEquipmentTypes)
                .HasForeignKey(ce => ce.UniversityId).OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(ce => ce.AcademicYearEntity)
                .WithMany(ce => ce.CourseEquipmentTypes)
                .HasForeignKey(ce => ce.AcademicYearId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
