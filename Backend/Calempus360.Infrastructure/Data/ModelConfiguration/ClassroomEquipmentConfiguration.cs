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
            builder.HasKey(ce => new { ce.Equipment_Id, ce.Classroom_Id });
            builder.Property(ce => ce.AcademicYear_Id).IsRequired();
            builder.HasOne(ce => ce.Classroom).WithMany(ce => ce.Equipments).HasForeignKey(ce => ce.Equipment_Id);
            builder.HasOne(ce => ce.Equipment).WithOne(ce => ce.Classroom).HasForeignKey<ClassroomEquipment>(ce => ce.Equipment_Id);
        }
    }
}
