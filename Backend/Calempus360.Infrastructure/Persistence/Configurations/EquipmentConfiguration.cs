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
    internal class EquipmentConfiguration : IEntityTypeConfiguration<EquipmentEntity>
    {
        public void Configure(EntityTypeBuilder<EquipmentEntity> builder)
        {
            builder.HasKey(e => e.EquipmentId);
            builder.Property(e => e.EquipmentId).HasDefaultValueSql("NEWID()");
            
            builder.Property(e => e.Name).IsRequired();
            
            builder.Property(e => e.Code).IsRequired();
            
            builder.Property(e => e.Brand).IsRequired();
            
            builder.Property(e => e.Model).IsRequired();
            
            builder.Property(e => e.Description).IsRequired();
            
            builder.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            
            builder.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");

            builder
                .HasOne(e => e.EquipmentTypeEntity)
                .WithMany(e => e.Equipments)
                .HasForeignKey(e => e.EquipmentTypeId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
