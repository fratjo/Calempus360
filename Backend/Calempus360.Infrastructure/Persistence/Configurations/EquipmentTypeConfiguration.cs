using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Data.ModelConfiguration
{
    internal class EquipmentTypeConfiguration : IEntityTypeConfiguration<EquipmentTypeEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EquipmentTypeEntity> builder)
        {
            builder.HasKey(e => e.EquipmentTypeId);
            builder.Property(e => e.EquipmentTypeId).HasDefaultValueSql("NEWID()");
            
            builder.Property(e => e.Name).IsRequired();
            builder.HasIndex(e => e.Name).IsUnique();
            
            builder.Property(e => e.Code).IsRequired();
            builder.HasIndex(e => e.Code).IsUnique();
            
            builder.Property(e => e.Description).IsRequired();
            
            builder.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            
            builder.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
        }
    }
}
