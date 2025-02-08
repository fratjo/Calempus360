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
    internal class AcademicYearConfiguration : IEntityTypeConfiguration<AcademicYear>
    {
        public void Configure(EntityTypeBuilder<AcademicYear> builder)
        {
            builder.HasKey(a => a.AcademicYear_Id);
            builder.Property(a => a.AcademicYear_Id).ValueGeneratedOnAdd();
            builder.Property(a => a.DateStart).IsRequired();
            builder.Property(a => a.DateEnd).IsRequired();
            builder.Property(a => a.CreatedAt).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(a => a.UpdatedAt).IsRequired().HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();
        }
    }
}
