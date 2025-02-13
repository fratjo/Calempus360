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
            builder.HasKey(use => new { use.EquipmentId, use.AcademicYearId });
            
            builder.Property(use => use.EquipmentId).IsRequired();
            
            builder.Property(use => use.AcademicYearId).IsRequired();

            builder.Property(use => use.UniversityId).IsRequired();
            
            builder
                .HasOne(use => use.Equipment)
                .WithOne(use => use.UniversitySiteEquipment)
                .HasForeignKey<UniversitySiteEquipment>(use => use.EquipmentId);
            
            builder
                .HasOne(use => use.University)
                .WithMany(use => use.Equipments)
                .HasForeignKey(use => use.UniversityId);
            
            builder
                .HasOne(use => use.Site)
                .WithMany(use => use.Equipments)
                .HasForeignKey(use => use.SiteId);
            
            builder
                .HasOne(use => use.AcademicYear)
                .WithMany(use => use.UniversitySiteEquipments)
                .HasForeignKey(use => use.AcademicYearId);
        }
    }
}
