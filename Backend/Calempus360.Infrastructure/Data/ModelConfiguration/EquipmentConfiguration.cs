using Calempus360.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Infrastructure.Data.ModelConfiguration
{
    internal class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.HasKey(e => e.Equipment_Id);
            builder.Property(e => e.Equipment_Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Code).IsRequired();
            builder.Property(e => e.Brand).IsRequired();
            builder.Property(e => e.Model).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();

            builder.HasOne(e => e.EquipmentType).WithMany(e => e.Equipments).HasForeignKey(e => e.EquipmentType_Id);
        }
    }
}
