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
    internal class UniversityConfiguration : IEntityTypeConfiguration<University>
    {
        public void Configure(EntityTypeBuilder<University> builder)
        {
            builder.HasKey(u => u.UniversityId);
            builder.Property(u => u.UniversityId).HasDefaultValueSql("NEWID()");
            
            builder.Property(u => u.Name).IsRequired();
            builder.HasIndex(u => u.Name).IsUnique();
            
            builder.Property(u => u.Code).IsRequired();
            builder.HasIndex(u => u.Code).IsUnique();
            
            builder.Property(u => u.Phone).IsRequired();
            builder.HasIndex(u => u.Phone).IsUnique();
            
            builder.Property(u => u.Address).IsRequired();
            
            builder.Property(u => u.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(u => u.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
        }
    }
}
