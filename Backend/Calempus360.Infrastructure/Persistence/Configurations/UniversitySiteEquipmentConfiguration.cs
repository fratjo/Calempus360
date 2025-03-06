using Calempus360.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calempus360.Infrastructure.Persistence.Configurations
{
    internal class UniversitySiteEquipmentConfiguration : IEntityTypeConfiguration<UniversitySiteEquipmentEntity>
    {
        public void Configure(EntityTypeBuilder<UniversitySiteEquipmentEntity> builder)
        {
            builder.HasKey(use => use.EquipmentId);
            
            builder.Property(use => use.EquipmentId).IsRequired();

            builder.Property(use => use.UniversityId).IsRequired();
            
            builder
                .HasOne(use => use.EquipmentEntity)
                .WithOne(use => use.UniversitySiteEquipmentEntity)
                .HasForeignKey<UniversitySiteEquipmentEntity>(use => use.EquipmentId).OnDelete(DeleteBehavior.Cascade);
            
            builder
                .HasOne(use => use.UniversityEntity)
                .WithMany(use => use.Equipments)
                .HasForeignKey(use => use.UniversityId).OnDelete(DeleteBehavior.Cascade);
            
            builder
                .HasOne(use => use.SiteEntity)
                .WithMany(use => use.Equipments)
                .HasForeignKey(use => use.SiteId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
