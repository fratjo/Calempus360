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
    internal class OptionConfiguration : IEntityTypeConfiguration<OptionEntity>
    {
        public void Configure(EntityTypeBuilder<OptionEntity> builder)
        {
            builder.HasKey(o => o.OptionId);
            builder.Property(o => o.OptionId).HasDefaultValueSql("NEWID()");
            
            builder.Property(o => o.Name).IsRequired();
            
            builder.Property(o => o.Code).IsRequired();
            builder.HasIndex(o => o.Code).IsUnique();
            
            builder.Property(o => o.Description).IsRequired();
            
            builder.Property(o => o.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            
            builder.Property(o => o.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
        }
    }
}
