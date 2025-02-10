using Calempus360.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Infrastructure.Data.ModelConfiguration
{
    internal class EquipmentTypeConfiguration : IEntityTypeConfiguration<EquipmentType>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EquipmentType> builder)
        {
            builder.HasKey(e => e.EquipmentType_Id);
            builder.Property(e => e.EquipmentType_Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Code).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();
        }
    }
}
