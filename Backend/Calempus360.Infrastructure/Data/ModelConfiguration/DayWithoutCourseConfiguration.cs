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
    internal class DayWithoutCourseConfiguration : IEntityTypeConfiguration<DayWithoutCourse>
    {
        public void Configure(EntityTypeBuilder<DayWithoutCourse> builder)
        {
            builder.HasKey(d => d.DayWithoutCourseId);

            builder.Property(d => d.DayWithoutCourseId).HasDefaultValueSql("NEWID()");
            
            builder.Property(d => d.Name).IsRequired();
            
            builder.Property(d => d.Date).IsRequired().HasColumnType("DATE");
            builder.HasIndex(d => d.Date).IsUnique();
            
            builder.Property(d => d.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            
            builder.Property(d => d.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
            
            builder
                .HasOne(a => a.AcademicYear)
                .WithMany(d => d.DaysWithoutCourse)
                .HasForeignKey(d => d.AcademicYearId);
        }
    }

}
