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
    internal class DayWithoutCourseConfiguration : IEntityTypeConfiguration<DayWithoutCourseEntity>
    {
        public void Configure(EntityTypeBuilder<DayWithoutCourseEntity> builder)
        {
            builder.HasKey(d => d.DayWithoutCourseId);

            builder.Property(d => d.DayWithoutCourseId).HasDefaultValueSql("NEWID()");
            
            builder.Property(d => d.Name).IsRequired();
            
            builder.Property(d => d.Date).IsRequired().HasColumnType("DATE");
            builder.HasIndex(d => d.Date).IsUnique();
            
            builder.Property(d => d.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            
            builder.Property(d => d.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
            
            builder
                .HasOne(a => a.AcademicYearEntity)
                .WithMany(d => d.DaysWithoutCourses)
                .HasForeignKey(d => d.AcademicYearId);
        }
    }

}
