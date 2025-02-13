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
    internal class AcademicYearConfiguration : IEntityTypeConfiguration<AcademicYear>
    {
        public void Configure(EntityTypeBuilder<AcademicYear> builder)
        {
            builder.HasKey(a => a.AcademicYearId);
            builder.Property(a => a.AcademicYearId).IsRequired();

            builder.Property(a => a.DateStart).IsRequired().HasColumnType("DATE");
            builder.Property(a => a.DateEnd).IsRequired().HasColumnType("DATE");
            
            builder.Property(a => a.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
        }
    }
}
