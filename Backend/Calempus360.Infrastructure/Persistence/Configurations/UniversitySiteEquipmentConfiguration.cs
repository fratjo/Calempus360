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
    internal class UniversitySiteEquipmentConfiguration : IEntityTypeConfiguration<UniversitySiteEquipmentEntity>
    {
        public void Configure(EntityTypeBuilder<UniversitySiteEquipmentEntity> builder)
        {
            builder.HasKey(use => new { use.EquipmentId, use.AcademicYearId });
            
            builder.Property(use => use.EquipmentId).IsRequired();
            
            builder.Property(use => use.AcademicYearId).IsRequired();

            builder.Property(use => use.UniversityId).IsRequired();
            
            builder
                .HasOne(use => use.EquipmentEntity)
                .WithOne(use => use.UniversitySiteEquipmentEntity)
                .HasForeignKey<UniversitySiteEquipmentEntity>(use => use.EquipmentId);
            
            builder
                .HasOne(use => use.UniversityEntity)
                .WithMany(use => use.Equipments)
                .HasForeignKey(use => use.UniversityId);
            
            builder
                .HasOne(use => use.SiteEntity)
                .WithMany(use => use.Equipments)
                .HasForeignKey(use => use.SiteId);
            
            builder
                .HasOne(use => use.AcademicYearEntity)
                .WithMany(use => use.UniversitySiteEquipments)
                .HasForeignKey(use => use.AcademicYearId);
        }
    }
}
