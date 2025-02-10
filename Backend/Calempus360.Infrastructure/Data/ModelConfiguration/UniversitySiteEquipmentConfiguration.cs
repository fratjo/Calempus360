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
    internal class UniversitySiteEquipmentConfiguration : IEntityTypeConfiguration<UniversitySiteEquipment>
    {
        public void Configure(EntityTypeBuilder<UniversitySiteEquipment> builder)
        {
            builder.HasKey(use => use.Equipment_Id);
            builder.Property(use => use.AcademicYear_Id).IsRequired();
            builder.HasOne(use => use.Equipment).WithOne(use => use.UniversitySiteEquipment).HasForeignKey<UniversitySiteEquipment>(use => use.Equipment_Id);
            builder.HasOne(use => use.University).WithMany(use => use.Equipments).HasForeignKey(use => use.University_Id);
            builder.HasOne(use => use.Site).WithMany(use => use.Equipments).HasForeignKey(use => use.Site_Id);

        }
    }
}
