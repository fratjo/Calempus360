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
            builder.HasKey(ce => new { ce.EquipmentType_Id, ce.Course_Id });
            builder.Property(ce => ce.AcademicYear_Id).IsRequired();
            builder.Property(ce => ce.Quantity).IsRequired();
            builder.HasOne(ce => ce.EquipmentType).WithMany(ce => ce.Courses).HasForeignKey(ce => ce.EquipmentType_Id);
            builder.HasOne(ce => ce.Course).WithMany(ce => ce.EquipmentType).HasForeignKey(ce => ce.Course_Id);
        }
    }
}
